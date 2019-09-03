# Step by step guide

## GCP

### Container registry

Enable Container Registry API.
```bash
$ gcloud config set compute/zone us-central1-a
$ gcloud services enable containerregistry.googleapis.com
```
Create service account for role storage.admin
```bash
$ gcloud iam service-accounts create azure-pipelines-publisher --display-name "Azure Pipelines Publisher"
$ PROJECT_NUMBER=$(gcloud projects describe $(gcloud config get-value core/project) --format='value(projectNumber)')
$ AZURE_PIPELINES_PUBLISHER=$(gcloud iam service-accounts list --filter="displayName:Azure Pipelines Publisher" --format='value(email)')
$ gcloud projects add-iam-policy-binding $(gcloud config get-value core/project) \
    --member serviceAccount:$AZURE_PIPELINES_PUBLISHER --role roles/storage.admin
$ gcloud iam service-accounts keys create azure-pipelines-publisher.json --iam-account $AZURE_PIPELINES_PUBLISHER
$ tr -d '\n' < azure-pipelines-publisher.json > azure-pipelines-publisher-oneline.json
```

### Kubernetes
Create kubernetes cluster. Don't forget to add scope cloud-platform - to access cloud api. In this project we need to connect to Memorystore and CloudSQL.
```bash
$ gcloud services enable container.googleapis.com
$ gcloud container clusters create gamestore-dev --disk-size=20GB --enable-autoscaling --max-nodes=3 --min-nodes=1 --num-nodes=1 -m n2-highcpu-2  --scopes=cloud-platform --min-cpu-platform "Intel Cascade Lake" --enable-ip-alias --network=default 
```

Create kubernetes key
```bash
$ gcloud container clusters get-credentials gamestore
$ kubectl create serviceaccount azure-pipelines-deploy
$ kubectl create clusterrolebinding azure-pipelines-deploy --clusterrole=cluster-admin --serviceaccount=default:azure-pipelines-deploy
$ kubectl get secret $(kubectl get serviceaccounts azure-pipelines-deploy -o custom-columns=":secrets[0].name") -o yaml
$ gcloud container clusters describe azure-pipelines-cicd-dev --format=value\(endpoint\)
```

### CloudSQL
Create MySQL instance
```bash
$ gcloud services enable servicenetworking.googleapis.com
$ gcloud beta compute addresses create google-managed-services-default --description='Peering range reserved for Google' --global --network=default --purpose=VPC_PEERING --prefix-length=16
$ gcloud alpha services vpc-peerings connect --network=default --ranges=google-managed-services-default --service=servicenetworking.googleapis.com
$ gcloud beta sql instances create gamestore --tier=db-n1-standard-1 --zone=us-central1-a  --network=default --no-assign-ip
$ gcloud beta sql users set-password root --host=% --instance=gamestore --password klFrt@1HEToNh7
```

### Memorystore
Create Redis instance
```bash
$ gcloud services enable redis.googleapis.com
$ gcloud redis instances create gamestore --size=1 --redis-version=redis_4_0 --region us-central1 --network=default 
```

# Azure DevOps
## Add some service connections to project

New docker registry sevice connection:
- <b>Connection Name:</b> gcr
- <b>Docker Registry:</b> https://gcr.io/[PROJECT-ID], where [PROJECT-ID] is the name of your GCP project.
- <b>Docker ID:</b> _json_key
- <b>Password:</b> Paste the content of azure-pipelines-publisher-oneline.json

New kubernetes service connection:
- <b>Choose authentication:</b> Service account.
- <b>Connection name:</b> azure-pipelines-cicd-dev.
- <b>Server URL:</b> https://[MASTER-IP]/. Replace [MASTER-IP] with the IP address that you determined earlier.
- <b>Secret:</b> kubernetes key determined earlier.

## Create a build pipeline

Don't forget to add 2 variables:
```json
DockerImageName: [project-id]/gamestore
DockerRegistry: [created connection to docker registry name]
```

Create or link [azure-build-pipeline.yml](azure-build-pipeline.yml) file:
```yml
trigger:
- master

pool:
  vmImage: 'ubuntu-16.04'

steps:
- task: Docker@2
  displayName: Docker build
  inputs:
    containerRegistry: $(DockerRegistry)
    repository: $(DockerImageName)
    command: 'build'
    Dockerfile: 'GameStore.WebUI/Dockerfile'
    buildContext: '.'

- task: Bash@3
  displayName: Build & Run unit tests
  inputs:
    targetType: 'inline'
    script: |
      docker build --pull -t gamestore:unittests -f ./GameStore.UnitTests/Dockerfile .
      mkdir $(System.DefaultWorkingDirectory)/testResultsFiles
      docker run --rm -v $(System.DefaultWorkingDirectory)/testResultsFiles:/app/GameStore.UnitTests/TestResults gamestore:unittests

- task: PublishTestResults@2
  inputs:
    testRunner: VSTest
    searchFolder: $(System.DefaultWorkingDirectory)/testResultsFiles
    testResultsFiles: '**/*.trx'
    failTaskOnFailedTests: true

- task: Docker@2
  displayName: Docker push
  inputs:
    containerRegistry: $(DockerRegistry)
    repository:  $(DockerImageName)
    command: 'push'

- task: CmdLine@1
  displayName: 'Lock image version in deployment.yaml'
  inputs:
    filename: /bin/bash
    arguments: '-c "awk ''{gsub(\"GAMESTORE_IMAGE\", \"gcr.io/$(DockerImageName):$(Build.BuildId)\", $0); print}'' kubernetes/deployment.yaml > $(build.artifactstagingdirectory)/deployment.yaml"'

- task: CopyFiles@2
  displayName: 'CopyFiles used in release pipeline'
  inputs:
    SourceFolder: '$(System.DefaultWorkingDirectory)/kubernetes'
    Contents: 'deployment.sh'
    TargetFolder: '$(build.artifactstagingdirectory)'

- task: PublishBuildArtifacts@1
  displayName: 'Publish Artifact'
  inputs:
    PathtoPublish: '$(build.artifactstagingdirectory)'
```

## Create release pipeline

Select created build pipeline as artifact. Specify Source alias as "manifest".

Add Bash task to run [deployment.sh](deployment.sh) file:
```bash
#!/bin/bash
starts=$1
file=$2
env | grep $starts > _temp
sed -i 's#="#: "#g' _temp
sed -i 's/^/  /g' _temp
sed -i "s/$starts//g" _temp
sed -i '/GAMESTORE_ENV_DATA/r _temp' $file
sed -i '/GAMESTORE_ENV_DATA/d' $file
```

Add Deploy to Kubernetes task with apply command and [deployment.yaml](deployment.yaml) file:
```yml
apiVersion: v1
kind: Service
metadata:
  name: gamestore
spec:
  ports:
  - port: 80
    targetPort: 80
    protocol: TCP
    name: http
  selector:
    app: gamestore
  type: NodePort

---
apiVersion: v1
kind: ConfigMap
metadata:
  name: gamestore-env
data:
  GAMESTORE_ENV_DATA
  
---
apiVersion: extensions/v1beta1
kind: Ingress
metadata:
  name: gamestore
spec:
  backend:
    serviceName: gamestore
    servicePort: 80

---
apiVersion: extensions/v1beta1
kind: Deployment
metadata:
  name: gamestore
spec:
  replicas: 1
  template:
    metadata:
      labels:
        app: gamestore
    spec:
      containers:
      - name: gamestore
        image: GAMESTORE_IMAGE
        envFrom:
          - configMapRef:
              name: gamestore-env
        ports:
          - containerPort: 80
        livenessProbe:
          httpGet:
            path: /
            port: 80
          initialDelaySeconds: 5
          periodSeconds: 5
        readinessProbe:
          httpGet:
            path: /
            port: 80
          initialDelaySeconds: 3
          periodSeconds: 5
```

Add and link variable group to release pipeline:

```json
GameStore_Container_ConnectionStrings__GameStoreDatabase: "[connection string to games db]",
GameStore_Container_ConnectionStrings__GameStoreIdentityDatabase: "[connection string to identity db]",
GameStore_Container_Redis__Configuration: "[connection string to redis inctance]",
GameStore_Container_RedisKeys__Configuration: "[connection string to redis inctance]"
```

Make some changes to source code and look at result.

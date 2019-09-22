$envVariables = gci env: | where name -like 'GameStore_WebUI_*'
$dest = 'script.ps1'
Add-Content $dest '$envVariables = ('
$i=0
ForEach ($envVariable in $envVariables)
{
    $str = ("@{name='"+$envVariable.Name.replace("GAMESTORE_WEBUI_","")+"';value='"+$envVariable.Value+"'}")
    if($i++ -ne $envVariables.count-1){
      $str+=','
    }
    Add-Content $dest $str 
}
Add-Content $dest (");Set-WebConfigurationProperty -pspath 'IIS:\Sites\Default Web Site\GameStore' -filter 'system.webServer/aspNetCore/environmentVariables' -name '.' -value"+' $envVariables')

sudo unzip /tmp/GameStore.WebUI.zip -d /tmp/publish
sudo cp /tmp/publish/* /etc/puppetlabs/code/environments/production/modules/gamestore/files -r

sudo cp /tmp/puppet/production/* /etc/puppetlabs/code/environments/production -r

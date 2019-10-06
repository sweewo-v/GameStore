wget https://apt.puppetlabs.com/puppet6-release-bionic.deb
sudo dpkg -i puppet6-release-bionic.deb
sudo apt update
sudo apt-get install -y puppetserver
sudo apt-get install unzip

sudo systemctl start puppetserver
sudo systemctl enable puppetserver

sudo tee -a /etc/puppetlabs/puppet/autosign.conf << EOF
*.internal
EOF

mkdir -m 777 /tmp/publish
sudo mkdir -p /etc/puppetlabs/code/environments/production/modules/gamestore/files

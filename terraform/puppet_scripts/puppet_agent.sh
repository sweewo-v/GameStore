wget https://apt.puppetlabs.com/puppet6-release-bionic.deb
sudo dpkg -i puppet6-release-bionic.deb
sudo apt update
sudo apt-get install -y puppet-agent

sudo tee -a /etc/hosts << EOF
${puppet_master_address}        Puppet
EOF

sudo tee -a /etc/puppetlabs/puppet/puppet.conf << EOF
[agent]
runinterval=300
EOF

sudo systemctl start puppet
sudo systemctl enable puppet
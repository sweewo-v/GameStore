class dotnet {
  exec { 'logrotate':
    path     => '/usr/bin:/usr/sbin:/bin',
    provider => shell,
    onlyif   => "
wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo add-apt-repository universe
sudo apt-get update
sudo apt-get -y install apt-transport-https
sudo apt-get -y install aspnetcore-runtime-2.2=2.2.5-1
    ",
  }
}

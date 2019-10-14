class gamestore {
  file { '/var/www/gamestore':
    ensure => directory,
    recurse => true,
    purge => true,
    source  => 'puppet:///modules/gamestore'
  }
  file { '/etc/systemd/system/gamestore.service':
    content => "
[Unit]
Description=GameStore
[Install]
WantedBy=multi-user.target
[Service]
WorkingDirectory=/var/www/gamestore
ExecStart=/usr/bin/dotnet /var/www/gamestore/GameStore.WebUI.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=dotnet
User=www-data
EnvironmentFile=/var/www/gamestore/env.txt
    ",
  }
  service { 'gamestore':
    ensure => running,
    enable => true,
    subscribe => File["/etc/systemd/system/gamestore.service"],
  }
  exec { 'restart':
    path     => '/usr/bin:/usr/sbin:/bin',
    provider => shell,
    onlyif   => "
sudo systemctl restart gamestore
    ",
  }

  if('prod' in $hostname) {
     $file = 'secure'
  }
  else {
    $file = 'default'
  }
  file { '/etc/nginx/sites-available/default':
    source  => "puppet:///modules/nginx/$file"
  }
}

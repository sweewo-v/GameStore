class nginx {
  package { 'nginx':
    ensure => latest
  }
  service { 'nginx':
    ensure => running,
    enable => true,
    require => Package['nginx'],
    subscribe => File["/etc/nginx/sites-available/default"],
  }
}

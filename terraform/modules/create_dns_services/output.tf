output "dns_name" {
  value = google_dns_managed_zone.default.dns_name
}

output "dns_name_servers" {
  value = google_dns_managed_zone.default.name_servers
}

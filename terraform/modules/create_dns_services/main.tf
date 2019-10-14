resource "google_dns_managed_zone" "default" {
  name = "final-demo"
  dns_name = var.dns_name
}

resource "google_dns_record_set" "a" {
  name = google_dns_managed_zone.default.dns_name
  managed_zone = google_dns_managed_zone.default.name
  type = "A"
  ttl  = 300

  rrdatas = [var.backend_ip]
}

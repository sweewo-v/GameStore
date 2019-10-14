resource "google_compute_managed_ssl_certificate" "default" {
  provider = "google-beta"

  name = "${var.env}-cert"

  managed {
    domains = [var.dns_name]
  }
}

resource "google_compute_global_address" "default" {
  name = "global-${var.env}-ip"
}

resource "google_compute_target_https_proxy" "default" {
  name             = "${var.env}-proxy"
  url_map          = var.url_map
  ssl_certificates = [google_compute_managed_ssl_certificate.default.self_link]
}

resource "google_compute_global_forwarding_rule" "https" {
  name       = "${var.env}-forwarding-rule"
  ip_address = google_compute_global_address.default.address
  target     = google_compute_target_https_proxy.default.self_link
  port_range = 443
}

resource "google_compute_target_http_proxy" "default" {
  name             = "${var.env}-proxy-def"
  url_map          = var.url_map
}

resource "google_compute_global_forwarding_rule" "http" {
  name       = "${var.env}-forwarding-rule-def"
  ip_address = google_compute_global_address.default.address
  target     = google_compute_target_http_proxy.default.self_link
  port_range = 80
}

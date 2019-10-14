resource "google_compute_global_forwarding_rule" "http" {
  name       = "${var.env}-global-rule"
  target     = google_compute_target_http_proxy.default.self_link
  port_range = "80"
}

resource "google_compute_target_http_proxy" "default" {
  name        = "${var.env}-proxy"
  url_map     = var.url_map
}

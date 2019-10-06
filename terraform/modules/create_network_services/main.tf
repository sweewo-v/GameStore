resource "google_compute_network" "default" {
  name       = "${var.env}-private-network"
}

resource "google_compute_firewall" "ssh" {
  name       = "${google_compute_network.default.name}-allow-ssh"
  network    = google_compute_network.default.name

  allow {
    protocol = "tcp"
    ports    = ["22"]
  }
}

resource "google_compute_firewall" "http" {
  name    = "${google_compute_network.default.name}-backend-allow-http"
  network = google_compute_network.default.name

  allow {
    protocol = "tcp"
    ports    = ["80"]
  }

  source_ranges = ["130.211.0.0/22","35.191.0.0/16"]
  target_tags   = ["${var.env}-backend"]
}

resource "google_compute_firewall" "allow-internal" {
  name    = "${google_compute_network.default.name}-allow-internal"
  network = google_compute_network.default.name
  
  allow {
    protocol = "icmp"
  }
  
  allow {
    protocol = "tcp"
    ports    = ["0-65535"]
  }
  
  allow {
    protocol = "udp"
    ports    = ["0-65535"]
  }
  
  source_ranges = ["10.128.0.0/9"]
}

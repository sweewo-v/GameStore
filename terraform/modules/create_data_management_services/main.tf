resource "google_compute_global_address" "private_ip_address" {
  name          = "${var.env}-private-ip-address"
  purpose       = "VPC_PEERING"
  address_type = "INTERNAL"
  prefix_length = 16
  network       = var.network_name

  depends_on   = [var.network_name]
}

resource "google_service_networking_connection" "private_vpc_connection" {
  network                 = var.network_name
  service                 = "servicenetworking.googleapis.com"
  reserved_peering_ranges = [google_compute_global_address.private_ip_address.name]

  depends_on              = [var.network_name]
}

resource "random_id" "db_name_suffix" {
  byte_length = 4
}

resource "google_sql_database_instance" "default" {
  name             = "${var.env}-instance-${random_id.db_name_suffix.hex}"
  region           = var.region_name
  database_version = "MYSQL_5_7"

  settings {
    tier = var.db_machine_size
    ip_configuration {
      ipv4_enabled    = false
      private_network = var.network_name
    }
  }

  depends_on = ["google_service_networking_connection.private_vpc_connection"]
}

resource "random_id" "root_pass" {
  byte_length = 8
}

resource "google_sql_user" "root" {
  name       = "root"
  instance   = google_sql_database_instance.default.name
  host       = "%"
  password   = random_id.root_pass.hex

  depends_on = ["google_sql_database_instance.default"]
}

resource "google_redis_instance" "default" {
  name           = "${var.env}-instance"
  tier           = "BASIC"
  memory_size_gb = 1

  location_id        = var.zone_name
  authorized_network = var.network_name

  depends_on = [var.network_name]
}

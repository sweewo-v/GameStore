provider "google" {
  project = var.project_name
  region  = var.region_name
  zone    = var.zone_name
}

provider "google-beta" {
  project = var.project_name
  region  = var.region_name
  zone    = var.zone_name
}
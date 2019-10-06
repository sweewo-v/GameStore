terraform {
  backend "gcs" {
    bucket = "terraform_state_b"
    prefix = "terraform/staging"
  }
}
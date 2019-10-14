variable "project_name" {}

variable "dns_name" {}

variable "region_name" {
  type        = "string"
  description = "The region that this terraform configuration will instanciate at."
  default     = "europe-west3"
}

variable "zone_name" {
  type        = "string"
  description = "The zone that this terraform configuration will instanciate at."
  default     = "europe-west3-a"
}

variable "machine_size" {
  type        = "string"
  description = "The size that this instance will be."
  default     = "n1-standard-1"
}

variable "master_machine_size" {
  type        = "string"
  description = "The size that this instance will be."
  default     = "n1-standard-2"
}

variable "image_name" {
  type        = "string"
  description = "The kind of VM this instance will become"
  default     = "ubuntu-os-cloud/ubuntu-1804-lts"
}

variable "master_image_name" {
  type        = "string"
  description = "The kind of VM this instance will become"
  default     = "ubuntu-os-cloud/ubuntu-1804-lts"
}

variable "db_machine_size" {
  type        = "string"
  description = "The size that this instance will be."
  default     = "db-n1-standard-1"
}

variable "publish_folder_path" {
  type           = "string"
  description    = "The path to project publish directory"
  default        = "../../GameStore.WebUI.zip"
}

variable "private_key_path" {
  type        = "string"
  description = "The path to the private key used to connect to the instance"
  default     = "../ssh/id_rsa"
}

variable "public_key_path" {
  type        = "string"
  description = "The path to the private key used to connect to the instance"
  default     = "../ssh/id_rsa.pub"
}

variable "ssh_user" {
  type        = "string"
  description = "The name of the user that will be used to remote exec the script"
  default     = "DESKTOP-R09LI80"
}

variable "env" {
  type        = "string"
  description = "The name of environment"
  default     = "production"
}

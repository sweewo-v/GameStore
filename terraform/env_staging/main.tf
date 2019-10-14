module "network_services" {
  source = "../modules/create_network_services"
  env    = var.env
}

module "data_management_services" {
  source          = "../modules/create_data_management_services"
  region_name     = var.region_name
  zone_name       = var.zone_name
  network_name    = module.network_services.network_name
  env             = var.env
  db_machine_size = var.db_machine_size
}

module "application_services" {
  source              = "../modules/create_application_services"
  region_name         = var.region_name
  zone_name           = var.zone_name
  machine_size        = var.machine_size
  master_machine_size = var.master_machine_size
  image_name          = var.image_name
  master_image_name   = var.master_image_name
  publish_folder_path = var.publish_folder_path
  network_name        = module.network_services.network_name
  sql_root_password   = module.data_management_services.sql_root_password
  sql_host_ip         = module.data_management_services.sql_host_ip
  memorystore_host_ip = module.data_management_services.memorystore_host_ip
  private_key_path    = var.private_key_path
  public_key_path     = var.public_key_path
  ssh_user            = var.ssh_user
  env                 = var.env
  instance-count      = 2
}

module "http_frontend" {
  source     = "../modules/create_http_frontend"
  env        = var.env
  url_map    = module.application_services.url_map
}

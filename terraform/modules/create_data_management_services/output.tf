output "sql_root_password" {
  value = google_sql_user.root.password
}

output "sql_host_ip" {
  value = google_sql_database_instance.default.private_ip_address
}

output "memorystore_host_ip" {
  value = google_redis_instance.default.host
}

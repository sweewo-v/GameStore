resource "google_compute_instance" "puppet-master" {
  name         = "${var.env}-puppet-master"
  machine_type = var.master_machine_size
  zone         = var.zone_name

  boot_disk {
    initialize_params {
      image = var.master_image_name
    }
  }

  network_interface {
    network = var.network_name
    access_config {
    }
  }

  metadata = {
    ssh-keys    = "${var.ssh_user}:${file("${var.public_key_path}")}"
    private-key = file(var.private_key_path)
  }

  metadata_startup_script = file("../puppet_scripts/puppet_master.sh")

  lifecycle {
    ignore_changes = [
      "metadata"
    ]
  }
}

data "template_file" "env_file" {
  template = file("${path.module}/env.tpl")
  vars = {
    sql_host   = var.sql_host_ip
    sql_pass   = var.sql_root_password
    redis_host = var.memorystore_host_ip
  }
}

resource "null_resource" "env_provisioner" {
  triggers = {
    env_provisioner = uuid()
  }

  provisioner "file" {
    source      = var.publish_folder_path
    destination = "/tmp/GameStore.WebUI.zip"

    connection {
      type        = "ssh"
      host        = google_compute_instance.puppet-master.network_interface.0.access_config.0.nat_ip
      user        = var.ssh_user
      private_key = google_compute_instance.puppet-master.metadata.private-key
    }
  }

  provisioner "file" {
    source      = "../puppet"
    destination = "/tmp"

    connection {
      type        = "ssh"
      host        = google_compute_instance.puppet-master.network_interface.0.access_config.0.nat_ip
      user        = var.ssh_user
      private_key = google_compute_instance.puppet-master.metadata.private-key
    }
  }

  provisioner "file" {
    content     = data.template_file.env_file.rendered
    destination = "/tmp/publish/env.txt"

    connection {
      type        = "ssh"
      host        = google_compute_instance.puppet-master.network_interface.0.access_config.0.nat_ip
      user        = var.ssh_user
      private_key = google_compute_instance.puppet-master.metadata.private-key
    }
  }

  provisioner "remote-exec" {
    script = "../puppet_scripts/pupet_master_repeat.sh"

    connection {
      type        = "ssh"
      host        = google_compute_instance.puppet-master.network_interface.0.access_config.0.nat_ip
      user        = var.ssh_user
      private_key = google_compute_instance.puppet-master.metadata.private-key
    }
  }
}

data "template_file" "metadata_startup_script" {
  template = file("../puppet_scripts/puppet_agent.sh")
  vars = {
    puppet_master_address = google_compute_instance.puppet-master.network_interface.0.network_ip
  }
}

resource "google_compute_instance_template" "default" {
  name_prefix  = "${var.env}-"
  machine_type = var.machine_size
  region       = var.region_name

  tags = ["${var.env}-backend"]

  disk {
    source_image = var.image_name
  }

  network_interface {
    network = var.network_name
    access_config {
    }
  }

  lifecycle {
    create_before_destroy = true
  }

  metadata_startup_script = data.template_file.metadata_startup_script.rendered
  depends_on              = ["google_compute_instance.puppet-master", "null_resource.env_provisioner"]
}

resource "google_compute_instance_group_manager" "default" {
  name               = var.env
  instance_template  = google_compute_instance_template.default.self_link
  base_instance_name = var.env
  zone               = var.zone_name
  target_size        = var.min_replicas
}

resource "google_compute_url_map" "default" {
  name            = var.env
  default_service = google_compute_backend_service.default.self_link
}

resource "google_compute_backend_service" "default" {
  name         = var.env
  port_name    = "http"
  protocol     = "HTTP"
  timeout_sec  = 10

  backend {
    group = google_compute_instance_group_manager.default.instance_group
  }

  health_checks = [google_compute_http_health_check.default.self_link]
}

resource "google_compute_http_health_check" "default" {
  name                = "${var.env}-health-check"
  request_path        = "/"
  check_interval_sec  = 10
  unhealthy_threshold = 10
}

resource "google_compute_autoscaler" "default" {
  name   = "${var.env}-autoscaler"
  zone   = var.zone_name
  target = google_compute_instance_group_manager.default.self_link

  autoscaling_policy {
    max_replicas    = var.max_replicas
    min_replicas    = var.min_replicas
    cooldown_period = 60

    cpu_utilization {
      target = 0.5
    }
  }
}
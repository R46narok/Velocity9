resource "kubernetes_deployment_v1" "skeletal-api" {
  metadata {
    name = "skeletal-depl"
  }
  spec {
    replicas = 1
    selector {
      match_labels = {
        "service" = "skeletal"
      }
    }
    template {
      metadata {
        labels = {
          "app"     = "skeletal"
          "service" = "skeletal"
        }
      }

      spec {
        container {
          name  = "skeletal"
          image = "d3ds3g/v9-skeletal-api"
          port {
            container_port = 80
            protocol       = "TCP"
          }
          env {
            name  = "ASPNETCORE_URLS"
            value = "http://+:80"
          }
          env {
            name = "EnvConnection"
            value = "Data Source=${var.skeletalmssql},1433;Initial Catalog = skeletal;User id=skeletaladmin;Password=sqladmin123!@#;"
          }
        }
      }
    }
  }
}

resource "kubernetes_service" "skeletal-loadbalancer" {
  metadata {
    name = "skeletal-loadbalancer"
  }
  spec {
    selector = {
      app = "skeletal"
    }
    port {
      port        = 80
      target_port = 80
    }

    type = "LoadBalancer"
  }
}
resource "kubernetes_deployment_v1" "authorization-api" {
  metadata {
    name = "authorization-depl"
  }
  spec {
    replicas = 1
    selector {
      match_labels = {
        "service" = "authorization"
      }
    }
    template {
      metadata {
        labels = {
          "app"     = "authorization"
          "service" = "authorization"
        }
      }

      spec {
        container {
          name  = "authorization"
          image = "d3ds3g/v9-authorization-api"
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
            value = "Data Source=${var.authmssql},1433;Initial Catalog = authorization;User id=authadmin;Password=sqladmin123!@#;"
          }
        }
      }
    }
  }
}

resource "kubernetes_service" "authorization-loadbalancer" {
  metadata {
    name = "authorization-loadbalancer"
  }
  spec {
    selector = {
      app = "authorization"
    }
    port {
      port        = 80
      target_port = 80
    }

    type = "LoadBalancer"
  }
}
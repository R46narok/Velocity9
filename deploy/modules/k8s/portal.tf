resource "kubernetes_deployment_v1" "portal" {
  metadata {
    name = "portal-depl"
  }
  spec {
    replicas = 1
    selector {
      match_labels = {
        "service" = "portal"
      }
    }
    template {
      metadata {
        labels = {
          "app"     = "portal"
          "service" = "portal"
        }
      }

      spec {
        container {
          name  = "portal"
          image = "d3ds3g/v9-portal"
          port {
            container_port = 80
            protocol       = "TCP"
          }
          env {
            name  = "ASPNETCORE_URLS"
            value = "http://+:80"
          }
          
        }
      }
    }
  }
}

resource "kubernetes_service" "portal-loadbalancer" {
  metadata {
    name = "portal-loadbalancer"
  }
  spec {
    selector = {
      app = "portal"
    }
    port {
      port        = 80
      target_port = 80
    }

    type = "LoadBalancer"
  }
}
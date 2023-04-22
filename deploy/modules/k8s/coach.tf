resource "kubernetes_deployment_v1" "coach-api" {
  metadata {
    name = "coach-depl"
  }
  spec {
    replicas = 1
    selector {
      match_labels = {
        "service" = "coach"
      }
    }
    template {
      metadata {
        labels = {
          "app"     = "coach"
          "service" = "coach"
        }
      }

      spec {
        container {
          name  = "coach"
          image = "d3ds3g/v9-coach-api"
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

resource "kubernetes_service" "coach-loadbalancer" {
  metadata {
    name = "coach-loadbalancer"
  }
  spec {
    selector = {
      app = "coach"
    }
    port {
      port        = 80
      target_port = 80
    }

    type = "LoadBalancer"
  }
}
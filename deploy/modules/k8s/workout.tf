resource "kubernetes_deployment_v1" "workout-api" {
  metadata {
    name = "workout-depl"
  }
  spec {
    replicas = 1
    selector {
      match_labels = {
        "service" = "workout"
      }
    }
    template {
      metadata {
        labels = {
          "app"     = "workout"
          "service" = "workout"
        }
      }

      spec {
        container {
          name  = "workout"
          image = "d3ds3g/v9-workout-api"
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
            value = "Data Source=${var.workoutmssql},1433;Initial Catalog = workout;User id=workoutadmin;Password=sqladmin123!@#;"
          }
        }
      }
    }
  }
}

resource "kubernetes_service" "workout-loadbalancer" {
  metadata {
    name = "workout-loadbalancer"
  }
  spec {
    selector = {
      app = "workout"
    }
    port {
      port        = 80
      target_port = 80
    }

    type = "LoadBalancer"
  }
}
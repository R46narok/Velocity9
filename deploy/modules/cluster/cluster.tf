resource "azurerm_resource_group" "rg" {
  name = "v9Depl"
  location = var.location
}

resource "azurerm_kubernetes_cluster" "cluster" {
  name = "v9Cluster"
  resource_group_name = azurerm_resource_group.rg.name
  location = azurerm_resource_group.rg.location
  dns_prefix = "v9cluster"

  default_node_pool {
    name       = "default"
    node_count = "1"
    vm_size    = "standard_d2_v2"
  }

  service_principal  {
    client_id = var.serviceprinciple_id
    client_secret = var.serviceprinciple_key
  }

  linux_profile {
    admin_username = "v9user"
    ssh_key {
        key_data = var.ssh_key
    }
  }

  network_profile {
      network_plugin = "kubenet"
      load_balancer_sku = "standard"
  }
  
}

resource "azurerm_mssql_server" "authmssql" {
  name                         = "authmssql"
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  version                      = "12.0"
  administrator_login          = "authadmin"
  administrator_login_password = "sqladmin123!@#"
  minimum_tls_version          = "1.2"
}

resource "azurerm_mssql_server" "skeletalmssql" {
  name                         = "skeletalmssql"
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  version                      = "12.0"
  administrator_login          = "skeletaladmin"
  administrator_login_password = "sqladmin123!@#"
  minimum_tls_version          = "1.2"
}

resource "azurerm_mssql_server" "workoutmssql" {
  name                         = "workoutmssql"
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  version                      = "12.0"
  administrator_login          = "workoutadmin"
  administrator_login_password = "sqladmin123!@#"
  minimum_tls_version          = "1.2"
}

resource "azurerm_mssql_firewall_rule" "kubernetes-pod-access" {
  name                = "kubernetes-pod-access"
  server_id         = azurerm_mssql_server.authmssql.id
  start_ip_address    = "0.0.0.0"
  end_ip_address      = "0.0.0.0"
}

resource "azurerm_mssql_firewall_rule" "kubernetes-pod-access1" {
  name                = "kubernetes-pod-access"
  server_id         = azurerm_mssql_server.skeletalmssql.id
  start_ip_address    = "0.0.0.0"
  end_ip_address      = "0.0.0.0"
}

resource "azurerm_mssql_firewall_rule" "kubernetes-pod-access2" {
  name                = "kubernetes-pod-access"
  server_id         = azurerm_mssql_server.workoutmssql.id
  start_ip_address    = "0.0.0.0"
  end_ip_address      = "0.0.0.0"
}
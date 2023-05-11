output "kube_config" {
    value = azurerm_kubernetes_cluster.cluster.kube_config_raw
}

output "cluster_ca_certificate" {
    value = azurerm_kubernetes_cluster.cluster.kube_config.0.cluster_ca_certificate
}

output "client_certificate" {
    value = azurerm_kubernetes_cluster.cluster.kube_config.0.client_certificate
}

output "client_key" {
    value = azurerm_kubernetes_cluster.cluster.kube_config.0.client_key
}

output "host" {
    value = azurerm_kubernetes_cluster.cluster.kube_config.0.host
}

output "authmssql" {
    value = azurerm_mssql_server.authmssql.fully_qualified_domain_name
}

output "skeletalmssql" {
    value = azurerm_mssql_server.skeletalmssql.fully_qualified_domain_name
}

output "workoutmssql" {
    value = azurerm_mssql_server.workoutmssql.fully_qualified_domain_name
}




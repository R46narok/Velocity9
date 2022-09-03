using System;
using System.Linq;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Xunit;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Infrastructure.MessageBrokers;
using ZeroGravity.Services.Authorization.Data.Persistence;

namespace ZeroGravity.Tests.Integration.Authorization;

public class AuthorizationWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
   private readonly MsSqlTestcontainer _dbContainer;

   private readonly int _dbPort = Random.Shared.Next(10000, 60000);
   
   public AuthorizationWebApplicationFactory()
   {
      _dbContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
         .WithDatabase(new MsSqlTestcontainerConfiguration
         {
            Port = _dbPort,
            Password = "#Password1234"
         })
         .Build();
   }

   protected override void ConfigureWebHost(IWebHostBuilder builder)
   {
      builder.ConfigureAppConfiguration(configurationBuilder =>
      {
         var integrationConfig = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
         configurationBuilder.AddConfiguration(integrationConfig);
      });

      builder.ConfigureTestServices(services =>
      {
         RemoveDescriptor<DbContextOptions<UserDbContext>>(services);
         RemoveDescriptor<UserDbContext>(services);

         services.AddDbContext<UserDbContext>(
               opt => opt.UseSqlServer(_dbContainer.ConnectionString));

         var provider = services.BuildServiceProvider();
      });
   }

   private void RemoveDescriptor<T>(IServiceCollection services)
   {
      var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(T))!;
      services.Remove(descriptor);
   }
   
   public async Task InitializeAsync()
   {
      await _dbContainer.StartAsync();
   }

   public new async Task DisposeAsync()
   {
      await _dbContainer.StopAsync();
   }
}
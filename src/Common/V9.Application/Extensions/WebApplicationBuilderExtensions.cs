﻿using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using V9.Application.Behaviours;

namespace V9.Application.Extensions;

public static class WebApplicationBuilderExtensions
{
   public static IServiceCollection AddMediatorAndFluentValidation(this IServiceCollection services, Assembly[] assemblies)
   {
       services.AddMediatR(assemblies);
       
       services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
       services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
       services.AddValidatorsFromAssemblies(assemblies);
       services.AddAutoMapper(assemblies);
       
       return services;
   }

   public static void AddPersistence<T>(this WebApplicationBuilder builder, string name = "Database")
       where T : DbContext
   {
       var connectionString = builder.Configuration.GetConnectionString(name);
       builder.Services.AddDbContext<T>(opt =>
           {
               opt.UseSqlServer(connectionString);
               opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
           }
       );
    }
   
}
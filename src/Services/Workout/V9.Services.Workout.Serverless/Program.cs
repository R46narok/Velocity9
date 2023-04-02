using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using V9.Application.Extensions;
using V9.Application.Infrastructure.MessageBrokers;
using V9.Infrastructure.MessageBrokers;
using V9.Services.Workout.Commands;
using V9.Services.Workout.Data.Entities;
using V9.Services.Workout.Data.Persistence;
using V9.Services.Workout.Data.Repositories;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(builder =>
    {
        var configuration = builder
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("host.settings.json", optional: true, reloadOnChange: true)
            .Build();

        builder.AddConfiguration(configuration);
    })
    .ConfigureServices((host, services)=>
    {
        var config = host.Configuration;

        var conn = config.GetConnectionString("Database");
        services.AddDbContext<WorkoutDbContext>(opt =>
            opt.UseSqlServer(conn));

        services.AddTransient<ISetRepository, SetRepository>();
        services.AddTransient<IMuscleRepository, MuscleRepository>();
        services.AddTransient<IWorkoutRepository, WorkoutRepository>();
        services.AddTransient<IExerciseRepository, ExerciseRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IPreferencesRepository, PreferencesRepository>();
        
        services.AddSingleton<IMessagePublisher, MessagePublisher>();
        services.AddMediatorAndFluentValidation(new[] {typeof(WorkoutDbContext).Assembly, typeof(Program).Assembly});
    })
    .Build();

host.Run();
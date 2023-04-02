using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using V9.Application.Extensions;
using V9.Application.Infrastructure.MessageBrokers;
using V9.Infrastructure.MessageBrokers;
using V9.Services.Skeletal.Data.Persistence;
using V9.Services.Skeletal.Data.Repositories;

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
    .ConfigureServices((host, services) =>
    {
        var config = host.Configuration;

        var conn = config.GetConnectionString("Database");
        services.AddDbContext<SkeletalDbContext>(opt =>
            opt.UseSqlServer(conn));

        services.AddTransient<IFiberRepository, FiberRepository>();
        services.AddTransient<IAuthorRepository, AuthorRepository>();
        services.AddTransient<IMuscleRepository, MuscleRepository>();
        services.AddTransient<IMuscleGroupRepository, MuscleGroupRepository>();
        services.AddTransient<IExerciseRepository, ExerciseRepository>();

        services.AddSingleton<IMessagePublisher, MessagePublisher>();
        services.AddMediatorAndFluentValidation(new[] {typeof(SkeletalDbContext).Assembly, typeof(Program).Assembly});
    })
    .Build();

host.Run();
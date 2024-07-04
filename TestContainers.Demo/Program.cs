using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TestContainers.Demo.Data;
using TestContainers.Demo.Services;

//https://testcontainers.com/guides/getting-started-with-testcontainers-for-dotnet/
var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddScoped<ICustomerService, CustomerService>();
        services.AddSingleton<DbConnectionProvider>(_ =>
            new DbConnectionProvider(configuration.GetConnectionString("Database")!));
    })
    .Build();

host.Run();
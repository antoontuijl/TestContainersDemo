using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TestContainers.Demo.Services;

//https://testcontainers.com/guides/getting-started-with-testcontainers-for-dotnet/
var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddScoped<ICustomerService, CustomerService>();
    })
    .Build();

host.Run();
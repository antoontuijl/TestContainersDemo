using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestContainers.Demo.Data;
using TestContainers.Demo.Services;

namespace TestContainers.Demo.Tests;

public static class TestHelpers
{
    public static IHostBuilder ConfigureDefaultTestHost(this IHostBuilder builder, CustomerFunctionFixture customerFunctionFixture)
    {
        return
            builder
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .ConfigureServices(services =>
                {
                    services.AddScoped<ICustomerService, CustomerService>();
                    services.AddSingleton<DbConnectionProvider>(_ =>
                        new DbConnectionProvider(customerFunctionFixture.GetPostgresDbConnectionString()));

                    services.RemoveAll(typeof(IHostedService));
                    
                    services.AddTransient<CustomerFunction>();
                });
    }
}
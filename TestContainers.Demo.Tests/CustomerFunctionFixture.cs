using Testcontainers.PostgreSql;

namespace TestContainers.Demo.Tests;

public class CustomerFunctionFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .Build();

    public Task InitializeAsync()
    {
        return _postgres.StartAsync();
    }

    public Task DisposeAsync()
    {
        return _postgres.DisposeAsync().AsTask();
    }
    
    public string GetPostgresDbConnectionString() => _postgres.GetConnectionString();
}
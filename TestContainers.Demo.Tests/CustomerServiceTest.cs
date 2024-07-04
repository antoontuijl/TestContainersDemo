using TestContainers.Demo.Data;
using TestContainers.Demo.Dtos;
using TestContainers.Demo.Services;
using Testcontainers.PostgreSql;

namespace TestContainers.Demo.Tests;

public sealed class CustomerServiceTest : IClassFixture<CustomerFunctionFixture>
{
    private readonly CustomerFunctionFixture _customerFunctionFixtureFixture;

    public CustomerServiceTest(CustomerFunctionFixture customerFunctionFixtureFixture)
    {
        _customerFunctionFixtureFixture = customerFunctionFixtureFixture;
    }

    [Fact]
    public void ShouldReturnTwoCustomers()
    {
        // Arrange
        var customerService = new CustomerService(new DbConnectionProvider(_customerFunctionFixtureFixture.GetPostgresDbConnectionString()));

        // Act
        customerService.Create(new CustomerDto(1, "George"));
        customerService.Create(new CustomerDto(2, "John"));
        var customers = customerService.GetCustomers();

        // Assert
        Assert.Equal(2, customers.Count());
    }
}
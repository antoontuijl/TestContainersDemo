using TestContainers.Demo.Data;
using TestContainers.Demo.Dtos;
using TestContainers.Demo.Services;

namespace TestContainers.Demo.Tests;

public sealed class CustomerServiceTest(CustomerFunctionFixture customerFunctionFixture)
    : IClassFixture<CustomerFunctionFixture>
{
    [Fact]
    public void ShouldReturnTwoCustomers()
    {
        // Arrange
        var customerService = new CustomerService(new DbConnectionProvider(customerFunctionFixture.GetPostgresDbConnectionString()));

        // Act
        customerService.Create(new CustomerDto(1, "George"));
        customerService.Create(new CustomerDto(2, "John"));
        var customers = customerService.GetCustomers();

        // Assert
        Assert.Equal(2, customers.Count());
    }
}
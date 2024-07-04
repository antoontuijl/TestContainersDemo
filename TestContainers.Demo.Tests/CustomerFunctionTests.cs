using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using TestContainers.Demo.Dtos;

namespace TestContainers.Demo.Tests;

public class CustomerFunctionTests : IClassFixture<CustomerFunctionFixture>
{
    private readonly CustomerFunctionFixture _customerFunctionFixtureFixture;

    public CustomerFunctionTests(CustomerFunctionFixture customerFunctionFixtureFixture)
    {
        _customerFunctionFixtureFixture = customerFunctionFixtureFixture;
    }
    
    [Fact]
    public async Task Post_ShouldCreateNote_WhenCalledWithValidNoteDetails()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureDefaultTestHost(_customerFunctionFixtureFixture)
            .Build();

        using (host)
        {
            await host.StartAsync();

            var sut = host.Services.GetRequiredService<CustomerFunction>();

            // Arrange
            var customerDto = new CustomerDto(1, "George");
            var context = new FakeFunctionContext();
            var req = new FakeHttpRequestData(context,
                new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(customerDto)))
            );
            
            // Act
            var response = await sut.Run(req, context);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using TestContainers.Demo.Dtos;

namespace TestContainers.Demo.Tests;

public class CustomerFunctionTests(CustomerFunctionFixture customerFunctionFixtureFixture)
    : IClassFixture<CustomerFunctionFixture>
{
    [Fact]
    public async Task Post_ShouldCreateNote_WhenCalledWithValidNoteDetails()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureDefaultTestHost(customerFunctionFixtureFixture)
            .Build();

        using (host)
        {
            await host.StartAsync();

            var sut = host.Services.GetRequiredService<CustomerFunction>();

            // Arrange
            var customerDto = new CustomerDto(1, "George");
            var context = new FakeFunctionContext();
            var json = JsonConvert.SerializeObject(customerDto);
            var req = new FakeHttpRequestData(context,
                new MemoryStream(Encoding.UTF8.GetBytes(json))
            );
            
            // Act
            var response = await sut.CreateCustomer(req);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
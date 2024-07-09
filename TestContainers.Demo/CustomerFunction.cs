using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TestContainers.Demo.Dtos;
using TestContainers.Demo.Services;

namespace TestContainers.Demo;

public class CustomerFunction(ICustomerService customerService, ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<CustomerFunction>();

    [Function("CreateCustomer")]
    public async Task<HttpResponseData> CreateCustomer([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        var json = await req.ReadAsStringAsync()  ?? throw new ArgumentException("JSON body is not valid");
        CustomerDto customerDto = JsonConvert.DeserializeObject<CustomerDto>(json);
        customerService.Create(customerDto);

        HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        await response.WriteStringAsync("Customer created");

        return response;
    }
}
using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using TestContainers.Demo.Dtos;

namespace TestContainers.Demo;

public class TestFunction
{
    private readonly ILogger _logger;

    public TestFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<TestFunction>();
    }

    [Function("TestFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        CustomerDto customerDto = await req.ReadFromJsonAsync<CustomerDto>();
        
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString("Welcome to Azure Functions!");

        return response;
        
    }
}
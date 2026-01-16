using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CustomMiddleware;

public class CustomMiddleware
{
    private readonly ILogger<CustomMiddleware> _logger;

    public CustomMiddleware(ILogger<CustomMiddleware> logger)
    {
        _logger = logger;
    }

    [Function("CustomMiddleware")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        int.TryParse(req.Query["number"], out int number);
        int res = 10 / number;
        return new OkObjectResult(new {result=res});
    }
}
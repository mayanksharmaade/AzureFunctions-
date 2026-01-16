using CustomMiddleware;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

var host = new HostBuilder().ConfigureFunctionsWebApplication(builder =>
builder.UseMiddleware<CustomExceptionHandler>())

.ConfigureServices(services =>
{
   services.AddApplicationInsightsTelemetryWorkerService();
   services .ConfigureFunctionsApplicationInsights();
})


.Build();

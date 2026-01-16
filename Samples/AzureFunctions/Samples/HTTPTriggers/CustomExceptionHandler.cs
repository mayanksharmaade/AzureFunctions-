using Azure;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomMiddleware
{
    public class CustomExceptionHandler : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
             try
            {
                await next(context);
                Console.WriteLine("No Exception Occured");
            }
            catch( Exception ex)
            {
                var request = await context.GetHttpRequestDataAsync();
                var response = request!.CreateResponse();
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;

                var ErrorMessage = new { Message = "An unhandked exception Occured, Please Try later", Exception = ex.Message };
                string Responsebody = JsonSerializer.Serialize(ErrorMessage);
                await response.WriteStringAsync(Responsebody);
                Console.WriteLine("Exception Occured");
                context.GetInvocationResult().Value = response;
            }
          
        }
    }
}

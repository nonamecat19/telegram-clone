using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.MiddleWare;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            #pragma warning disable CA2254
            logger.LogError(e, e.Message);
            #pragma warning restore CA2254
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment()
                ? new ApiException((int)HttpStatusCode.InternalServerError, e.Message, e.StackTrace!.ToString())
                : new ApiException((int)HttpStatusCode.InternalServerError);
            
            var json = JsonSerializer.Serialize(response, JsonSerializerOptions);

            await context.Response.WriteAsync(json);
        }
    }
}
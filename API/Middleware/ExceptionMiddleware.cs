using System.Net;
using System.Text.Json;
using API.Erros;


namespace API.ExcpetionMiddleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> Logger, IHostEnvironment env)
{

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "{message}", ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment()
            ? new ApiExcpetion(context.Response.StatusCode, ex.Message, ex.StackTrace)
            : new ApiExcpetion(context.Response.StatusCode, ex.Message, "Internal server error");

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);


        }
    }
}
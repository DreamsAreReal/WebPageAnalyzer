using System.Text.Json;
using WebPageAnalyzer.Exceptions;

namespace WebPageAnalyzer.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;


    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }


    /// <summary>
    ///     It is necessary to process in more detail, but in order to save time, I neglected this
    /// </summary>
    /// <param name="httpContext"></param>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException ex)
        {
            await ConstructResponse(httpContext, ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            await ConstructResponse(httpContext, 500, ex.Message);
        }
    }

    private Task ConstructResponse(HttpContext context, int statusCode, string message)
    {
        var errorResponse = JsonSerializer.Serialize(
            new
            {
                Code = statusCode,
                Message = message
            });

        return context.Response.WriteAsync(errorResponse);
    }
}
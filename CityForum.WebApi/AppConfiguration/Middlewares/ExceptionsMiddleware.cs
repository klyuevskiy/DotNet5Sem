using System.Text.Json;
using CityForum.Shared.Exceptions;
using CityForum.WebApi.Extensions;
using CityForum.WebApi.Models;

namespace CityForum.WebApi.AppConfiguration.Middlewares;

public class ExceptionsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionsMiddleware> logger;


    public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger)
    {
        this._next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        ErrorResponse response = null;
        try
        {
            await _next.Invoke(context);
        }
        catch (LogicException pe)
        {
            logger.LogError(pe.ToString());
            response = pe.ToErrorResponse();
        }
        catch (RepositoryException re)
        {
            logger.LogError(re.ToString());
            response = re.ToErrorResponse();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            throw;
        }
        finally
        {
            if (!(response is null))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                await context.Response.StartAsync();
                await context.Response.CompleteAsync();
                logger.LogError($"Request: {context.Request.Method} {context.Request.Path}, responsed 400, bad Request with error response {JsonSerializer.Serialize(response)}");
            }
        }
    }
}
using System.Net;
using TechStore.Core.Exceptions;

namespace TechStore.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            await WriteProblem(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (BusinessRuleException ex)
        {
            await WriteProblem(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception)
        {
            await WriteProblem(
                context,
                HttpStatusCode.InternalServerError,
                "Erro interno inesperado."
            );
        }
    }

    private static async Task WriteProblem(HttpContext context, HttpStatusCode code, string message)
    {
        context.Response.StatusCode = (int)code;
        context.Response.ContentType = "application/problem+json";

        var problem = new
        {
            title = message,
            status = (int)code,
            traceId = context.TraceIdentifier,
        };

        await context.Response.WriteAsJsonAsync(problem);
    }
}

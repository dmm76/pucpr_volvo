using System.Net;
using TechStore.Api.Errors;
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
            await WriteProblem(context, HttpStatusCode.NotFound, ex.Code);
        }
        catch (BusinessRuleException ex)
        {
            await WriteProblem(context, HttpStatusCode.BadRequest, ex.Code);
        }
        catch (Exception)
        {
            await WriteProblem(
                context,
                HttpStatusCode.InternalServerError,
                ErrorCodes.InternalServerError
            );
        }
    }

    private static async Task WriteProblem(HttpContext context, HttpStatusCode status, string code)
    {
        context.Response.StatusCode = (int)status;
        context.Response.ContentType = "application/problem+json";

        var problem = new
        {
            title = ErrorMessagesPtBr.Get(code),
            status = (int)status,
            traceId = context.TraceIdentifier,
            code,
        };

        await context.Response.WriteAsJsonAsync(problem);
    }
}

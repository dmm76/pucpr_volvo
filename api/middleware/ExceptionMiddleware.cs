using System.Net;
using TechStore.Api.Errors;
using TechStore.Core.Exceptions;

namespace TechStore.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            _logger.LogInformation(
                ex,
                "NotFoundException: {Code}. TraceId: {TraceId}",
                ex.Code,
                context.TraceIdentifier
            );

            await WriteProblem(context, HttpStatusCode.NotFound, ex.Code);
        }
        catch (BusinessRuleException ex)
        {
            _logger.LogWarning(
                ex,
                "BusinessRuleException: {Code}. TraceId: {TraceId}",
                ex.Code,
                context.TraceIdentifier
            );

            await WriteProblem(context, HttpStatusCode.BadRequest, ex.Code);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Unhandled exception. {Method} {Path}. Code: {Code}. Status: {Status}. TraceId: {TraceId}",
                context.Request.Method,
                context.Request.Path.Value,
                ErrorCodes.InternalServerError,
                (int)HttpStatusCode.InternalServerError,
                context.TraceIdentifier
            );
        }
    }

    private static async Task WriteProblem(HttpContext context, HttpStatusCode status, string code)
    {
        context.Response.StatusCode = (int)status;
        context.Response.ContentType = "application/problem+json";

        // Header extra pra facilitar copiar/achar nos logs
        context.Response.Headers["X-Trace-Id"] = context.TraceIdentifier;

        var title = ErrorMessagesPtBr.Get(code);

        // se não traduzido, title == code
        if (title == code)
            title = (int)status < 500 ? "Requisição inválida." : "Erro interno inesperado.";

        var problem = new
        {
            title,
            status = (int)status,
            traceId = context.TraceIdentifier,
            code,
        };

        await context.Response.WriteAsJsonAsync(problem);
    }
}

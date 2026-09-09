using AccountService.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace AccountService.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (SaldoNoDisponibleException ex)
        {
            _logger.LogWarning(
                ex,
                "Saldo insuficiente para realizar el movimiento.");

            await HandleExceptionAsync(
                context,
                HttpStatusCode.BadRequest,
                ex.Message
            );
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(
                ex,
                "Recurso no encontrado.");

            await HandleExceptionAsync(
                context,
                HttpStatusCode.NotFound,
                ex.Message
            );
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(
                ex,
                "Error de operación.");

            await HandleExceptionAsync(
                context,
                HttpStatusCode.Conflict,
                ex.Message
            );
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(
                ex,
                "Argumento inválido.");

            await HandleExceptionAsync(
                context,
                HttpStatusCode.BadRequest,
                ex.Message
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error no controlado.");

            await HandleExceptionAsync(
                context,
                HttpStatusCode.InternalServerError,
                "Ocurrió un error interno en el servidor."
            );
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            mensaje = message
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response)
        );
    }
}
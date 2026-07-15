using Conectea.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }


    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        context.Response.ContentType = "application/json";


        switch (exception)
        {
            case ValidationException validationException:

                context.Response.StatusCode = 
                    StatusCodes.Status400BadRequest;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        message = validationException.Message,
                        errors = validationException.Errors
                    },
                    cancellationToken);

                return true;


            case NotFoundException notFoundException:

                context.Response.StatusCode =
                    StatusCodes.Status404NotFound;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        message = notFoundException.Message
                    },
                    cancellationToken);

                return true;


            case ConflictException conflictException:

                context.Response.StatusCode =
                    StatusCodes.Status409Conflict;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        message = conflictException.Message
                    },
                    cancellationToken);

                return true;


            default:

                _logger.LogError( exception, "Erro não tratado");

                context.Response.StatusCode =
                    StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        message = "Erro interno do servidor."
                    },
                    cancellationToken);

                return true;
        }
    }
}
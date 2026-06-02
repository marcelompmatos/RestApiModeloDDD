using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Domain.Exceptions;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestApiModeloDDD.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
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
            catch (DomainValidationException ex)
            {
                _logger.LogWarning(ex,
                    "Erro de validação");

                var response = new
                {
                    erro = "Erro de validação",
                    detalhes = ex.Errors
                };

                await WriteResponseAsync(
                    context,
                    StatusCodes.Status400BadRequest,
                    response);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex,
                    "Recurso não encontrado");

                var response = new
                {
                    erro = ex.Message
                };

                await WriteResponseAsync(
                    context,
                    StatusCodes.Status404NotFound,
                    response);
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(ex,
                    "Erro de domínio");

                var response = new
                {
                    erro = ex.Message
                };

                await WriteResponseAsync(
                    context,
                    StatusCodes.Status400BadRequest,
                    response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Erro interno não tratado");

                var response = new
                {
                    erro = "Ocorreu um erro interno no servidor."
                };

                await WriteResponseAsync(
                    context,
                    StatusCodes.Status500InternalServerError,
                    response);
            }
        }

        private static async Task WriteResponseAsync(
            HttpContext context,
            int statusCode,
            object response)
        {
            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = new object();
            int statusCode;

            switch (ex)
            {
                case DomainValidationException validationEx:
                    _logger.LogWarning(validationEx, "Erro de validação");

                    statusCode = StatusCodes.Status400BadRequest;
                    response = new
                    {
                        erro = "Erro de validação",
                        detalhes = validationEx.Errors
                    };
                    break;

                case NotFoundException notFoundEx:
                    _logger.LogWarning(notFoundEx, "Recurso não encontrado");

                    statusCode = StatusCodes.Status404NotFound;
                    response = new
                    {
                        erro = notFoundEx.Message
                    };
                    break;

                case DomainException domainEx:
                    _logger.LogWarning(domainEx, "Erro de domínio");

                    statusCode = StatusCodes.Status400BadRequest;
                    response = new
                    {
                        erro = domainEx.Message
                    };
                    break;

                default:
                    _logger.LogError(ex, "Erro interno não tratado");

                    statusCode = StatusCodes.Status500InternalServerError;
                    response = new
                    {
                        erro = "Ocorreu um erro interno no servidor."
                    };
                    break;
            }

            await WriteResponseAsync(context, statusCode, response);
        }

        private static async Task WriteResponseAsync(
            HttpContext context,
            int statusCode,
            object response)
        {
            if (context.Response.HasStarted)
                return;

            context.Response.Clear();
            context.Response.StatusCode = statusCode;

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            });

            await context.Response.WriteAsync(json);
        }
    }
}
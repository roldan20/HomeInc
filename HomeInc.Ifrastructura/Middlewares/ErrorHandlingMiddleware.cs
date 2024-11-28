using HomeInc.Domain.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace HomeInc.Ifrastructura.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IHostEnvironment hostEnvironment)
        {
            _next = next;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Continuar con la solicitud
                await _next(context);
            }
            catch (Exception ex)
            {
                // Manejar excepciones
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Loguear la excepción
            _logger.LogError(exception, "Ocurrió un error no manejado.");

            // Determinar el tipo de error y construir la respuesta
            context.Response.ContentType = "application/json";

            // Establecer un código de estado por defecto
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Crear un objeto de respuesta estándar
            var response = new
            {
                mensaje = "Ocurrió un error en el servidor.",
                detalle = _hostEnvironment.IsDevelopment() ? exception.StackTrace : null // Mostrar stacktrace solo en desarrollo
            };

            // Si es una excepción específica del dominio, puedes personalizar la respuesta
            if (exception is NotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response = new
                {
                    mensaje = exception.Message,
                    detalle = _hostEnvironment.IsDevelopment() ? exception.StackTrace : null
                };
            }
            else if (exception is ValidationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response = new
                {
                    mensaje = exception.Message,
                    detalle = _hostEnvironment.IsDevelopment() ? exception.StackTrace : null
                };
            }
            string jsonResponse = JsonSerializer.Serialize(response);
            // Serializar la respuesta a JSON
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}

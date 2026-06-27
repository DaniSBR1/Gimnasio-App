using System.Net;
using System.Text.Json;

namespace GimnasioApp.API.Middleware
{
    /// <summary>
    /// Captura cualquier excepción no controlada (por ejemplo, fallas de conexión
    /// a la base de datos) y devuelve una respuesta JSON en español con código 500,
    /// evitando que la API "caiga" mostrando detalles técnicos al consumidor.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                _logger.LogError(ex, "Error no controlado al procesar la solicitud.");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var respuesta = JsonSerializer.Serialize(new
                {
                    mensaje = "Ocurrió un error inesperado al procesar la solicitud. Intente nuevamente."
                });

                await context.Response.WriteAsync(respuesta);
            }
        }
    }
}

using Newtonsoft.Json;
using System.Net;

namespace BlackEnd.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // ✅ Tratando Erros de Model Binding (Conversão de JSON)
            if (exception is Newtonsoft.Json.JsonSerializationException ||
                exception is FormatException ||
                exception is InvalidCastException)
            {
                var response = new
                {
                    title = "Erro de Conversão de Dados",
                    status = 400,
                    errors = new Dictionary<string, string[]>
                    {
                        { "JsonError", new[] { "O valor enviado é inválido. Verifique o formato." } }
                    }
                };

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }

            // ✅ Tratando Outros Erros Gerais
            var generalResponse = new
            {
                title = "Erro Interno do Servidor",
                status = 500,
                errors = new Dictionary<string, string[]>
                {
                    { "Exception", new[] { exception.Message } }
                }
            };

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(generalResponse));
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Extensoes
{
    public class ExcecaoMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExcecaoMiddleware> _logger;
        public ExcecaoMiddleware(RequestDelegate next, ILogger<ExcecaoMiddleware> logger)
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
            catch (InvalidOperationException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (UnauthorizedAccessException ex) 
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode statusCode)
        {
            _logger.LogError(ex, "Erro tratado no middleware.");

            var resposta = PadraoRespostasApi<object>.CriarResposta<Object>(null, ex.Message, statusCode);

            var jsonResponse = JsonSerializer.Serialize(resposta);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.CrossCutting.Middlewares
{
    /// <summary>
    /// Class that handles exceptions threw by application
    /// </summary>
    public static class GlobalExceptionHandler
    {
        /// <summary>
        /// Method that handle the exceptions caught by ExceptionHandlerMiddleware
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns>A response containing a json object with exception errors. Otherwise, null</returns>
        public static async Task Handle(HttpContext httpContext)
        {
            var exceptionHandlerFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            
            if (exceptionHandlerFeature is null)
                return;

            var (httpStatusCode, message) = exceptionHandlerFeature.Error switch
            {
                NotFoundEntityException ex => (HttpStatusCode.NotFound, ex.Message),
                BadRequestException ex => (HttpStatusCode.BadRequest, ex.Message),               
                UnauthorizedException ex => (HttpStatusCode.Unauthorized, ex.Message),
                ForbiddenException ex => (HttpStatusCode.Forbidden, ex.Message),
                _ => (HttpStatusCode.InternalServerError, "Erro inesperado")
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)httpStatusCode;

            var jsonResponse = new
            {
                httpContext.Response.StatusCode,
                Message = message,
                Erros = exceptionHandlerFeature.Error is BadRequestException 
                    ? GetErrors(exceptionHandlerFeature.Error as BadRequestException) 
                    : new List<string>()
            };

            var jsonSerialised = JsonSerializer.Serialize(jsonResponse);
            await httpContext.Response.WriteAsync(jsonSerialised);
        }

        private static IEnumerable<string> GetErrors(BadRequestException ex)
        {
            return ex.Errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}");
        }
    }
}

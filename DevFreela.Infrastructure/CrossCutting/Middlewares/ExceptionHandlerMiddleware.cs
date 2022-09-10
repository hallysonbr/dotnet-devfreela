using DevFreela.Infrastructure.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace DevFreela.Infrastructure.CrossCutting.Middlewares
{
    public static class ExceptionHandlerMiddleware
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder

                .UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = async context =>
                    {
                        var _contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                        if (_contextFeature == null)
                            return;

                        var _statusCode = _contextFeature.Error is AppException
                        ? ((AppException)_contextFeature.Error).StatusCode
                        : HttpStatusCode.InternalServerError;

                        context.Response.StatusCode = (int)_statusCode;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(
                            new AppError
                            {
                                StatusCode = _statusCode,
                                Message = _contextFeature.Error.Message,
                            }.ToString()
                        );
                    }
                });
        }
    }
}
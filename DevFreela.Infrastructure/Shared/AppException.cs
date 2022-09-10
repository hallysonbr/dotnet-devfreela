using System;
using System.Net;

namespace DevFreela.Infrastructure.Shared
{
    public class AppException : Exception
    {
        public AppException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public AppException(string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public AppException(
            string message,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            Exception innerException = null
        ) : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}
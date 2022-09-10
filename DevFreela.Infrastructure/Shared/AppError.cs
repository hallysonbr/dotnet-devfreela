using System.Net;
using System.Text.Json;

namespace DevFreela.Infrastructure.Shared
{
    public class AppError
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
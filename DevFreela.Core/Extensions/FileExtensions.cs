using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;

namespace DevFreela.Core.Extensions
{
    public static class FileExtensions
    {
        private static readonly FileExtensionContentTypeProvider _provider = new();

        public static string GetContentType(this string fileName)
        {
            if (!_provider.TryGetContentType(fileName, out var contentType))
                contentType = "application/octet-stream";

            return contentType;
        }

        public static string Base64Content(this Stream stream) 
        {
            var ms = new MemoryStream();
            stream.CopyTo(ms);
            var imageBytes = ms.ToArray();

            return Convert.ToBase64String(imageBytes);
        }
    }
}

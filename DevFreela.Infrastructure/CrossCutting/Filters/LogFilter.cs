using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace DevFreela.Infrastructure.CrossCutting.Filters
{
    public class LogFilter : IActionFilter
    {
       private readonly ILogger<LogFilter> _logger;

        public LogFilter(ILogger<LogFilter> logger)
        {
            _logger = logger;
        }

        // Executado antes da Action
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller.GetType().FullName;
            var action = context.RouteData.Values.Where(k => k.Key == "action").Select(v => v.Value).FirstOrDefault();
            var method = context.HttpContext.Request.Method;
            var logger = $"{controller}.{action}[{method}]";

            _logger.LogInformation($"{"".PadRight(20, '#')} {DateTimeOffset.Now} EXECUTANDO: {logger} ###".PadRight(200, '#'));
        }

        // Executado depois da Action
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"{"".PadRight(20, '#')} {DateTimeOffset.Now} EXECUTADO ".PadRight(200, '#'));
        }
    }
}
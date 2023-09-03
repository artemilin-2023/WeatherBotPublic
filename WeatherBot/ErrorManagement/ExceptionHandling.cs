using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherBot.Model;

namespace WeatherBot.ErrorManagement
{
    public class ExceptionHandling
    {
        private readonly RequestDelegate requestDelegate;
        private readonly ILogger<ExceptionHandling> logger;

        public ExceptionHandling(RequestDelegate requestDelegate, ILogger<ExceptionHandling> logger)
        {
            this.requestDelegate = requestDelegate;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}

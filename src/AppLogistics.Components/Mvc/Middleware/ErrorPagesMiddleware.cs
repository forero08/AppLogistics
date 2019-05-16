using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppLogistics.Components.Mvc
{
    public class ErrorPagesMiddleware
    {
        private RequestDelegate Next { get; }
        private readonly ILogger _logger;
        private readonly ILanguages _languages;

        public ErrorPagesMiddleware(RequestDelegate next, ILogger<ErrorPagesMiddleware> logger, ILanguages languages)
        {
            Next = next;
            _logger = logger;
            _languages = languages;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);

                if (!context.Response.HasStarted && context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    View(context, "/home/not-found");
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An unhandled exception has occurred while executing the request.");

                View(context, "/home/error");
            }
        }

        private async void View(HttpContext context, string path)
        {
            string originalPath = context.Request.Path;
            Match abbreviation = Regex.Match(originalPath, "^/(\\w{2})(/|$)");

            try
            {
                if (abbreviation.Success)
                {
                    Language language = _languages[abbreviation.Groups[1].Value];
                    if (language != _languages.Default)
                    {
                        context.Request.Path = $"/{language.Abbreviation}{path}";
                    }
                }
                else
                {
                    context.Request.Path = path;
                }

                await Next(context);
            }
            finally
            {
                context.Request.Path = originalPath;
            }
        }
    }
}

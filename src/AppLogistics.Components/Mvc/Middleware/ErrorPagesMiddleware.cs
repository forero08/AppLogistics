﻿using AppLogistics.Components.Notifications;
using AppLogistics.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AppLogistics.Components.Mvc
{
    public class ErrorPagesMiddleware
    {
        private ILogger Logger { get; }
        private RequestDelegate Next { get; }

        public ErrorPagesMiddleware(RequestDelegate next, ILogger<ErrorPagesMiddleware> logger)
        {
            Next = next;
            Logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "An unhandled exception has occurred while executing the request.");

                if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json; charset=utf-8";

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        alerts = new[]
                        {
                            new Alert
                            {
                                Id = "SystemError",
                                Type = AlertType.Danger,
                                Message = Resource.ForString("SystemError")
                            }
                        }
                    }));
                }
                else
                {
                    Redirect(context, "Error", "Home", new { area = "" });
                }
            }
        }

        private void Redirect(HttpContext context, string action, string controller, object values)
        {
            RouteData route = (context.Features[typeof(IRoutingFeature)] as IRoutingFeature)?.RouteData;
            IUrlHelper url = new UrlHelper(new ActionContext(context, route, new ActionDescriptor()));

            context.Response.Redirect(url.Action(action, controller, values));
        }
    }
}

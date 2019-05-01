﻿using AppLogistics.Components.Extensions;
using AppLogistics.Components.Notifications;
using AppLogistics.Components.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AppLogistics.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public abstract class BaseController : Controller
    {
        public virtual int CurrentAccountId { get; protected set; }
        public IAuthorization Authorization { get; protected set; }
        public Alerts Alerts { get; protected set; }

        protected BaseController()
        {
            Alerts = new Alerts();
        }

        public virtual ViewResult NotFoundView()
        {
            Response.StatusCode = StatusCodes.Status404NotFound;

            return View("~/Views/Home/NotFound.cshtml");
        }

        public virtual ViewResult NotEmptyView(object model)
        {
            if (model == null)
            {
                return NotFoundView();
            }

            return View(model);
        }

        public virtual ActionResult RedirectToLocal(string url)
        {
            if (!Url.IsLocalUrl(url))
            {
                return RedirectToDefault();
            }

            return Redirect(url);
        }

        public virtual RedirectToActionResult RedirectToDefault()
        {
            return base.RedirectToAction("Index", "Home", new { area = "" });
        }

        public override RedirectToActionResult RedirectToAction(string actionName, string controllerName, object routeValues)
        {
            IDictionary<string, object> values = HtmlHelper.AnonymousObjectToHtmlAttributes(routeValues);
            controllerName = controllerName ?? (values.ContainsKey("controller") ? values["controller"] as string : null);
            string area = values.ContainsKey("area") ? values["area"] as string : null;
            controllerName = controllerName ?? RouteData.Values["controller"] as string;
            area = area ?? RouteData.Values["area"] as string;

            if (!IsAuthorizedFor(actionName, controllerName, area))
            {
                return RedirectToDefault();
            }

            return base.RedirectToAction(actionName, controllerName, routeValues);
        }

        public virtual bool IsAuthorizedFor(string action, string controller, string area)
        {
            return Authorization?.IsGrantedFor(CurrentAccountId, area, controller, action) != false;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Authorization = HttpContext.RequestServices.GetService<IAuthorization>();

            CurrentAccountId = User.Id() ?? 0;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is JsonResult)
            {
                return;
            }

            Alerts alerts = Alerts;

            if (TempData["Alerts"] is string alertsJson)
            {
                alerts = JsonConvert.DeserializeObject<Alerts>(alertsJson);
                alerts.Merge(Alerts);
            }

            if (alerts.Count > 0)
            {
                TempData["Alerts"] = JsonConvert.SerializeObject(alerts);
            }
        }
    }
}

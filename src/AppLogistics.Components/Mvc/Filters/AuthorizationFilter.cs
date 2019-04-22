using AppLogistics.Components.Extensions;
using AppLogistics.Components.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace AppLogistics.Components.Mvc
{
    public class AuthorizationFilter : IResourceFilter
    {
        private readonly IAuthorization _authorization;

        public AuthorizationFilter(IAuthorization authorization)
        {
            _authorization = authorization;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            int? accountId = context.HttpContext.User.Id();
            string area = context.RouteData.Values["area"] as string;
            string action = context.RouteData.Values["action"] as string;
            string controller = context.RouteData.Values["controller"] as string;

            if (_authorization?.IsGrantedFor(accountId, area, controller, action) == false)
            {
                context.Result = RedirectToNotFound(context);
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        private IActionResult RedirectToNotFound(ActionContext context)
        {
            RouteValueDictionary route = new RouteValueDictionary();
            route["language"] = context.RouteData.Values["language"];
            route["action"] = "NotFound";
            route["controller"] = "Home";
            route["area"] = "";

            return new RedirectToRouteResult(route);
        }
    }
}

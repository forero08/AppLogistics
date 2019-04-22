﻿using AppLogistics.Components.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using NSubstitute;
using System.Security.Claims;
using Xunit;

namespace AppLogistics.Components.Mvc.Tests
{
    public class AuthorizationFilterTests
    {
        private ResourceExecutingContext context;
        private IAuthorization authorization;
        private AuthorizationFilter filter;

        public AuthorizationFilterTests()
        {
            ActionContext action = new ActionContext(Substitute.For<HttpContext>(), new RouteData(), new ActionDescriptor());
            context = new ResourceExecutingContext(action, new IFilterMetadata[0], new IValueProviderFactory[0]);
            authorization = Substitute.For<IAuthorization>();
            filter = new AuthorizationFilter(authorization);
        }

        #region OnResourceExecuting(ResourceExecutingContext context)

        [Fact]
        public void OnResourceExecuting_NotAuthenticated_SetsNullResult()
        {
            context.HttpContext.User.Identity.IsAuthenticated.Returns(false);

            filter.OnResourceExecuting(context);

            Assert.Null(context.Result);
        }

        [Fact]
        public void OnResourceExecuting_NotAuthorized_RedirectsToNotFound()
        {
            context.HttpContext.User.Identity.IsAuthenticated.Returns(true);
            context.RouteData.Values["language"] = "en";
            context.RouteData.Values["test"] = "Test";

            filter.OnResourceExecuting(context);

            RouteValueDictionary actual = (context.Result as RedirectToRouteResult).RouteValues;

            Assert.Equal("NotFound", actual["action"]);
            Assert.Equal("Home", actual["controller"]);
            Assert.Equal("en", actual["language"]);
            Assert.Equal("", actual["area"]);
            Assert.Equal(4, actual.Count);
        }

        [Fact]
        public void OnResourceExecuting_IsAuthorized_SetsNullResult()
        {
            context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Returns(new Claim(ClaimTypes.NameIdentifier, "11000"));
            context.HttpContext.User.Identity.IsAuthenticated.Returns(true);
            context.RouteData.Values["controller"] = "Controller";
            context.RouteData.Values["action"] = "Action";
            context.RouteData.Values["area"] = "Area";

            authorization.IsGrantedFor(11000, "Area", "Controller", "Action").Returns(true);

            filter.OnResourceExecuting(context);

            Assert.Null(context.Result);
        }

        #endregion
    }
}

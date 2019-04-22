﻿using AppLogistics.Components.Security;
using AppLogistics.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLogistics.Controllers
{
    [AllowUnauthorized]
    public class HomeController : ServicedController<IAccountService>
    {
        public HomeController(IAccountService service)
            : base(service)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (!Service.IsActive(CurrentAccountId))
            {
                return RedirectToAction("Logout", "Auth");
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Error()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[controller]/not-found")]
        public new ActionResult NotFound()
        {
            if (Service.IsLoggedIn(User) && !Service.IsActive(CurrentAccountId))
            {
                return RedirectToAction("Logout", "Auth");
            }

            Response.StatusCode = StatusCodes.Status404NotFound;

            return View();
        }
    }
}

﻿using AppLogistics.Components.Mvc;
using AppLogistics.Objects;
using AppLogistics.Services;
using AppLogistics.Validators;
using Microsoft.AspNetCore.Mvc;

namespace AppLogistics.Controllers.Administration
{
    [Area("Administration")]
    public class RolesController : ValidatedController<IRoleValidator, IRoleService>
    {
        public RolesController(IRoleValidator validator, IRoleService service)
            : base(validator, service)
        {
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(Service.GetViews());
        }

        [HttpGet]
        public ViewResult Create()
        {
            RoleView role = new RoleView();
            Service.SeedPermissions(role);

            return View(role);
        }

        [HttpPost]
        public ActionResult Create([BindExcludeId] RoleView role)
        {
            if (!Validator.CanCreate(role))
            {
                Service.SeedPermissions(role);

                return View(role);
            }

            Service.Create(role);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return NotEmptyView(Service.GetView(id));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return NotEmptyView(Service.GetView(id));
        }

        [HttpPost]
        public ActionResult Edit(RoleView role)
        {
            if (!Validator.CanEdit(role))
            {
                Service.SeedPermissions(role);

                return View(role);
            }

            Service.Edit(role);

            Authorization?.Refresh();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return NotEmptyView(Service.GetView(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        public RedirectToActionResult DeleteConfirmed(int id)
        {
            Service.Delete(id);

            Authorization?.Refresh();

            return RedirectToAction("Index");
        }
    }
}

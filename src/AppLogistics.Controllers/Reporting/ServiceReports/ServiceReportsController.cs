using AppLogistics.Objects;
using AppLogistics.Services;
using AppLogistics.Validators;
using Microsoft.AspNetCore.Mvc;

namespace AppLogistics.Controllers.Reporting
{
    [Area("Reporting")]
    public class ServiceReportsController : ValidatedController<IServiceReportValidator, IServiceReportService>
    {
        public ServiceReportsController(IServiceReportValidator validator, IServiceReportService service)
            : base(validator, service)
        {
        }

        [HttpGet]
        public ViewResult Query()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Query(ServiceReportQueryView serviceReportQuery)
        {
            if (!Validator.CanQuery(serviceReportQuery))
            {
                return View(serviceReportQuery);
            }

            return RedirectToAction("QueryResult", "ServiceReports", serviceReportQuery);
        }

        [HttpGet]
        public ActionResult QueryResult(ServiceReportQueryView query)
        {
            if (!Validator.CanQuery(query))
            {
                return View(query);
            }

            return NotEmptyView(Service.FilterByQuery(query));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return NotEmptyView(Service.GetDetail(id));
        }
    }
}

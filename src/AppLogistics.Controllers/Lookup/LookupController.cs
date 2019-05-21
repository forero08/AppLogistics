using AppLogistics.Components.Lookups;
using AppLogistics.Components.Mvc;
using AppLogistics.Components.Security;
using AppLogistics.Data.Core;
using AppLogistics.Objects;
using Microsoft.AspNetCore.Mvc;
using NonFactors.Mvc.Lookup;

namespace AppLogistics.Controllers
{
    [AllowUnauthorized]
    public class LookupController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public LookupController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [NonAction]
        public virtual JsonResult GetData(MvcLookup lookup, LookupFilter filter)
        {
            lookup.Filter = filter;

            return Json(lookup.GetData());
        }

        [AjaxOnly]
        public JsonResult Afp(LookupFilter filter)
        {
            return GetData(new MvcLookup<Afp, AfpView>(_unitOfWork), filter);
        }

        [AjaxOnly]
        public JsonResult BranchOffice(LookupFilter filter)
        {
            return GetData(new MvcLookup<BranchOffice, BranchOfficeView>(_unitOfWork), filter);
        }

        [AjaxOnly]
        public JsonResult DocumentType(LookupFilter filter)
        {
            return GetData(new MvcLookup<DocumentType, DocumentTypeView>(_unitOfWork), filter);
        }

        [AjaxOnly]
        public JsonResult Eps(LookupFilter filter)
        {
            return GetData(new MvcLookup<Eps, EpsView>(_unitOfWork), filter);
        }

        [AjaxOnly]
        public JsonResult MaritalStatus(LookupFilter filter)
        {
            return GetData(new MvcLookup<MaritalStatus, MaritalStatusView>(_unitOfWork), filter);
        }

        [AjaxOnly]
        public JsonResult Role(LookupFilter filter)
        {
            return GetData(new MvcLookup<Role, RoleView>(_unitOfWork), filter);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();

            base.Dispose(disposing);
        }
    }
}

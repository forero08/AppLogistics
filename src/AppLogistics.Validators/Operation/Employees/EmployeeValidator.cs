using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;
using System.Linq;

namespace AppLogistics.Validators
{
    public class EmployeeValidator : BaseValidator, IEmployeeValidator
    {
        public EmployeeValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(EmployeeCreateEditView view)
        {
            return IsUniqueDocumentNumber(view.Id, view.DocumentNumber) && ModelState.IsValid;
        }

        public bool CanEdit(EmployeeCreateEditView view)
        {
            return IsUniqueDocumentNumber(view.Id, view.DocumentNumber) && ModelState.IsValid;
        }

        private bool IsUniqueDocumentNumber(int employeeId, string docNumber)
        {
            var alreadyExists = UnitOfWork.Select<Employee>()
                .Where(e => e.DocumentNumber.Equals(docNumber) && e.Id != employeeId)
                .Any();

            if (alreadyExists)
            {
                Alerts.AddError(Validation.For<EmployeeCreateEditView>("NotUniqueDocumentNumber"));
                return false;
            }

            return true;
        }
    }
}

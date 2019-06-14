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
            return IsUniqueDocumentNumber(view.DocumentNumber) && ModelState.IsValid;
        }

        public bool CanEdit(EmployeeCreateEditView view)
        {
            return IsUniqueDocumentNumber(view.DocumentNumber) && ModelState.IsValid;
        }

        private bool IsUniqueDocumentNumber(string docNumber)
        {
            var alreadyExists = UnitOfWork.Select<Employee>()
                .Where(c => c.DocumentNumber.Equals(docNumber))
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

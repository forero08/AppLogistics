using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;
using System.Linq;

namespace AppLogistics.Validators
{
    public class MaritalStatusValidator : BaseValidator, IMaritalStatusValidator
    {
        public MaritalStatusValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(MaritalStatusView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(MaritalStatusView view)
        {
            return ModelState.IsValid;
        }

        public bool CanDelete(int id)
        {
            var hasReferencedEmployees = UnitOfWork.Select<Employee>()
                .Where(c => c.MaritalStatusId.Equals(id))
                .Any();

            if (hasReferencedEmployees)
            {
                Alerts.AddError(Validation.For<MaritalStatusView>("AssociatedEmployees"));
                return false;
            }

            return ModelState.IsValid;
        }
    }
}

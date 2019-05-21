using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;
using System.Linq;

namespace AppLogistics.Validators
{
    public class AfpValidator : BaseValidator, IAfpValidator
    {
        public AfpValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(AfpView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(AfpView view)
        {
            return ModelState.IsValid;
        }

        public bool CanDelete(int id)
        {
            var hasReferencedEmployees = UnitOfWork.Select<Employee>()
                .Where(c => c.AfpId.Equals(id))
                .Any();

            if (hasReferencedEmployees)
            {
                Alerts.AddError(Validation.For<AfpView>("AssociatedEmployees"));
                return false;
            }

            return ModelState.IsValid;
        }
    }
}

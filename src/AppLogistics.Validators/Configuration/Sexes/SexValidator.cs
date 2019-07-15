using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;
using System.Linq;

namespace AppLogistics.Validators
{
    public class SexValidator : BaseValidator, ISexValidator
    {
        public SexValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(SexView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(SexView view)
        {
            return ModelState.IsValid;
        }

        public bool CanDelete(int id)
        {
            var hasReferencedEmployees = UnitOfWork.Select<Employee>()
                .Where(c => c.SexId.Equals(id))
                .Any();

            if (hasReferencedEmployees)
            {
                Alerts.AddError(Validation.For<SexView>("AssociatedEmployees"));
                return false;
            }

            return ModelState.IsValid;
        }
    }
}

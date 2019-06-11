using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;
using System.Linq;

namespace AppLogistics.Validators
{
    public class EducationLevelValidator : BaseValidator, IEducationLevelValidator
    {
        public EducationLevelValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(EducationLevelView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(EducationLevelView view)
        {
            return ModelState.IsValid;
        }

        public bool CanDelete(int id)
        {
            var hasReferencedEmployees = UnitOfWork.Select<Employee>()
                .Where(c => c.EducationLevelId.Equals(id))
                .Any();

            if (hasReferencedEmployees)
            {
                Alerts.AddError(Validation.For<EducationLevelView>("AssociatedEmployees"));
                return false;
            }

            return ModelState.IsValid;
        }
    }
}

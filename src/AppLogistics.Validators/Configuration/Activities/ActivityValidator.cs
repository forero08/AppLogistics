using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;
using System.Linq;

namespace AppLogistics.Validators
{
    public class ActivityValidator : BaseValidator, IActivityValidator
    {
        public ActivityValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(ActivityView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(ActivityView view)
        {
            return ModelState.IsValid;
        }

        public bool CanDelete(int id)
        {
            var hasReferencedRates = UnitOfWork.Select<Rate>()
                .Where(c => c.ActivityId.Equals(id))
                .Any();

            if (hasReferencedRates)
            {
                Alerts.AddError(Validation.For<ActivityView>("AssociatedRates"));
                return false;
            }

            return ModelState.IsValid;
        }
    }
}

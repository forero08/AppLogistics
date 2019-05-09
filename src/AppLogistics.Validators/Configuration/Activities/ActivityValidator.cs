using AppLogistics.Data.Core;
using AppLogistics.Objects;

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
    }
}

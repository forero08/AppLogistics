using AppLogistics.Data.Core;
using AppLogistics.Objects;

namespace AppLogistics.Validators
{
    public class RateValidator : BaseValidator, IRateValidator
    {
        public RateValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(RateCreateEditView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(RateCreateEditView view)
        {
            return ModelState.IsValid;
        }
    }
}

using AppLogistics.Data.Core;
using AppLogistics.Objects;

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
    }
}

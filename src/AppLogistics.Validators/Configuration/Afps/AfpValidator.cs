using AppLogistics.Data.Core;
using AppLogistics.Objects;

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
    }
}

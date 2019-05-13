using AppLogistics.Data.Core;
using AppLogistics.Objects;

namespace AppLogistics.Validators
{
    public class CarrierValidator : BaseValidator, ICarrierValidator
    {
        public CarrierValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(CarrierView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(CarrierView view)
        {
            return ModelState.IsValid;
        }
    }
}

using AppLogistics.Data.Core;
using AppLogistics.Objects;

namespace AppLogistics.Validators
{
    public class VehicleTypeValidator : BaseValidator, IVehicleTypeValidator
    {
        public VehicleTypeValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(VehicleTypeView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(VehicleTypeView view)
        {
            return ModelState.IsValid;
        }
    }
}

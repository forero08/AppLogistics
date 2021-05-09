using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;
using System.Linq;

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

        public bool CanDelete(int id)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            var hasReferencedRates = UnitOfWork.Select<Rate>()
                .Where(c => c.VehicleTypeId.Equals(id))
                .Any();

            if (hasReferencedRates)
            {
                Alerts.AddError(Validation.For<VehicleTypeView>("AssociatedRates"));
                return false;
            }

            var hasReferencedServices = UnitOfWork.Select<Service>()
                .Where(c => c.VehicleTypeId.Equals(id))
                .Any();

            if (hasReferencedServices)
            {
                Alerts.AddError(Validation.For<VehicleTypeView>("AssociatedServices"));
                return false;
            }

            return true;
        }
    }
}

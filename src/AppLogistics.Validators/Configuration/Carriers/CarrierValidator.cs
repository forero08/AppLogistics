using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;
using System.Linq;

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

        public bool CanDelete(int id)
        {
            var hasReferencedServices = UnitOfWork.Select<Service>()
                .Where(c => c.CarrierId.Equals(id))
                .Any();

            if (hasReferencedServices)
            {
                Alerts.AddError(Validation.For<CarrierView>("AssociatedServices"));
                return false;
            }

            return ModelState.IsValid;
        }
    }
}

using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;
using System.Linq;

namespace AppLogistics.Validators
{
    public class ClientValidator : BaseValidator, IClientValidator
    {
        public ClientValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(ClientCreateEditView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(ClientCreateEditView view)
        {
            return ModelState.IsValid;
        }

        public bool CanDelete(int id)
        {
            var hasReferencedRates = UnitOfWork.Select<Rate>()
                .Where(c => c.ClientId.Equals(id))
                .Any();

            if (hasReferencedRates)
            {
                Alerts.AddError(Validation.For<ClientView>("AssociatedRates"));
                return false;
            }

            return ModelState.IsValid;
        }
    }
}

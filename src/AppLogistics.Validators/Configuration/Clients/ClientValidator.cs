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
            return IsUniqueNit(view.Nit) && ModelState.IsValid;
        }

        public bool CanEdit(ClientCreateEditView view)
        {
            return IsUniqueNit(view.Nit) && ModelState.IsValid;
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

        private bool IsUniqueNit(string nit)
        {
            var alreadyExists = UnitOfWork.Select<Client>()
                .Where(c => c.Nit.Equals(nit))
                .Any();

            if (alreadyExists)
            {
                Alerts.AddError(Validation.For<ClientCreateEditView>("NotUniqueNit"));
                return false;
            }

            return true;
        }
    }
}

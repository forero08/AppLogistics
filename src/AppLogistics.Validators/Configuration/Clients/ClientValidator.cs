using AppLogistics.Data.Core;
using AppLogistics.Objects;

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
    }
}

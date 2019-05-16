using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;
using System.Linq;

namespace AppLogistics.Validators
{
    public class BranchOfficeValidator : BaseValidator, IBranchOfficeValidator
    {
        public BranchOfficeValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(BranchOfficeView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(BranchOfficeView view)
        {
            return ModelState.IsValid;
        }

        public bool CanDelete(int id)
        {
            var hasReferencedClients = UnitOfWork.Select<Client>()
                .Where(c => c.BranchOfficeId.Equals(id))
                .Any();

            if (hasReferencedClients)
            {
                Alerts.AddError(Validation.For<BranchOfficeView>("AssociatedClients"));
                return false;
            }

            return ModelState.IsValid;
        }
    }
}

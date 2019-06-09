using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;
using System.Linq;

namespace AppLogistics.Validators
{
    public class EthnicGroupValidator : BaseValidator, IEthnicGroupValidator
    {
        public EthnicGroupValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(EthnicGroupView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(EthnicGroupView view)
        {
            return ModelState.IsValid;
        }

        public bool CanDelete(int id)
        {
            var hasReferencedEmployees = UnitOfWork.Select<Employee>()
                .Where(c => c.EthnicGroupId.Equals(id))
                .Any();

            if (hasReferencedEmployees)
            {
                Alerts.AddError(Validation.For<EthnicGroupView>("AssociatedEmployees"));
                return false;
            }

            return ModelState.IsValid;
        }
    }
}

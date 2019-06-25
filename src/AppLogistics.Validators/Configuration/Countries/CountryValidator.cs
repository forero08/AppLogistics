using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;
using System.Linq;

namespace AppLogistics.Validators
{
    public class CountryValidator : BaseValidator, ICountryValidator
    {
        public CountryValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(CountryView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(CountryView view)
        {
            return ModelState.IsValid;
        }

        public bool CanDelete(int id)
        {
            var hasReferencedEmployees = UnitOfWork.Select<Employee>()
                .Where(c => c.CountryId.Equals(id))
                .Any();

            if (hasReferencedEmployees)
            {
                Alerts.AddError(Validation.For<CountryView>("AssociatedEmployees"));
                return false;
            }

            return ModelState.IsValid;
        }
    }
}

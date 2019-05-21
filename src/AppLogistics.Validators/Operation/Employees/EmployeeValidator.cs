using AppLogistics.Data.Core;
using AppLogistics.Objects;

namespace AppLogistics.Validators
{
    public class EmployeeValidator : BaseValidator, IEmployeeValidator
    {
        public EmployeeValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(EmployeeCreateEditView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(EmployeeCreateEditView view)
        {
            return ModelState.IsValid;
        }
    }
}

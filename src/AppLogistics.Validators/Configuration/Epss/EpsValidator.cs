using AppLogistics.Data.Core;
using AppLogistics.Objects;
using System;

namespace AppLogistics.Validators
{
    public class EpsValidator : BaseValidator, IEpsValidator
    {
        public EpsValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(EpsView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(EpsView view)
        {
            return ModelState.IsValid;
        }
    }
}

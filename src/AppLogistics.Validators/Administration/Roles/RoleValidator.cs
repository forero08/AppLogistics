using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace AppLogistics.Validators
{
    public class RoleValidator : BaseValidator, IRoleValidator
    {
        public RoleValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(RoleView view)
        {
            bool isValid = ModelState.IsValid;
            isValid &= IsUniqueTitle(view);

            return isValid;
        }

        public bool CanEdit(RoleView view)
        {
            bool isValid = ModelState.IsValid;
            isValid &= IsUniqueTitle(view);

            return isValid;
        }

        private bool IsUniqueTitle(RoleView view)
        {
            bool isUnique = !UnitOfWork
                .Select<Role>()
                .Any(role =>
                    role.Id != view.Id
                    && string.Equals(role.Title, view.Title ?? "", StringComparison.OrdinalIgnoreCase));

            if (!isUnique)
            {
                ModelState.AddModelError<RoleView>(role => role.Title,
                    Validation.For<RoleView>("UniqueTitle"));
            }

            return isUnique;
        }
    }
}

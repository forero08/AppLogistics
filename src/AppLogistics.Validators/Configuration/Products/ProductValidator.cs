using AppLogistics.Data.Core;
using AppLogistics.Objects;

namespace AppLogistics.Validators
{
    public class ProductValidator : BaseValidator, IProductValidator
    {
        public ProductValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(ProductView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(ProductView view)
        {
            return ModelState.IsValid;
        }
    }
}

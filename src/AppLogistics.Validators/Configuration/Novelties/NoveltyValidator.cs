using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;
using System.Linq;

namespace AppLogistics.Validators
{
    public class NoveltyValidator : BaseValidator, INoveltyValidator
    {
        public NoveltyValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(NoveltyView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(NoveltyView view)
        {
            return ModelState.IsValid;
        }

        public bool CanDelete(int id)
        {
            var hasReferencedServices = UnitOfWork.Select<ServiceNovelty>()
               .Where(c => c.NoveltyId == id)
               .Any();

            if (hasReferencedServices)
            {
                Alerts.AddError(Validation.For<NoveltyView>("AssociatedServices"));
                return false;
            }

            return ModelState.IsValid;
        }
    }
}

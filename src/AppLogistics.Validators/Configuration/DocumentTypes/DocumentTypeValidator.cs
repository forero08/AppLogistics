using AppLogistics.Data.Core;
using AppLogistics.Objects;

namespace AppLogistics.Validators
{
    public class DocumentTypeValidator : BaseValidator, IDocumentTypeValidator
    {
        public DocumentTypeValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool CanCreate(DocumentTypeView view)
        {
            return ModelState.IsValid;
        }

        public bool CanEdit(DocumentTypeView view)
        {
            return ModelState.IsValid;
        }
    }
}

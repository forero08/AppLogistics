using AppLogistics.Objects;
using System;

namespace AppLogistics.Validators
{
    public interface IDocumentTypeValidator : IValidator
    {
        bool CanCreate(DocumentTypeView view);
        bool CanEdit(DocumentTypeView view);
    }
}

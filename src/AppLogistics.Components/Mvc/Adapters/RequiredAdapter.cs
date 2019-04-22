using AppLogistics.Resources;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Components.Mvc
{
    public class RequiredAdapter : RequiredAttributeAdapter
    {
        public RequiredAdapter(RequiredAttribute attribute)
            : base(attribute, null)
        {
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            Attribute.ErrorMessage = Validation.For("Required");

            return GetErrorMessage(validationContext.ModelMetadata);
        }
    }
}

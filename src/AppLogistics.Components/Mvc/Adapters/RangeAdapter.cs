using AppLogistics.Resources;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Components.Mvc
{
    public class RangeAdapter : RangeAttributeAdapter
    {
        public RangeAdapter(RangeAttribute attribute)
            : base(attribute, null)
        {
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            Attribute.ErrorMessage = Validation.For("Range");

            return GetErrorMessage(validationContext.ModelMetadata);
        }
    }
}

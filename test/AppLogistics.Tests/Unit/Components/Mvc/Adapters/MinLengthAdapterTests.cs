using AppLogistics.Resources;
using AppLogistics.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace AppLogistics.Components.Mvc.Tests
{
    public class MinLengthAdapterTests
    {
        #region GetErrorMessage(ModelValidationContextBase context)

        [Fact]
        public void GetErrorMessage_MinLength()
        {
            IModelMetadataProvider provider = new EmptyModelMetadataProvider();
            MinLengthAdapter adapter = new MinLengthAdapter(new MinLengthAttribute(128));
            ModelMetadata metadata = provider.GetMetadataForProperty(typeof(AllTypesView), "StringField");
            ModelValidationContextBase context = new ModelValidationContextBase(new ActionContext(), metadata, provider);

            string expected = Validation.For("MinLength", context.ModelMetadata.PropertyName, 128);
            string actual = adapter.GetErrorMessage(context);

            Assert.Equal(Validation.For("MinLength"), adapter.Attribute.ErrorMessage);
            Assert.Equal(expected, actual);
        }

        #endregion GetErrorMessage(ModelValidationContextBase context)
    }
}

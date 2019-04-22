using AppLogistics.Resources;
using AppLogistics.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace AppLogistics.Components.Mvc.Tests
{
    public class RequiredAdapterTests
    {
        #region GetErrorMessage(ModelValidationContextBase context)

        [Fact]
        public void GetErrorMessage_Required()
        {
            IModelMetadataProvider provider = new EmptyModelMetadataProvider();
            RequiredAdapter adapter = new RequiredAdapter(new RequiredAttribute());
            ModelMetadata metadata = provider.GetMetadataForProperty(typeof(AllTypesView), "StringField");
            ModelValidationContextBase context = new ModelValidationContextBase(new ActionContext(), metadata, provider);

            string expected = Validation.For("Required", context.ModelMetadata.PropertyName);
            string actual = adapter.GetErrorMessage(context);

            Assert.Equal(Validation.For("Required"), adapter.Attribute.ErrorMessage);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}

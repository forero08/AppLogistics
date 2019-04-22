using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using NSubstitute;
using Xunit;

namespace AppLogistics.Components.Mvc.Tests
{
    public class BindExcludeIdAttributeTests
    {
        #region PropertyFilter

        [Theory]
        [InlineData("id", true)]
        [InlineData("iD", true)]
        [InlineData("ID", true)]
        [InlineData("Id", false)]
        [InlineData("Prop", true)]
        public void PropertyFilter_Id(string property, bool isIncluded)
        {
            ModelMetadataIdentity identity = ModelMetadataIdentity.ForProperty(typeof(object), property, typeof(object));
            ModelMetadata metadata = Substitute.ForPartsOf<ModelMetadata>(identity);

            bool actual = new BindExcludeIdAttribute().PropertyFilter(metadata);
            bool expected = isIncluded;

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}

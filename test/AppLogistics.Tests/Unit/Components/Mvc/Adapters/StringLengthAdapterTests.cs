﻿using AppLogistics.Resources;
using AppLogistics.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace AppLogistics.Components.Mvc.Tests
{
    public class StringLengthAdapterTests
    {
        private StringLengthAdapter adapter;
        private ModelValidationContextBase context;

        public StringLengthAdapterTests()
        {
            adapter = new StringLengthAdapter(new StringLengthAttribute(128));
            IModelMetadataProvider provider = new EmptyModelMetadataProvider();
            ModelMetadata metadata = provider.GetMetadataForProperty(typeof(AllTypesView), "StringField");

            context = new ModelValidationContextBase(new ActionContext(), metadata, provider);
        }

        #region GetErrorMessage(ModelValidationContextBase context)

        [Fact]
        public void GetErrorMessage_StringLength()
        {
            adapter.Attribute.MinimumLength = 0;

            string expected = Validation.For("StringLength", context.ModelMetadata.PropertyName, 128);
            string actual = adapter.GetErrorMessage(context);

            Assert.Equal(Validation.For("StringLength"), adapter.Attribute.ErrorMessage);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetErrorMessage_StringLengthRange()
        {
            adapter.Attribute.MinimumLength = 4;

            string expected = Validation.For("StringLengthRange", context.ModelMetadata.PropertyName, 128, 4);
            string actual = adapter.GetErrorMessage(context);

            Assert.Equal(Validation.For("StringLengthRange"), adapter.Attribute.ErrorMessage);
            Assert.Equal(expected, actual);
        }

        #endregion GetErrorMessage(ModelValidationContextBase context)
    }
}

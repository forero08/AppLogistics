﻿using AppLogistics.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using Xunit;

namespace AppLogistics.Components.Mvc.Tests
{
    public class DateValidatorTests
    {
        #region AddValidation(ClientModelValidationContext context)

        [Fact]
        public void AddValidation_Date()
        {
            IModelMetadataProvider provider = new EmptyModelMetadataProvider();
            ModelMetadata metadata = provider.GetMetadataForType(typeof(DateTime));
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            ClientModelValidationContext context = new ClientModelValidationContext(new ActionContext(), metadata, provider, attributes);

            new DateValidator().AddValidation(context);

            Assert.Equal(2, attributes.Count);
            Assert.Equal("true", attributes["data-val"]);
            Assert.Equal(Validation.For("Date", "DateTime"), attributes["data-val-date"]);
        }

        #endregion
    }
}

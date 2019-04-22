﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace AppLogistics.Components.Mvc
{
    public class TrimmingModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            bindingContext.Result = BindModel(bindingContext);

            return Task.CompletedTask;
        }

        private ModelBindingResult BindModel(ModelBindingContext context)
        {
            ValueProviderResult result = context.ValueProvider.GetValue(context.ModelName);
            if (result == ValueProviderResult.None)
            {
                return context.Result;
            }

            string value = result.FirstValue.Trim();
            context.ModelState.SetModelValue(context.ModelName, result);

            if (value.Length == 0 && context.ModelMetadata.ConvertEmptyStringToNull)
            {
                return ModelBindingResult.Success(null);
            }

            return ModelBindingResult.Success(value);
        }
    }
}

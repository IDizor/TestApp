using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TestApp.Attributes
{
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelMetadata.IsEnumerableType)
            {
                var key = bindingContext.ModelName;
                var value = bindingContext.ValueProvider.GetValue(key).ToString();

                if (!string.IsNullOrWhiteSpace(value))
                {
                    var elementType = bindingContext.ModelType;

                    if (elementType == typeof(string[]))
                    {
                        bindingContext.Model = value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => x.Trim())
                            .ToArray();
                    }
                    else
                    {
                        var converter = TypeDescriptor.GetConverter(elementType);

                        var values = value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => converter.ConvertFromString(x.Trim()))
                            .ToArray();

                        var typedValues = Array.CreateInstance(elementType, values.Length);

                        values.CopyTo(typedValues, 0);

                        bindingContext.Model = typedValues;
                    }

                    bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
                }
                else
                {
                    // change this line to null if you prefer nulls to empty arrays 
                    bindingContext.Model = Array.CreateInstance(bindingContext.ModelType.GetElementType(), 0);
                }
            }

            return Task.FromResult(bindingContext.Result);
        }
    }
}

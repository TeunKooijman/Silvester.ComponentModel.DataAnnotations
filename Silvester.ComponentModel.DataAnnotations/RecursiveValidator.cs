using Microsoft.Extensions.Options;
using Silvester.ComponentModel.DataAnnotations.DependencyInjection;
using Silvester.ComponentModel.DataAnnotations.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Silvester.ComponentModel.DataAnnotations
{
    public interface IRecursiveValidator
    {   
        /**
         * <summary>Validates an instance of a class recursively. Here, recursively means any non-simple types, </summary>
         */
        List<ValidationResult> ValidateObjectRecursively(object instance, bool validateAllProperties = true);
    }

    public class RecursiveValidator : IRecursiveValidator
    {
        private IOptions<RecursiveValidatorOptions> Options { get; }
        private List<ValidationResult> Results { get; set; }
        private List<object> ProcessedInstances { get; set; }
        private bool ValidateAllProperties { get; set; }

        public RecursiveValidator(IOptions<RecursiveValidatorOptions> options)
        {
            Options = options;
        }

        public List<ValidationResult> ValidateObjectRecursively(object instance, bool validateAllProperties = true)
        {
            Results = new List<ValidationResult>();
            ProcessedInstances = new List<object>();
            ValidateAllProperties = validateAllProperties;

            ValidateObjectRecursivelyInternal(instance, null);

            return Results;
        }

        private void ValidateObjectRecursivelyInternal(object instance, string path)
        {
            if (instance == null)
            {
                return;
            }

            //Guard for infinite loops due to a cyclical object graph.
            if (ProcessedInstances.Contains(instance))
            {
                return;
            }
            ProcessedInstances.Add(instance);

            Type type = instance.GetType();
            if (type.IsSimpleType())
            {
                return;
            }

            Validate(instance, path);

            foreach (PropertyInfo property in type.GetProperties().Where(e => e.CanRead))
            {
                if (property.PropertyType.IsSimpleType())
                {
                    continue;
                }
                else if (IsDictionaryWithSimpleKeys(property))
                {
                    ValidateDictionaryRecursively(property.GetValue(instance) as IDictionary, path, property);
                }
                else if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    ValidateEnumerableRecursively(property.GetValue(instance) as IEnumerable, path, property);
                }
                else
                {
                    ValidateObjectRecursivelyInternal(property.GetValue(instance), CreatePropertyPath(path, property.Name));
                }
            }
        }

        private void Validate(object instance, string path)
        {
            List<ValidationResult> currentResults = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(instance);
            Validator.TryValidateObject(instance, context, currentResults, ValidateAllProperties);

            foreach (ValidationResult result in currentResults)
            {
                Results.Add(new ValidationResult(result.ErrorMessage, result.MemberNames.Select(e => CreatePropertyPath(path, e))));
            }
        }

        private static bool IsDictionaryWithSimpleKeys(PropertyInfo property)
        {
            return property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>) &&
                property.PropertyType.GetGenericArguments()[0].IsSimpleType();
        }

        private void ValidateDictionaryRecursively(IDictionary dictionary, string path, PropertyInfo property)
        {
            if (dictionary == null)
            {
                return;
            }

            foreach (object key in dictionary.Keys)
            {
                ValidateObjectRecursivelyInternal(dictionary[key], CreatePropertyPath(path, property.Name, key.ToString()));
            }
        }

        private void ValidateEnumerableRecursively(IEnumerable enumerable, string path, PropertyInfo property)
        {
            if(enumerable == null)
            {
                return;
            }

            int index = 0;
            foreach (object member in enumerable)
            {
                ValidateObjectRecursivelyInternal(member, CreatePropertyPath(path, $"{property.Name}[{index}]"));
                index += 1;
            }
        }

        private string ApplyNamingPolicy(string name)
        {
            return Options?.Value?.ValidationNamingPolicy?.ConvertName(name) ?? name;
        }

        private string CreatePropertyPath(params string[] segments)
        {
            return string.Join(".", segments.Where(e => e != null).Select(e => ApplyNamingPolicy(e)));
        }
    }
}
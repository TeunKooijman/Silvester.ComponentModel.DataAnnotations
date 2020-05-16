using Microsoft.Extensions.DependencyInjection;
using Silvester.ComponentModel.DataAnnotations.DependencyInjection;
using Silvester.ComponentModel.DataAnnotations.DependencyInjection.Options;
using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures;
using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests
{
    public class GivenNamingPolicy
    {
        [Fact]
        public void WhenValidateRecursively_ThenNamingPolicyIsReflectedInPath()
        {
            IServiceProvider services = new ServiceCollection().AddRecursiveValidator(options => { options.ValidationNamingPolicy = new NaiveCamelCasingPolicy(); }).BuildServiceProvider();
            IRecursiveValidator validator = services.GetRequiredService<IRecursiveValidator>();
            List<ValidationResult> results = validator.ValidateObjectRecursively(NestedObject.WithInvalidChild());

            Assert.Equal(3, results.Count);
            ValidationResultUtility.AssertValidationResultEquals(results[0], "The RequiredString field is required.", "child.requiredString");
            ValidationResultUtility.AssertValidationResultEquals(results[1], "The RequiredLong field is required.", "child.requiredLong");
            ValidationResultUtility.AssertValidationResultEquals(results[2], "The RequiredUnsignedLong field is required.", "child.requiredUnsignedLong");
        }
    }

    public class NaiveCamelCasingPolicy : IValidationNamingPolicy
    {
        public string ConvertName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }

            return name.Substring(0, 1).ToLower() + name.Substring(1);
        }
    }
}

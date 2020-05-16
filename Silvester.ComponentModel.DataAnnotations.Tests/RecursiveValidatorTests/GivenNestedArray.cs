using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures;
using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestUtilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests
{
    public class GivenNestedArray
    {
        [Fact]
        public void GivenValidNestedStructure_WhenValidateRecursively_ThenCorrectValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(NestedArray.Valid());
            Assert.Empty(results);
        }

        [Fact]
        public void GivenInvalidChild_WhenValidateRecursively_ThenCorrectValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(NestedArray.WithInvalidChild());

            Assert.Equal(3, results.Count);
            ValidationResultUtility.AssertValidationResultEquals(results[0], "The RequiredString field is required.", "Child[0].RequiredString");
            ValidationResultUtility.AssertValidationResultEquals(results[1], "The RequiredLong field is required.", "Child[0].RequiredLong");
            ValidationResultUtility.AssertValidationResultEquals(results[2], "The RequiredUnsignedLong field is required.", "Child[0].RequiredUnsignedLong");
        }
    }
}

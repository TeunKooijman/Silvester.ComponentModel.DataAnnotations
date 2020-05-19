using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures;
using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestUtilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests
{
    public class GivenGenericType
    {
        [Fact]
        public void GivenValidGenericType_WhenValidateRecursively_ThenCorrectValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(GenericType.Valid());
            Assert.Empty(results);
        }

        [Fact]
        public void GivenInvalidGenericType_WithMissingChild_ThenCorrectValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(GenericType.WithMissingChild());

            Assert.Single(results);
            ValidationResultUtility.AssertValidationResultEquals(results[0], "The Child field is required.", "Child");
        }

        [Fact]
        public void GivenInvalidGenericType_WithMissingChildValue_ThenCorrectValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(GenericType.WithMissingChildValue());

            Assert.Single(results);
            ValidationResultUtility.AssertValidationResultEquals(results[0], "The Value field is required.", "Child.Value");
        }
    }
}

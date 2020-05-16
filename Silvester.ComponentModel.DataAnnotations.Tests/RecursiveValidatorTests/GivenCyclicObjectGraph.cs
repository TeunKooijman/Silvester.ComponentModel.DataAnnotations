using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures;
using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestUtilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests
{
    public class GivenCyclicObjectGraph
    {
        [Fact]
        public void GivenValidCyclicObjectGraph_WhenValidateRecursively_ThenCorrectValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(CyclicObjectGraph.Valid());
            Assert.Empty(results);
        }

        [Fact]
        public void GivenInvalidChild_WhenValidateRecursively_ThenCorrectValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(CyclicObjectGraph.WithInvalidChild());

            Assert.Single(results);
            ValidationResultUtility.AssertValidationResultEquals(results[0], "The RequiredUnsignedLong field is required.", "Child.RequiredUnsignedLong");
        }

        [Fact]
        public void GivenInvalidParent_WhenValidateRecursively_ThenCorrectValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(CyclicObjectGraph.WithInvalidParent());

            Assert.Single(results);
            ValidationResultUtility.AssertValidationResultEquals(results[0], "The RequiredString field is required.", "RequiredString");
        }
    }
}

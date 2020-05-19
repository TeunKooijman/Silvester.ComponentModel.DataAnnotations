using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures;
using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestUtilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests
{
    public class GivenFunc
    {
        [Fact]
        public void GivenValidFuncs_WhenValidateRecursively_ThenCorrectValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(Funcs.Valid());
            Assert.Empty(results);
        }

        [Fact]
        public void GivenInvalidFuncs_WhenValidateRecursively_ThenCorrectValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(Funcs.Invalid());

            Assert.Single(results);
            ValidationResultUtility.AssertValidationResultEquals(results[0], "The SomeFunc field is required.", "SomeFunc");
        }
    }
}

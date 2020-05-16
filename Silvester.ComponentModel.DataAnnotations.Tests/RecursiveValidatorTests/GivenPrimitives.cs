using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures;
using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestUtilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests
{
    public class GivenPrimitives
    {
        [Fact]
        public void GivenValidUninitializedPrimitives_WhenValidateRecursively_ThenCorrectValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(new Primitives());
            Assert.Empty(results);
        }

        [Fact]
        public void GivenValidNullablePrimitives_WhenValidateRecursively_ThenCorrectValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(NullablePrimitives.Valid());

            Assert.Empty(results);
        }

        [Fact]
        public void GivenInvalidNullablePrimitives_WhenValidateRecursively_ThenCorrectValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(new NullablePrimitives());

            Assert.Equal(17, results.Count);
            ValidationResultUtility.AssertValidationResultEquals(results[0], "The RequiredString field is required.", "RequiredString");
            ValidationResultUtility.AssertValidationResultEquals(results[1], "The RequiredLong field is required.", "RequiredLong");
            ValidationResultUtility.AssertValidationResultEquals(results[2], "The RequiredUnsignedLong field is required.", "RequiredUnsignedLong");
            ValidationResultUtility.AssertValidationResultEquals(results[3], "The RequiredInt field is required.", "RequiredInt");
            ValidationResultUtility.AssertValidationResultEquals(results[4], "The RequiredUnsignedInt field is required.", "RequiredUnsignedInt");
            ValidationResultUtility.AssertValidationResultEquals(results[5], "The RequiredShort field is required.", "RequiredShort");
            ValidationResultUtility.AssertValidationResultEquals(results[6], "The RequiredUnsignedShort field is required.", "RequiredUnsignedShort");
            ValidationResultUtility.AssertValidationResultEquals(results[7], "The RequiredByte field is required.", "RequiredByte");
            ValidationResultUtility.AssertValidationResultEquals(results[8], "The RequiredChar field is required.", "RequiredChar");
            ValidationResultUtility.AssertValidationResultEquals(results[9], "The RequiredBool field is required.", "RequiredBool");
            ValidationResultUtility.AssertValidationResultEquals(results[10], "The RequiredFloat field is required.", "RequiredFloat");
            ValidationResultUtility.AssertValidationResultEquals(results[11], "The RequiredDouble field is required.", "RequiredDouble");
            ValidationResultUtility.AssertValidationResultEquals(results[12], "The RequiredDecimal field is required.", "RequiredDecimal");
            ValidationResultUtility.AssertValidationResultEquals(results[13], "The RequiredDateTime field is required.", "RequiredDateTime");
            ValidationResultUtility.AssertValidationResultEquals(results[14], "The RequiredDateTimeOffset field is required.", "RequiredDateTimeOffset");
            ValidationResultUtility.AssertValidationResultEquals(results[15], "The RequiredTimeSpan field is required.", "RequiredTimeSpan");
            ValidationResultUtility.AssertValidationResultEquals(results[16], "The RequiredGuid field is required.", "RequiredGuid");
        }
    }
}
using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests
{
    public class GivenNull
    {
        [Fact]
        public void GivenNullRoot_WhenValidateRecursively_ThenCorrectValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(null);
            Assert.Empty(results);
        }

        public static IEnumerable<object[]> Data()
        {
            yield return new object[] { new NullChildObject() };
            yield return new object[] { new NullChildArray() };
            yield return new object[] { new NullChildDictionary() };
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void GivenNullChild_WhenValidateRecursively_ThenCorrectValidationResults(object instance)
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(instance);

            Assert.Empty(results);
        }
    }
}

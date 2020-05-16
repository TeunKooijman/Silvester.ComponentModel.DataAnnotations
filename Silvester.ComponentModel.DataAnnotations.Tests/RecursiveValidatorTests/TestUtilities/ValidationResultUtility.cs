using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestUtilities
{
    public class ValidationResultUtility
    {
        public static void AssertValidationResultEquals(ValidationResult result, string expectedErrorMessage, params string[] expectedMemberNames)
        {
            Assert.Equal(expectedErrorMessage, result.ErrorMessage);
            string[] actualMemberNames = result.MemberNames.ToArray();

            for (int i = 0; i < expectedMemberNames.Length; i++)
            {
                Assert.Equal(expectedMemberNames[i], actualMemberNames[i]);
            }
        }
    }
}

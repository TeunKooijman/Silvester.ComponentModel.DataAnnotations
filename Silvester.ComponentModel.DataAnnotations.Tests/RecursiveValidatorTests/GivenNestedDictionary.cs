using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures;
using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests
{
    public class GivenNestedStringDictionary
    {
        public static IEnumerable<object[]> ValidDictionaries()
        {
            yield return new object[] { NestedDictionary<string>.Valid("key1") };
        }

        public static IEnumerable<object[]> InvalidDictionaries()
        {
            yield return new object[] { NestedDictionary<string>.WithInvalidChild("key1"), "key1" };
            yield return new object[] { NestedDictionary<int>.WithInvalidChild(-1), "-1" };
            yield return new object[] { NestedDictionary<uint>.WithInvalidChild(1), "1" };
            yield return new object[] { NestedDictionary<short>.WithInvalidChild(-2), "-2" };
            yield return new object[] { NestedDictionary<ushort>.WithInvalidChild(2), "2" };
            yield return new object[] { NestedDictionary<long>.WithInvalidChild(-3), "-3" };
            yield return new object[] { NestedDictionary<ulong>.WithInvalidChild(3), "3" };
            yield return new object[] { NestedDictionary<bool>.WithInvalidChild(true), "True" };
            yield return new object[] { NestedDictionary<bool>.WithInvalidChild(false), "False" };
            yield return new object[] { NestedDictionary<float>.WithInvalidChild(4f), "4" };
            yield return new object[] { NestedDictionary<double>.WithInvalidChild(5d), "5" };
            yield return new object[] { NestedDictionary<byte>.WithInvalidChild(6), "6" };
        }

        [Theory]
        [MemberData(nameof(ValidDictionaries))]
        public void GivenValidNestedStructure_WhenValidateRecursively_ThenCorrectValidationResults(object dictionary)
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(dictionary);
            Assert.Empty(results);
        }

        [Theory]
        [MemberData(nameof(InvalidDictionaries))]
        public void GivenInvalidChild_WhenValidateRecursively_ThenCorrectValidationResults(object dictionary, string key)
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(dictionary);

            Assert.Single(results);
            ValidationResultUtility.AssertValidationResultEquals(results[0], "The RequiredString field is required.", $"Child.{key}.RequiredString");
        }
    }
}

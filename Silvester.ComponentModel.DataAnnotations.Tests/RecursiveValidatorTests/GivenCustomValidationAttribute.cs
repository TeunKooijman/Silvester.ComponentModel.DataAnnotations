using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests
{
    public class GivenCustomValidationAttribute
    {
        [Fact]
        public void GivenValidObject_WhenValidateObjectRecursively_ThenNoValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(TestStructure.Valid());
            Assert.Empty(results);
        }

        [Fact]
        public void GivenInvalidObject_WhenValidateObjectRecursively_ThenCorrectValidationResults()
        {
            RecursiveValidator validator = new RecursiveValidator(null);
            List<ValidationResult> results = validator.ValidateObjectRecursively(TestStructure.Invalid());
            Assert.Single(results);
            ValidationResultUtility.AssertValidationResultEquals(results[0], "The SomeString field has to be lower cased.", "SomeString");
        }
    }

    public class TestStructure
    {
        public static TestStructure Valid()
        {
            return new TestStructure
            {
                SomeString = "some lowercased string"
            };
        }

        public static TestStructure Invalid()
        {
            return new TestStructure
            {
                SomeString = "Some String With Upper Case Letters"
            };
        }

        [IsLowerCase]
        public string SomeString { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IsLowerCaseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string instance = value as string;

            return instance.ToLower() == instance
                ? ValidationResult.Success
                : new ValidationResult($"The {validationContext.DisplayName} field has to be lower cased.", new[] { validationContext.MemberName });
        }
    }
}

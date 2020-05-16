using Microsoft.Extensions.Options;
using Silvester.ComponentModel.DataAnnotations.DependencyInjection;
using Silvester.ComponentModel.DataAnnotations.Tests.ReadMeTests.TestStructures;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Silvester.ComponentModel.DataAnnotations.Tests.ReadMeTests
{
	public class GivenNamingPolicy
	{
		private IRecursiveValidator Validator { get; }

		public GivenNamingPolicy()
		{
			RecursiveValidatorOptions options = new RecursiveValidatorOptions();
			options.ValidationNamingPolicy = new NaiveCamelCasingPolicy();

			Validator = new RecursiveValidator(options: Options.Create(options));
		}

		[Fact]
		public void GivenComplexObject_WhenValidateObjectRecursively_ResultsContainChildErrors()
		{
			SomeParent parent = new SomeParent
			{
				SomeString = "some value",
				SomeChild = new SomeChild
				{
					//This will trigger a validation error, as the property is annotated with [Required].
					SomeChildString = null
				}
			};

			List<ValidationResult> results = Validator.ValidateObjectRecursively(parent);
			Assert.Single(results);
			Assert.Equal("The SomeChildString field is required.", results[0].ErrorMessage);
			Assert.Equal("someChild.someChildString", results[0].MemberNames.First());
		}
	}
}

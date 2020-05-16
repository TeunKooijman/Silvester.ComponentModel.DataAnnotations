using Silvester.ComponentModel.DataAnnotations.Tests.ReadMeTests.TestStructures;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Silvester.ComponentModel.DataAnnotations.Tests.ReadMeTests
{
    public class GivenNoNamingPolicy
    {
		private IRecursiveValidator Validator { get; }

		public GivenNoNamingPolicy()
		{
			Validator = new RecursiveValidator(options: null);
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
			Assert.Equal("SomeChild.SomeChildString", results[0].MemberNames.First());
		}
	}
}

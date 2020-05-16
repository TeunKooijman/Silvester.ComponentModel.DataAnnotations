# Build Status
![.NET Core](https://github.com/TeunKooijman/Silvester.ComponentModel.DataAnnotations/workflows/.NET%20Core/badge.svg)

# Examples
## Dependency Injection
You can add an `IRecursiveValidator` to your IoC container through:
```
using Silvester.ComponentModel.DataAnnotations.DependencyInjection;

public void ConfigureServices(IServiceCollection services)
{
	services.AddRecursiveValidator();
}
```

You can also, optionally, configure the validator to adhere to some naming policy when defining the member names for the `ValidationResult`s.
```
using Silvester.ComponentModel.DataAnnotations.DependencyInjection;

public void ConfigureServices(IServiceCollection services)
{
	services.AddRecursiveValidator(options =>
	{
		options.ValidationNamingPolicy = new SomeClassImplementingIValidationNamingPolicy();
	});
}
```

If you are using, for example, the new `System.Text.Json` (de)serialization packages (with it's corresponding `JsonSerializerOptions`), you could fairly easily integrate the naming policies using:
```
using System.Text.Json;
using Microsoft.Extensions.Options;
using Silvester.ComponentModel.DataAnnotations.DependencyInjection.Options;

namespace Silvester.ComponentModel.DataAnnotations.Tests.DependencyInjectionTests
{
	public class JsonNamingPolicyAdapter : IValidationNamingPolicy
	{
		private IOptions<JsonSerializerOptions> Options { get; }
		
		public JsonNamingPolicyAdapter(IOptions<JsonSerializerOptions> options)
		{
			Options = options;
		}

		public string ConvertName(string name)
		{
			return Options?.Value?.PropertyNamingPolicy?.ConvertName(name) ?? name;
		}
	}
}

...

using Silvester.ComponentModel.DataAnnotations.DependencyInjection;

public void ConfigureServices(IServiceCollection services)
{
	services.AddRecursiveValidator((options, services) =>
	{
		options.ValidationNamingPolicy = ActivatorUtilities.CreateInstance<JsonNamingPolicyAdapter>(services);;
	});
}
```

## Basic Usage
Let's say we have some model that can be validated using `DataAnnotations`:
```
public class SomeParent
{
	[Required]
	public string SomeString {get;set;}
	
	[Required]
	public SomeChild SomeChild {get;set;}
}

public class SomeChild
{
	[Required]
	public string SomeChildString {get;set;}
}
```

We can recursively validate this object graph using the `IRecursiveValidator` that has been injected in our IoC container (or optionally create a concrete `RecursiveValidator` ourselves).
```
using Silvester.ComponentModel.DataAnnotations.Tests.ReadMeTests.TestStructures;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

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
```

## Naming Polcies
Or, if we instead want to apply (for example) some naïve camel casing policy for the member names paths:
```
using Silvester.ComponentModel.DataAnnotations.DependencyInjection.Options;

public class NaiveCamelCasingPolicy : IValidationNamingPolicy
{
	public string ConvertName(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return name;
		}

		return name.Substring(0, 1).ToLower() + name.Substring(1);
	}
}
```

The validation results would adhere to that policy:
```
using Microsoft.Extensions.Options;
using Silvester.ComponentModel.DataAnnotations.DependencyInjection;
using Silvester.ComponentModel.DataAnnotations.Tests.ReadMeTests.TestStructures;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

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
		
		//The path is now camel-cased:
		Assert.Equal("someChild.someChildString", results[0].MemberNames.First());
	}
}
```





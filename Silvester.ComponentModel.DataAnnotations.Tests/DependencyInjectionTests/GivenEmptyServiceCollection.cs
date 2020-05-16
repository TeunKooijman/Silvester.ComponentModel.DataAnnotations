using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Silvester.ComponentModel.DataAnnotations.DependencyInjection;
using Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests;
using System;
using Xunit;

namespace Silvester.ComponentModel.DataAnnotations.Tests.DependencyInjectionTests
{
    public class GivenEmptyServiceCollection
    {
        [Fact]
        public void GivenNullConfigureAction_WhenAddRecursiveValidator_ThenServiceCanBeResolved()
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddRecursiveValidator();
            IServiceProvider services = collection.BuildServiceProvider();
            Assert.NotNull(services.GetService<IRecursiveValidator>());
        }

        [Fact]
        public void GivenNullConfigureAction_WhenAddRecursiveValidator_ThenOptionsCantBeResolved()
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddRecursiveValidator();
            IServiceProvider services = collection.BuildServiceProvider(); 
            IOptions<RecursiveValidatorOptions> options = services.GetService<IOptions<RecursiveValidatorOptions>>();
            Assert.NotNull(options);
            Assert.NotNull(options.Value);
            Assert.Null(options.Value.ValidationNamingPolicy);
        }

        [Fact]
        public void GivenNonNullConfigureAction_WhenAddRecursiveValidator_ThenServiceCanBeResolved()
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddRecursiveValidator(options => { });
            IServiceProvider services = collection.BuildServiceProvider();
            Assert.NotNull(services.GetService<IRecursiveValidator>());
        }

        [Fact]
        public void GivenNonNullConfigureAction_WhenAddRecursiveValidator_ThenOptionsCanBeResolved()
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddRecursiveValidator(options => { options.ValidationNamingPolicy = new NaiveCamelCasingPolicy(); });
            IServiceProvider services = collection.BuildServiceProvider();
            IOptions<RecursiveValidatorOptions> options = services.GetService<IOptions<RecursiveValidatorOptions>>();
            Assert.NotNull(options);
            Assert.NotNull(options.Value);
            Assert.NotNull(options.Value.ValidationNamingPolicy);
            Assert.IsType<NaiveCamelCasingPolicy>(options.Value.ValidationNamingPolicy);
        }
    }
}

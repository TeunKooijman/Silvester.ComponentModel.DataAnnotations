using Microsoft.Extensions.DependencyInjection;
using Silvester.ComponentModel.DataAnnotations.DependencyInjection;
using System;
using Xunit;

namespace Silvester.ComponentModel.DataAnnotations.Tests.DependencyInjectionTests
{
    public class GivenRequiredServices
    {
        [Fact]
        public void WhenServicesRequired_ThenServiceCanBeResolvedInConfigureAction()
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddTransient<ToResolve>();
            collection.AddRecursiveValidator((options, services) => 
            {
                Assert.NotNull(services);
                Assert.NotNull(services.GetService<ToResolve>());
            });
            IServiceProvider services = collection.BuildServiceProvider();
        }

        public class ToResolve
        {

        }
    }
}

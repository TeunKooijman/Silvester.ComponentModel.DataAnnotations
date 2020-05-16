using Microsoft.Extensions.DependencyInjection;
using System;

namespace Silvester.ComponentModel.DataAnnotations.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRecursiveValidator(this IServiceCollection services, Action<RecursiveValidatorOptions> configureAction = null)
        {
            if(services == null)
            {
                return null;
            }

            services.AddOptions();
            if(configureAction != null)
            {
                services.Configure(configureAction);
            }
            services.AddTransient<IRecursiveValidator, RecursiveValidator>();

            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;

namespace Silvester.ComponentModel.DataAnnotations.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRecursiveValidator(this IServiceCollection services, Action<RecursiveValidatorOptions> configureAction = null)
        {
            if (services == null)
            {
                return null;
            }

            services.AddRecursiveValidator((options, services) => 
            {
                if(configureAction != null)
                {
                    configureAction.Invoke(options);
                }
            });
            
            return services;
        }
        
        public static IServiceCollection AddRecursiveValidator(this IServiceCollection services, Action<RecursiveValidatorOptions, IServiceProvider> configureAction)
        {
            if (services == null)
            {
                return null;
            }

            

            if (configureAction != null)
            {
                services
                    .AddOptions<RecursiveValidatorOptions>()
                    .Configure(configureAction);
            }

            services.AddTransient<IRecursiveValidator, RecursiveValidator>();

            return services;
        }
    }
}

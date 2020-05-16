using Silvester.ComponentModel.DataAnnotations.DependencyInjection.Options;

namespace Silvester.ComponentModel.DataAnnotations.DependencyInjection
{
    public class RecursiveValidatorOptions
    {
        public IValidationNamingPolicy ValidationNamingPolicy { get; set; }
    }
}
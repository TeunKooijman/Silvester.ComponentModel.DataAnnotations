using Silvester.ComponentModel.DataAnnotations.DependencyInjection.Options;

namespace Silvester.ComponentModel.DataAnnotations.Tests.ReadMeTests.TestStructures
{
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
}

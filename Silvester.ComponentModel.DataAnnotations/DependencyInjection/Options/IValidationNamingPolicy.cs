namespace Silvester.ComponentModel.DataAnnotations.DependencyInjection.Options
{
    public interface IValidationNamingPolicy
    {
        string ConvertName(string name);
    }
}

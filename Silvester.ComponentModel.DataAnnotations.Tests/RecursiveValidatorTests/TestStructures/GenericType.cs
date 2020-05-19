using System.ComponentModel.DataAnnotations;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures
{
    public class GenericType
    {
        public static GenericType Valid()
        {
            return new GenericType
            {
                Child = new GenericChild<string>
                {
                    Value = "some-value"
                }
            };
        }

        public static GenericType WithMissingChildValue()
        {
            return new GenericType
            {
                Child = new GenericChild<string>()
            };
        }

        public static GenericType WithMissingChild()
        {
            return new GenericType();
        }

        [Required]
        public GenericChild<string> Child { get; set; }
    }

    public class GenericChild<T>
    {
        [Required]
        public T Value { get; set; }
    }
}

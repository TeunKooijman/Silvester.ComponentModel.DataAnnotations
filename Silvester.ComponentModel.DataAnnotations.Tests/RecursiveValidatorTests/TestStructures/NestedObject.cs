using System.ComponentModel.DataAnnotations;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures
{
    public class NestedObject
    {
        public static NestedObject Valid()
        {
            return new NestedObject
            {
                RequiredString = "s",
                RequiredLong = -3,
                RequiredUnsignedLong = 3,
                Child = new Child
                {
                    RequiredString = "s",
                    RequiredLong = -3,
                    RequiredUnsignedLong = 3,
                }
            };
        }

        public static NestedObject WithInvalidChild()
        {
            return new NestedObject
            {
                RequiredString = "s",
                RequiredLong = -3,
                RequiredUnsignedLong = 3,
                Child = new Child
                {

                }
            };
        }

        [Required]
        public string RequiredString { get; set; }

        [Required]
        public long? RequiredLong { get; set; }

        [Required]
        public ulong? RequiredUnsignedLong { get; set; }

        [Required]
        public Child Child { get; set; }
    }

    public class Child
    {
        [Required]
        public string RequiredString { get; set; }

        [Required]
        public long? RequiredLong { get; set; }

        [Required]
        public ulong? RequiredUnsignedLong { get; set; }
    }
}

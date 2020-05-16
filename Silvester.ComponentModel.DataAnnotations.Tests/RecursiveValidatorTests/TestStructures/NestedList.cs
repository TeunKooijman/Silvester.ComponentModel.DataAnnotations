using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures
{
    public class NestedList
    {
        public static NestedList Valid()
        {
            return new NestedList
            {
                RequiredString = "s",
                RequiredLong = -3,
                RequiredUnsignedLong = 3,
                Child = new List<Child>
                {
                    new Child
                    {
                        RequiredString = "s",
                        RequiredLong = -3,
                        RequiredUnsignedLong = 3,
                    }
                }

            };
        }

        public static NestedList WithInvalidChild()
        {
            return new NestedList
            {
                RequiredString = "s",
                RequiredLong = -3,
                RequiredUnsignedLong = 3,
                Child = new List<Child>
                {
                    new Child()
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
        public List<Child> Child { get; set; }
    }
}

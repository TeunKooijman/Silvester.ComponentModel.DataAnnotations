using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures
{
    public class NestedArray
    {
        public static NestedArray Valid()
        {
            return new NestedArray
            {
                RequiredString = "s",
                RequiredLong = -3,
                RequiredUnsignedLong = 3,
                Child = new Child[]
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

        public static NestedArray WithInvalidChild()
        {
            return new NestedArray
            {
                RequiredString = "s",
                RequiredLong = -3,
                RequiredUnsignedLong = 3,
                Child = new Child[]
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
        public Child[] Child { get; set; }
    }
}

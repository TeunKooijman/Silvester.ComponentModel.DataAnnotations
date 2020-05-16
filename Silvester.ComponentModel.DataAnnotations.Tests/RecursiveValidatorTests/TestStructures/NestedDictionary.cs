using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures
{
    public class NestedDictionary<TKey>
    {
        public static NestedDictionary<TKey> Valid(TKey key)
        {
            return new NestedDictionary<TKey>
            {
                RequiredString = "s",
                RequiredLong = -3,
                RequiredUnsignedLong = 3,
                Child = new Dictionary<TKey, Child>
                {
                    {key,  new Child
                        {
                            RequiredString = "s",
                            RequiredLong = -3,
                            RequiredUnsignedLong = 3,
                        }
                    }
                }

            };
        }

        public static NestedDictionary<TKey> WithInvalidChild(TKey key)
        {
            return new NestedDictionary<TKey>
            {
                RequiredString = "s",
                RequiredLong = -3,
                RequiredUnsignedLong = 3,
                Child = new Dictionary<TKey, Child>
                {
                    {key,  new Child()
                        {
                            RequiredLong = -3,
                            RequiredUnsignedLong = 3,
                        }
                    }
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
        public Dictionary<TKey, Child> Child { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures
{
    public class CyclicObjectGraph
    {
        public static CyclicObjectGraph Valid()
        {
            CyclicObjectGraph parent = new CyclicObjectGraph
            {
                RequiredString = "s",
                RequiredLong = -3,
                RequiredUnsignedLong = 3,
            };

            CylicChild child = new CylicChild
            {
                RequiredString = "s",
                RequiredLong = -3,
                RequiredUnsignedLong = 3,
                CyclicObjectGraph = parent
            };
            parent.Child = child;

            return parent;
        }

        public static CyclicObjectGraph WithInvalidChild()
        {
            CyclicObjectGraph parent = new CyclicObjectGraph
            {
                RequiredString = "s",
                RequiredLong = -3,
                RequiredUnsignedLong = 3,
            };

            CylicChild child = new CylicChild
            {
                RequiredString = "s",
                RequiredLong = -3,
                CyclicObjectGraph = parent
            };
            parent.Child = child;

            return parent;
        }

        public static CyclicObjectGraph WithInvalidParent()
        {
            CyclicObjectGraph parent = new CyclicObjectGraph
            {
                RequiredLong = -3,
                RequiredUnsignedLong = 3,
            };

            CylicChild child = new CylicChild
            {
                RequiredString = "s",
                RequiredLong = -3,
                RequiredUnsignedLong = 3,
                CyclicObjectGraph = parent
            };
            parent.Child = child;

            return parent;
        }

        [Required]
        public string RequiredString { get; set; }

        [Required]
        public long? RequiredLong { get; set; }

        [Required]
        public ulong? RequiredUnsignedLong { get; set; }

        [Required]
        public CylicChild Child { get; set; }
    }

    public class CylicChild
    {
        [Required]
        public string RequiredString { get; set; }

        [Required]
        public long? RequiredLong { get; set; }

        [Required]
        public ulong? RequiredUnsignedLong { get; set; }

        [Required]
        public CyclicObjectGraph CyclicObjectGraph { get; set; }
    }
}

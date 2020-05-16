using System;
using System.ComponentModel.DataAnnotations;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures
{
    public class NullablePrimitiveArray
    {
        public static NullablePrimitiveArray Valid()
        {
            return new NullablePrimitiveArray()
            {
                RequiredString =new [] { "Some string"},
                RequiredLong = new long?[] {-3L },
                RequiredUnsignedLong = new ulong?[] {3L},
                RequiredInt = new int?[] {-3},
                RequiredUnsignedInt = new uint?[] {3},
                RequiredShort = new short?[] {-3},
                RequiredUnsignedShort = new ushort?[] {3},
                RequiredByte = new byte?[] {7},
                RequiredChar = new char?[] {'c'},
                RequiredBool = new bool?[] {false},
                RequiredFloat = new float?[] {3.3f},
                RequiredDouble = new double?[] {3.3d},
                RequiredDecimal = new decimal?[] {3m},
                RequiredDateTime = new DateTime?[] {DateTime.Now},
                RequiredDateTimeOffset = new DateTimeOffset?[] {DateTimeOffset.Now},
                RequiredTimeSpan = new TimeSpan?[] {TimeSpan.FromSeconds(3)},
                RequiredGuid = new Guid?[] {Guid.NewGuid() }
            };
        }

        [Required]
        public string[] RequiredString { get; set; }

        [Required]
        public long?[] RequiredLong { get; set; }

        [Required]
        public ulong?[] RequiredUnsignedLong { get; set; }

        [Required]
        public int?[] RequiredInt { get; set; }

        [Required]
        public uint?[] RequiredUnsignedInt { get; set; }

        [Required]
        public short?[] RequiredShort { get; set; }

        [Required]
        public ushort?[] RequiredUnsignedShort { get; set; }

        [Required]
        public byte?[] RequiredByte { get; set; }

        [Required]
        public char?[] RequiredChar { get; set; }

        [Required]
        public bool?[] RequiredBool { get; set; }

        [Required]
        public float?[] RequiredFloat { get; set; }

        [Required]
        public double?[] RequiredDouble { get; set; }

        [Required]
        public decimal?[] RequiredDecimal { get; set; }

        [Required]
        public DateTime?[] RequiredDateTime { get; set; }

        [Required]
        public DateTimeOffset?[] RequiredDateTimeOffset { get; set; }

        [Required]
        public TimeSpan?[] RequiredTimeSpan { get; set; }

        [Required]
        public Guid?[] RequiredGuid { get; set; }
    }
}

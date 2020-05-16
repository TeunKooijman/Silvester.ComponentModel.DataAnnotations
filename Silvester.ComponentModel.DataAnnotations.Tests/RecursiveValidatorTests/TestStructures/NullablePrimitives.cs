using System;
using System.ComponentModel.DataAnnotations;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures
{
    public class NullablePrimitives
    {
        public static NullablePrimitives Valid()
        {
            return new NullablePrimitives()
            {
                RequiredString = "Some string",
                RequiredLong = -3L,
                RequiredUnsignedLong = 3L,
                RequiredInt = -3,
                RequiredUnsignedInt = 3,
                RequiredShort = -3,
                RequiredUnsignedShort = 3,
                RequiredByte = 7,
                RequiredChar = 'c',
                RequiredBool = false,
                RequiredFloat = 3.3f,
                RequiredDouble = 3.3d,
                RequiredDecimal = 3m,
                RequiredDateTime = DateTime.Now,
                RequiredDateTimeOffset = DateTimeOffset.Now,
                RequiredTimeSpan = TimeSpan.FromSeconds(3),
                RequiredGuid = Guid.NewGuid()
            };
        }

        [Required]
        public string RequiredString { get; set; }

        [Required]
        public long? RequiredLong { get; set; }

        [Required]
        public ulong? RequiredUnsignedLong { get; set; }

        [Required]
        public int? RequiredInt { get; set; }

        [Required]
        public uint? RequiredUnsignedInt { get; set; }

        [Required]
        public short? RequiredShort { get; set; }

        [Required]
        public ushort? RequiredUnsignedShort { get; set; }

        [Required]
        public byte? RequiredByte { get; set; }

        [Required]
        public char? RequiredChar { get; set; }

        [Required]
        public bool? RequiredBool { get; set; }

        [Required]
        public float? RequiredFloat { get; set; }

        [Required]
        public double? RequiredDouble { get; set; }

        [Required]
        public decimal? RequiredDecimal { get; set; }

        [Required]
        public DateTime? RequiredDateTime { get; set; }

        [Required]
        public DateTimeOffset? RequiredDateTimeOffset { get; set; }

        [Required]
        public TimeSpan? RequiredTimeSpan { get; set; }

        [Required]
        public Guid? RequiredGuid { get; set; }
    }
}

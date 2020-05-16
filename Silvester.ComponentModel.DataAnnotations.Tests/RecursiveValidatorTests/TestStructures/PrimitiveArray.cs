using System;
using System.ComponentModel.DataAnnotations;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures
{
    public class PrimitiveArray
    {
        public static PrimitiveArray Valid()
        {
            return new PrimitiveArray
            {
                RequiredLong = new long[] { },
                RequiredUnsignedLong = new ulong[] { },
                RequiredInt = new int[] { },
                RequiredUnsignedInt = new uint[] { },
                RequiredShort = new short[] { },
                RequiredUnsignedShort = new ushort[] { },
                RequiredByte = new byte[] { },
                RequiredChar = new char[] { },
                RequiredBool = new bool[] { },
                RequiredFloat = new float[] { },
                RequiredDouble = new double[] { },
                RequiredDecimal = new decimal[] { }
            };
        }

        [Required]
        public long[] RequiredLong { get; set; }

        [Required]
        public ulong[] RequiredUnsignedLong { get; set; }

        [Required]
        public int[] RequiredInt { get; set; }

        [Required]
        public uint[] RequiredUnsignedInt { get; set; }

        [Required]
        public short[] RequiredShort { get; set; }

        [Required]
        public ushort[] RequiredUnsignedShort { get; set; }

        [Required]
        public byte[] RequiredByte { get; set; }

        [Required]
        public char[] RequiredChar { get; set; }

        [Required]
        public bool[] RequiredBool { get; set; }

        [Required]
        public float[] RequiredFloat { get; set; }

        [Required]
        public double[] RequiredDouble { get; set; }

        [Required]
        public decimal[] RequiredDecimal { get; set; }
    }
}

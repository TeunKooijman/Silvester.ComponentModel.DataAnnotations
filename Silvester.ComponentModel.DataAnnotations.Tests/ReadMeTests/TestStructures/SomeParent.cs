using System.ComponentModel.DataAnnotations;

namespace Silvester.ComponentModel.DataAnnotations.Tests.ReadMeTests.TestStructures
{
	public class SomeParent
	{
		[Required]
		public string SomeString { get; set; }

		[Required]
		public SomeChild SomeChild { get; set; }
	}

	public class SomeChild
	{
		[Required]
		public string SomeChildString { get; set; }
	}
}

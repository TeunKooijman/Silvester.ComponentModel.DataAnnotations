using System.Collections.Generic;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures
{
    public class NullChildDictionary
    {
        public Dictionary<NullablePrimitives, string> Children { get; set; }
    }
}

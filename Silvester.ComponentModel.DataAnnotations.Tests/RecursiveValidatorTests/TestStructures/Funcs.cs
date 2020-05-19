using System;
using System.ComponentModel.DataAnnotations;

namespace Silvester.ComponentModel.DataAnnotations.Tests.RecursiveValidatorTests.TestStructures
{
    public class Funcs
    {
        public static Funcs Valid()
        {
            return new Funcs
            {
                SomeFunc = (o) => { return o; }
            };
        }

        public static Funcs Invalid()
        {
            return new Funcs();
        }

        [Required]
        public Func<object, object> SomeFunc { get; set; }
    }
}

using System;
using System.Linq;

namespace Silvester.ComponentModel.DataAnnotations.Extensions
{
    public static class TypeExtensions
    {
        private static Type[] SpecialNonPrimitives = new Type[] { typeof(string), typeof(decimal), typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan), typeof(Guid) };

        public static bool IsSimpleType(this Type type)
        {
            return
                type.IsPrimitive ||
                SpecialNonPrimitives.Contains(type) ||
                type.IsEnum ||
                Convert.GetTypeCode(type) != TypeCode.Object ||
                type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && type.GetGenericArguments()[0].IsSimpleType();
        }
    }
}

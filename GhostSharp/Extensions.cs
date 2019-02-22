using GhostSharp.Attributes;
using GhostSharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostSharp
{
    public static class Ext
    {
        public static string GetQueryStringFromFlagsEnum<T>(Enum fields) where T : Enum
        {
            return string.Join(",", Enum.GetValues(typeof(T)).Cast<T>()
                                        .Where(x => fields.HasFlag(x))
                                        .Select(x => GetFieldName(x)));
        }

        public static string GetOrderQueryString<T>(IEnumerable<Tuple<T, OrderDirection>> fields) where T : Enum
        {
            return string.Join(",", from field in fields
                                    let name = GetFieldName(field.Item1)
                                    where name != null
                                    select $"{name} {field.Item2.ToString()}");
        }

        public static string GetFieldName<T>(T enumValue) where T : Enum
        {
            var t = typeof(T);
            try
            {
                return ((GhostFieldAttribute)t.GetMember(t.GetEnumName(enumValue))[0]
                                              .GetCustomAttributes(typeof(GhostFieldAttribute), false)
                                              .SingleOrDefault())?.FieldName;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
    }
}

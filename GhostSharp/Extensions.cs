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
            var t = typeof(T);
            return string.Join(",", fields.Select(x => $"{GetFieldName(x.Item1)}%20{x.Item2.ToString()}"));
        }

        public static string GetFieldName<T>(T enumValue) where T : Enum
        {
            var t = typeof(T);
            return ((GhostFieldAttribute)t.GetMember(t.GetEnumName(enumValue))[0]
                                          .GetCustomAttributes(typeof(GhostFieldAttribute), false)
                                          .Single()).FieldName;
        }
    }
}

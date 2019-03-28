using GhostSharp.Attributes;
using GhostSharp.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostSharp
{
    public static class Ext
    {
        /// <summary>
        /// Translates a list of fields, such as used to limit which fields are returned,
        /// into a comma-delimited list that can be used in the querystring.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static string GetQueryStringFromFlagsEnum<T>(Enum fields) where T : Enum
        {
            return string.Join(",", Enum.GetValues(typeof(T)).Cast<T>()
                                        .Where(x => fields.HasFlag(x))
                                        .Select(x => GetFieldName(x)));
        }

        /// <summary>
        /// Translates the list of fields, used to order the results,
        /// into a comma-delimited list that can be used in the querystring.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static string GetOrderQueryString<T>(IEnumerable<Tuple<T, OrderDirection>> fields) where T : Enum
        {
            return string.Join(",", from field in fields
                                    let name = GetFieldName(field.Item1)
                                    where name != null
                                    select $"{name} {field.Item2.ToString()}");
        }

        /// <summary>
        /// For a given enum value, returns the field name as it exists in the Ghost API.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="enumValue">The enum value to retrieve the field name of.</param>
        /// <returns></returns>
        public static string GetFieldName<T>(T enumValue) where T : Enum
        {
            var t = typeof(T);
            try
            {
                return ((JsonPropertyAttribute)t.GetMember(t.GetEnumName(enumValue))[0]
                                              .GetCustomAttributes(typeof(JsonPropertyAttribute), false)
                                              .SingleOrDefault())?.PropertyName;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}

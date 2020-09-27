using GhostSharp.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace GhostSharp.ContractResolvers
{
    public class UpdateContractResolver : DefaultContractResolver
    {
        public static readonly UpdateContractResolver Instance = new UpdateContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            var setterMethod = property.DeclaringType.GetProperty(property.UnderlyingName).GetSetMethod();

            property.ShouldSerialize =
                instance =>
                {
                    // Property must have a public setter or a RequiredForUpdate attribute
                    return (setterMethod != null && setterMethod.IsPublic) ||
                           property.DeclaringType.GetProperty(property.UnderlyingName)
                                   .GetCustomAttribute<RequiredForUpdateAttribute>() != null;
                };

            return property;
        }
    }
}

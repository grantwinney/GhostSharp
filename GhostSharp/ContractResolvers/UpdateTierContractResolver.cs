using GhostSharp.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace GhostSharp.ContractResolvers
{
    public class UpdateTierContractResolver : DefaultContractResolver
    {
        public static readonly UpdateTierContractResolver Instance = new UpdateTierContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty jsonProp = base.CreateProperty(member, memberSerialization);

            var setterMethod = jsonProp.DeclaringType.GetProperty(jsonProp.UnderlyingName).GetSetMethod();

            jsonProp.ShouldSerialize =
                instance =>
                {
                    // Property must have a public setter or a RequiredForUpdate attribute
                    return setterMethod?.IsPublic == true
                           || jsonProp.DeclaringType.GetProperty(jsonProp.UnderlyingName)
                                      .GetCustomAttribute<RequiredForUpdateAttribute>() != null;
                };

            return jsonProp;
        }
    }
}

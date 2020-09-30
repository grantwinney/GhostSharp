using GhostSharp.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace GhostSharp.ContractResolvers
{
    public class UpdatePostContractResolver : DefaultContractResolver
    {
        public static readonly UpdatePostContractResolver Instance = new UpdatePostContractResolver();

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

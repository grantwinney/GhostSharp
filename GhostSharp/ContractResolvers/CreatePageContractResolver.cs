using GhostSharp.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace GhostSharp.ContractResolvers
{
    public class CreatePageContractResolver : DefaultContractResolver
    {
        public static readonly CreatePageContractResolver Instance = new CreatePageContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty jsonProp = base.CreateProperty(member, memberSerialization);

            var property = jsonProp.DeclaringType.GetProperty(jsonProp.UnderlyingName);

            jsonProp.ShouldSerialize =
                instance =>
                {
                    return property.GetSetMethod()?.IsPublic == true
                        && property.GetCustomAttribute<PostOnly>() == null;
                };

            return jsonProp;
        }
    }
}

using GhostSharp.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace GhostSharp.ContractResolvers
{
    public class UpdatePageContractResolver : DefaultContractResolver
    {
        public static readonly UpdatePageContractResolver Instance = new UpdatePageContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty jsonProp = base.CreateProperty(member, memberSerialization);

            var property = jsonProp.DeclaringType.GetProperty(jsonProp.UnderlyingName);

            jsonProp.ShouldSerialize =
                instance =>
                {
                    return (property.GetSetMethod()?.IsPublic == true && property.GetCustomAttribute<PostOnly>() == null)
                            || property.GetCustomAttribute<RequiredForUpdateAttribute>() != null;
                };

            return jsonProp;
        }
    }
}

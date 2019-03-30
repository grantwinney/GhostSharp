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

            property.ShouldSerialize =
                instance =>
                {
                    return property.DeclaringType.GetProperty(property.UnderlyingName)
                                   .GetCustomAttribute<UpdatableFieldAttribute>() != null;
                };

            return property;
        }
    }
}

using GhostSharp.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace GhostSharp.ContractResolvers
{
    public class UpdateOfferContractResolver : DefaultContractResolver
    {
        public static readonly UpdateOfferContractResolver Instance = new UpdateOfferContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty jsonProp = base.CreateProperty(member, memberSerialization);

            var property = jsonProp.DeclaringType.GetProperty(jsonProp.UnderlyingName);

            jsonProp.ShouldSerialize =
                instance =>
                {
                    return property.GetSetMethod()?.IsPublic == true;
                };

            return jsonProp;
        }
    }
}

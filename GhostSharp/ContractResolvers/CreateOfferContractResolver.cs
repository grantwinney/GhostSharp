using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace GhostSharp.ContractResolvers
{
    public class CreateOfferContractResolver : DefaultContractResolver
    {
        public static readonly CreateOfferContractResolver Instance = new CreateOfferContractResolver();

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

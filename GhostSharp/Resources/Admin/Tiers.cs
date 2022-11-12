using GhostSharp.ContractResolvers;
using GhostSharp.Entities;
using GhostSharp.QueryParams;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace GhostSharp
{
    public partial class GhostAdminAPI
    {
        /// <summary>
        /// Get all tiers
        /// </summary>
        /// <returns>Returns all available tiers</returns>
        /// <seealso cref="https://ghost.org/docs/content-api/#usage"/>
        public new TierResponse GetTiers(TierQueryParams queryParams = null)
        {
            return base.GetTiers(queryParams);
        }

        /// <summary>
        /// Create a tier
        /// </summary>
        /// <param name="tier">Tier to create</param>
        /// <returns>Returns the created tier</returns>
        /// <seealso cref="https://ghost.org/docs/admin-api/#creating-a-tier"/>
        public Tier CreateTier(Tier tier)
        {
            var request = new RestRequest("tiers/", Method.Post);
            request.AddJsonBody(new TierRequest { Tiers = new List<Tier> { tier } });
            return Execute<TierRequest>(request).Tiers[0];
        }

        /// <summary>
        /// Update a tier
        /// </summary>
        /// <param name="tier">Tier to update</param>
        /// <returns>Returns the updated tier</returns>
        /// <seealso cref="https://ghost.org/docs/admin-api/#updating-a-tier"/>
        public Tier UpdateTier(Tier tier)
        {
            var serializedTier = JsonConvert.SerializeObject(
               new TierRequest { Tiers = new List<Tier> { tier } },
               new JsonSerializerSettings { ContractResolver = UpdateTierContractResolver.Instance }
            );

            var request = new RestRequest($"tiers/{tier.ID}", Method.Post);
            request.AddJsonBody(serializedTier);
            return Execute<TierRequest>(request).Tiers[0];
        }
    }
}

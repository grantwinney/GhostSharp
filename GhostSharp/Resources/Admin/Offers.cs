using GhostSharp.ContractResolvers;
using GhostSharp.Entities;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace GhostSharp
{
    public partial class GhostAdminAPI
    {
        /// <summary>
        /// Get all offers
        /// </summary>
        /// <returns>Returns all available offers</returns>
        /// <seealso cref="https://ghost.org/docs/admin-api/#offers"/>
        public OfferResponse GetOffers()
        {
            var request = new RestRequest("offers/", Method.Get);
            return Execute<OfferResponse>(request);
        }

        /// <summary>
        /// Create an offer
        /// </summary>
        /// <param name="offer">offer to create</param>
        /// <returns>Returns the created offer</returns>
        /// <seealso cref="https://ghost.org/docs/admin-api/#creating-an-offer"/>
        public Offer CreateOffer(Offer offer)
        {
            var serializedOffer = JsonConvert.SerializeObject(
               new OfferRequest { Offers = new List<Offer> { offer } },
               new JsonSerializerSettings { ContractResolver = CreateOfferContractResolver.Instance }
            );

            var request = new RestRequest("offers/", Method.Post);
            request.AddJsonBody(serializedOffer);
            return Execute<OfferRequest>(request).Offers[0];
        }

        /// <summary>
        /// Update an offer
        /// (only Name, Code, DisplayTitle and DisplayDescription are editable)
        /// </summary>
        /// <param name="offer">Tier to update</param>
        /// <returns>Returns the updated tier</returns>
        /// <seealso cref="https://ghost.org/docs/admin-api/#updating-an-offer"/>
        public Offer UpdateOffer(Offer offer)
        {
            var serializedOffer = JsonConvert.SerializeObject(
               new OfferRequest { Offers = new List<Offer> { offer } },
               new JsonSerializerSettings { ContractResolver = UpdateOfferContractResolver.Instance }
            );

            var request = new RestRequest($"offers/{offer.ID}", Method.Post);
            request.AddJsonBody(serializedOffer);
            return Execute<OfferRequest>(request).Offers[0];
        }
    }
}

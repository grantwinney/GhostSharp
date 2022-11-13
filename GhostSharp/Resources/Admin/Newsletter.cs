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
        /// Get a collection of newsletters.
        /// </summary>
        /// <returns>The newsletters.</returns>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public NewsletterResponse GetNewsletters(NewsletterQueryParams queryParams = null)
        {
            var request = new RestRequest("newsletters/", Method.Get);
            ApplyNewsletterQueryParams(request, queryParams);
            return Execute<NewsletterResponse>(request);
        }

        /// <summary>
        /// Create a newsletter
        /// </summary>
        /// <param name="newsletter">Newsletter to create</param>
        /// <param name="optInExisting">When set to true, existing members with a subscription to one or more active newsletters are also subscribed to this newsletter.</param>
        /// <returns>Returns the created newsletter</returns>
        /// <seealso cref="https://ghost.org/docs/admin-api/#creating-a-newsletter"/>
        public Newsletter CreateNewsletter(Newsletter newsletter, bool optInExisting = false)
        {
            var request = new RestRequest("newsletters/", Method.Post);
            request.AddJsonBody(new NewsletterRequest { Newsletters = new List<Newsletter> { newsletter } });
            if (optInExisting)
                request.AddQueryParameter("opt_in_existing", optInExisting);
            return Execute<NewsletterRequest>(request).Newsletters[0];
        }

        /// <summary>
        /// Update a newsletter
        /// </summary>
        /// <param name="newsletter">Newsletter to update</param>
        /// <returns>Returns the updated newsletter</returns>
        public Newsletter UpdateNewsletter(Newsletter newsletter)
        {
            var serializedPost = JsonConvert.SerializeObject(
               new NewsletterRequest { Newsletters = new List<Newsletter> { newsletter } },
               new JsonSerializerSettings { ContractResolver = UpdatePostContractResolver.Instance }
            );

            var request = new RestRequest($"newsletters/{newsletter.ID}/", Method.Put);
            request.AddJsonBody(serializedPost);
            return Execute<NewsletterRequest>(request).Newsletters[0];
        }

        /// <summary>
        /// Applies any specified parameters to the request.
        /// </summary>
        /// <param name="request">REST request.</param>
        /// <param name="queryParams">Query parameters.</param>
        private static void ApplyNewsletterQueryParams(RestRequest request, NewsletterQueryParams queryParams)
        {
            if (queryParams != null)
            {
                if (queryParams.NoLimit)
                    request.AddQueryParameter("limit", "all");
                else if (queryParams.Limit > 0)
                    request.AddQueryParameter("limit", queryParams.Limit);
            }
        }
    }
}

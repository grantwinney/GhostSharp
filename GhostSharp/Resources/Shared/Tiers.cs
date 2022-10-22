using System.Collections.Generic;
using System.Linq;
using GhostSharp.Entities;
using GhostSharp.Enums;
using GhostSharp.QueryParams;
using RestSharp;

namespace GhostSharp
{
    public partial class GhostAPI
    {
        public TiersResponse GetTiers(TierQueryParams queryParams = null)
        {
            var request = new RestRequest("tiers/", Method.Get);
            ApplyTierQueryParams(request, queryParams);
            return Execute<TiersResponse>(request);
        }

        /// <summary>
        /// Applies any specified parameters to the post request.
        /// </summary>
        /// <param name="request">A post REST request.</param>
        /// <param name="queryParams">Query parameters.</param>
        private void ApplyTierQueryParams(RestRequest request, TierQueryParams queryParams)
        {
            if (queryParams != null)
            {
                var includeList = new List<string>();

                if (queryParams.IncludeBenefits)
                    includeList.Add("benefits");
                if (queryParams.IncludeMonthlyPrice)
                    includeList.Add("monthly_price");
                if (queryParams.IncludeYearlyPrice)
                    includeList.Add("yearly_price");

                if (includeList.Any())
                    request.AddQueryParameter("include", string.Join(",", includeList));
            }
        }
    }
}

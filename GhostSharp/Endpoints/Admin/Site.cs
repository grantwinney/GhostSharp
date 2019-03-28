using GhostSharp.Entities;
using RestSharp;

namespace GhostSharp
{
    public partial class GhostAdminAPI
    {
        /// <summary>
        /// Retrieve basic information about a site.
        /// </summary>
        /// <returns>Basic information about a site. (i.e. title, version, URL)</returns>
        public Site GetSite()
        {
            var request = new RestRequest("site/", Method.GET);

            return Execute<SiteResponse>(request).Site;
        }
    }
}


using GhostSharp.Entities;
using RestSharp;

namespace GhostSharp
{
    public partial class GhostAPI
    {
        public Settings GetSettings()
        {
            var request = new RestRequest("settings", Method.GET);

            request.AddQueryParameter("key", key);

            return Execute<SettingsResponse>(request)?.Settings;
        }
    }
}


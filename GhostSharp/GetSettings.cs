using GhostSharp.Entities;
using RestSharp;

namespace GhostSharp
{
    public partial class GhostAPI
    {
        public Settings GetSettings()
        {
            var request = new RestRequest("settings", Method.GET);

            return Execute<SettingsResponse>(request)?.Settings;
        }
    }
}


using GhostSharp.Entities;
using RestSharp;

namespace GhostSharp
{
    public partial class GhostAPI
    {
        /// <summary>
        /// Get the settings for the blog, including title, description,
        /// code injected into the header or footer (if any), etc.
        /// </summary>
        /// <returns>The blog settings.</returns>
        public Settings GetSettings()
        {
            var request = new RestRequest("settings/", Method.GET);

            return Execute<SettingsResponse>(request)?.Settings;
        }
    }
}


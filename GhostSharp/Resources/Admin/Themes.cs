using GhostSharp.Entities;
using RestSharp;

namespace GhostSharp
{
    public partial class GhostAdminAPI
    {
        /// <summary>
        /// Upload a theme to the site
        /// </summary>
        /// <returns>Returns metadata about the theme.</returns>
        public Theme UploadTheme(ThemeRequest theme)
        {
            var request = new RestRequest("themes/upload/", Method.Post);

            if (theme.FilePath != null)
                request.AddFile("file", theme.FilePath, "application/zip");
            else
                request.AddFile("file", theme.File, theme.FileName, "application/zip");

            return Execute<ThemeResponse>(request).Themes[0];
        }

        /// <summary>
        /// Activate a theme with the given name on the site
        /// </summary>
        /// <returns>Returns metadata about the theme.</returns>
        public Theme ActivateTheme(string name)
        {
            var request = new RestRequest($"themes/{name}/activate/", Method.Put)
                .AddUrlSegment("themename", name);

            return Execute<ThemeResponse>(request).Themes[0];
        }
    }
}

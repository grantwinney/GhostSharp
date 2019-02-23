using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Response representing settings and any meta data
    /// </summary>
    public class SettingsResponse
    {
        /// <summary>
        /// Represents your publication's settings.
        /// </summary>
        public Settings Settings { get; set; }

        /// <summary>
        /// Meta data regarding the response.
        /// </summary>
        public Meta Meta { get; set; }
    }

    /// <summary>
    /// Represents your publication's settings.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// The title used to identify your publication around the web.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description used to identify your publication around the web.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The primary logo for your brand displayed across your theme.
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// A square, social icon used in the UI of your publication.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// An optional large background image for your site.
        /// </summary>
        public string CoverImage { get; set; }

        /// <summary>
        /// Your publication's Facebook page.
        /// </summary>
        public string Facebook { get; set; }

        /// <summary>
        /// Your publication's Twitter page.
        /// </summary>
        public string Twitter { get; set; }

        /// <summary>
        /// Your publication's language.
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// Your publication's time zone.
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// Your publication's navigation menu.
        /// </summary>
        public List<Navigation> Navigation { get; set; }

        /// <summary>
        /// Code injected into your publication's header.
        /// </summary>
        public string CodeInjectionHead { get; set; }

        /// <summary>
        /// Code injected into your publication's footer.
        /// </summary>
        public string CodeInjectionFoot { get; set; }
    }

    /// <summary>
    /// Represents a single menu item on your publication.
    /// </summary>
    public class Navigation
    {
        /// <summary>
        /// Menu label as visitors see it
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Menu URL (relative URL, such as /about-me)
        /// </summary>
        public string Url { get; set; }
    }
}

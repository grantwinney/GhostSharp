using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Represents your publication's settings.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// The title used to identify your publication around the web.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// The description used to identify your publication around the web.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The primary logo for your brand displayed across your theme.
        /// </summary>
        [JsonProperty("logo")]
        public string Logo { get; set; }

        /// <summary>
        /// A square, social icon used in the UI of your publication.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Accent color.
        /// </summary>
        [JsonProperty("accent_color")]
        public string AccentColor { get; set; }

        /// <summary>
        /// An optional large background image for your site.
        /// </summary>
        [JsonProperty("cover_image")]
        public string CoverImage { get; set; }

        /// <summary>
        /// Your publication's Facebook page.
        /// </summary>
        [JsonProperty("facebook")]
        public string Facebook { get; set; }

        /// <summary>
        /// Your publication's Twitter page.
        /// </summary>
        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        /// <summary>
        /// Your publication's language.
        /// </summary>
        [JsonProperty("lang")]
        public string Lang { get; set; }

        /// <summary>
        /// Your publication's time zone.
        /// </summary>
        [JsonProperty("timezone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Code injected into your publication's header.
        /// </summary>
        [JsonProperty("codeinjection_head")]
        public string CodeInjectionHead { get; set; }

        /// <summary>
        /// Code injected into your publication's footer.
        /// </summary>
        [JsonProperty("codeinjection_foot")]
        public string CodeInjectionFoot { get; set; }

        /// <summary>
        /// Your publication's navigation menu.
        /// </summary>
        [JsonProperty("navigation")]
        public List<Navigation> Navigation { get; set; }

        /// <summary>
        /// Your publication's navigation menu.
        /// </summary>
        [JsonProperty("secondary_navigation")]
        public List<Navigation> SecondaryNavigation { get; set; }

        /// <summary>
        /// Meta title.
        /// </summary>
        [JsonProperty("meta_title")]
        public string MetaTitle { get; set; }

        /// <summary>
        /// Meta description.
        /// </summary>
        [JsonProperty("meta_description")]
        public string MetaDescription { get; set; }

        /// <summary>
        /// OG image.
        /// </summary>
        [JsonProperty("og_image")]
        public string OgImage { get; set; }

        /// <summary>
        /// OG title.
        /// </summary>
        [JsonProperty("og_title")]
        public string OgTitle { get; set; }

        /// <summary>
        /// OG description.
        /// </summary>
        [JsonProperty("og_description")]
        public string OgDescription { get; set; }

        /// <summary>
        /// Twitter image.
        /// </summary>
        [JsonProperty("twitter_image")]
        public string TwitterImage { get; set; }

        /// <summary>
        /// Twitter title.
        /// </summary>
        [JsonProperty("twitter_title")]
        public string TwitterTitle { get; set; }

        /// <summary>
        /// Twitter description.
        /// </summary>
        [JsonProperty("twitter_description")]
        public string TwitterDescription { get; set; }

        /// <summary>
        /// Support email address. (i.e. noreply@docs.ghost.io)
        /// </summary>
        [JsonProperty("members_support_address")]
        public string MembersSupportAddress { get; set; }

        /// <summary>
        /// URL. (i.e. https://docs.ghost.io/)
        /// </summary>
        [JsonProperty("url")]
        public string URL { get; set; }
    }
}

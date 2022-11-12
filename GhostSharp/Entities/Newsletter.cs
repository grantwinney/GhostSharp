using GhostSharp.Attributes;
using Newtonsoft.Json;
using System;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Represents a newsletter
    /// </summary>
    public class Newsletter
    {
        /// <summary>
        /// The unique ID of the newsletter
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; set; }

        /// <summary>
        /// Public name for the newsletter.
        /// </summary>
        [Updateable]
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        /// Public description of the newsletter. (nullable)
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The reference to this newsletter that can be used in the newsletter option when sending a post via email
        /// </summary>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Denotes if the newsletter is active or archived (i.e. active, archived)
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// The sender name of the emails (nullable)
        /// </summary>
        [JsonProperty("sender_name")]
        public string SenderName { get; set; }

        /// <summary>
        /// The email from which to send emails. Requires validation. (nullable)
        /// </summary>
        [JsonProperty("sender_email")]
        public string SenderEmail { get; set; }

        /// <summary>
        /// The reply-to email address for sent emails. Can be either newsletter (= use sender_email) or support (use support email from Portal settings).
        /// </summary>
        [JsonProperty("sender_reply_to")]
        public string SenderReplyTo { get; set; }

        /// <summary>
        /// Visibility level for the newsletter. (i.e. members)
        /// </summary>
        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        /// <summary>
        /// Whether members should automatically subscribe to this newsletter on signup.
        /// </summary>
        [JsonProperty("subscribe_on_signup")]
        public bool SubscribeOnSignup { get; set; }

        /// <summary>
        /// Sort order.
        /// </summary>
        [JsonProperty("sort_order")]
        public int SortOrder { get; set; }

        /// <summary>
        /// Path to an image to show at the top of emails. Recommended size 1200x600. (nullable)
        /// </summary>
        [JsonProperty("header_image")]
        public string HeaderImage { get; set; }

        /// <summary>
        /// Show the site icon in emails.
        /// </summary>
        [JsonProperty("show_header_icon")]
        public bool ShowHeaderIcon { get; set; }

        /// <summary>
        /// Show the site name in emails.
        /// </summary>
        [JsonProperty("show_header_title")]
        public bool ShowHeaderTitle { get; set; }

        /// <summary>
        /// Show the newsletter name in emails.
        /// </summary>
        [JsonProperty("show_header_name")]
        public bool ShowHeaderName { get; set; }

        /// <summary>
        /// Title font style. Either serif or sans_serif.
        /// </summary>
        [JsonProperty("title_font_category")]
        public string TitleFontCategory { get; set; }

        /// <summary>
        /// Title alignment. (i.e. center)
        /// </summary>
        [JsonProperty("title_alignment")]
        public string TitleAlignment { get; set; }

        /// <summary>
        /// Show the post's feature image in emails.
        /// </summary>
        [JsonProperty("show_feature_image")]
        public bool ShowFeatureImage { get; set; }

        /// <summary>
        /// Body font style. Either serif or sans_serif.
        /// </summary>
        [JsonProperty("body_font_category")]
        public string BodyFontCategory { get; set; }

        /// <summary>
        /// Extra information or legal text to show in the footer of emails. Should contain valid HTML. (nullable)
        /// </summary>
        [JsonProperty("footer_content")]
        public string FooterContent { get; set; }

        /// <summary>
        /// Add a small Ghost badge in the footer.
        /// </summary>
        [JsonProperty("show_badge")]
        public bool ShowBadge { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Updated At
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// UUID
        /// </summary>
        [JsonProperty("uuid")]
        public string UUID { get; set; }
    }
}

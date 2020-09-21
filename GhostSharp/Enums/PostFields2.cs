using Newtonsoft.Json;
using System;

namespace GhostSharp.Enums
{
    /// <summary>
    /// More fields representing a Post
    /// </summary>

    [Flags]
    public enum PostFields2
    {
        /// <summary>
        /// Visibility (public, members, paid)
        /// </summary>
        [JsonProperty("visibility")]
        Visibility = 1,

        /// <summary>
        /// Send Email When Published (set by query parameter when publishing/scheduling)
        /// </summary>
        [JsonProperty("send_email_when_published")]
        SendEmailWhenPublished = 2,

        /// <summary>
        /// Email Subject
        /// </summary>
        [JsonProperty("email_subject")]
        EmailSubject = 4,

        /// <summary>
        /// Access (true if members is disabled; otherwise, set to currently logged in members' access)
        /// </summary>
        [JsonProperty("access")]
        Access = 8
    }
}

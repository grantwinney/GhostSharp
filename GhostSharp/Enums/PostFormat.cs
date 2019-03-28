using Newtonsoft.Json;
using System;

namespace GhostSharp.Enums
{
    /// <summary>
    /// The format of the page/post.
    /// </summary>
    [Flags]
    public enum PostFormat
    {
        /// <summary>
        /// HTML
        /// </summary>
        [JsonProperty("html")]
        Html = 1,

        /// <summary>
        /// Plain Text
        /// </summary>
        [JsonProperty("plaintext")]
        PlainText = 2,

        /// <summary>
        /// Mobile Doc
        /// </summary>
        [JsonProperty("mobiledoc")]
        MobileDoc = 4
    }
}

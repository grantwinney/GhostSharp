using GhostSharp.Attributes;
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
        [GhostField("html")]
        Html = 1,

        /// <summary>
        /// Plain Text
        /// </summary>
        [GhostField("plaintext")]
        PlainText = 2,

        /// <summary>
        /// Mobile Doc
        /// </summary>
        [GhostField("mobiledoc")]
        MobileDoc = 4
    }
}

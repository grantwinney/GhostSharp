using GhostSharp.Attributes;
using System;

namespace GhostSharp.Enums
{
    [Flags]
    public enum PostFormat
    {
        [GhostField("html")]
        Html = 1,

        [GhostField("plaintext")]
        PlainText = 2,

        [GhostField("mobiledoc")]
        MobileDoc = 4
    }
}

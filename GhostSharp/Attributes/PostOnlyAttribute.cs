using System;

namespace GhostSharp.Attributes
{
    /// <summary>
    /// Represents a field that should only be serialized for Posts, not Pages.
    /// </summary>
    internal class PostOnlyAttribute : Attribute
    {
    }
}

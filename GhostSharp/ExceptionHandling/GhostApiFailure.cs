using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Represents a collection of errors returned by the Ghost API.
    /// </summary>
    class GhostApiFailure
    {
        public List<GhostError> Errors { get; set; }
    }
}

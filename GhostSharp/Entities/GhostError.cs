using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Represents a single error returned by the Ghost API.
    /// </summary>
    public class GhostError
    {
        public string Message { get; set; }
        public string Context { get; set; }
        public string Type { get; set; }
        public List<GhostErrorDetail> Details { get; set; }
        public string Property { get; set; }
        public string Help { get; set; }
        public string Code { get; set; }
        public string Id { get; set; }

        public override string ToString() => $"{Message} {Context}.\r\n{string.Join("\r\n", Details ?? new List<GhostErrorDetail>())}\r\n({Id ?? ""}, {Help ?? ""}, {Code ?? ""})";
    }
}

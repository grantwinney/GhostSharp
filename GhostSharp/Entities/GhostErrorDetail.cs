using System.Collections.Generic;
using System.Linq;

namespace GhostSharp.Entities
{
    public class GhostErrorDetail
    {
        public string Keyword { get; set; }
        public string DataPath { get; set; }
        public string SchemaPath { get; set; }
        public Dictionary<string, object> Params { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            var p = string.Join(", ", (Params ?? new Dictionary<string, object>()).Select(x => $"{x.Key}:{x.Value}"));
            return $"({Keyword} error) {DataPath} {Message}: {p}";
        }
    }
}

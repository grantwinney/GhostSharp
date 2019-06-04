namespace GhostSharp.Entities
{
    /// <summary>
    /// Represents a single error returned by the Ghost API.
    /// </summary>
    public class GhostError
    {
        public string Message { get; set; }
        public string Context { get; set; }
        public string ErrorType { get; set; }

        public override string ToString() => $"{Message}";
    }
}

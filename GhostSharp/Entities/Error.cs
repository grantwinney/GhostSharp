using System.Collections.Generic;

namespace GhostSharp.Entities
{
    public class ApiFailure
    {
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        public string Message { get; set; }
        public string Context { get; set; }
        public string ErrorType { get; set; }
    }
}

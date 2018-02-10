using System;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// An exception representing a failure while using the API.
    /// </summary>
    public class GhostSharpException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GhostSharp.Entities.GhostSharpException"/> class
        /// with a message.
        /// </summary>
        /// <param name="message">An error message.</param>
        public GhostSharpException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GhostSharp.Entities.GhostSharpException"/> class
        /// with a message and exception.
        /// </summary>
        /// <param name="message">An error message.</param>
        /// <param name="innerException">The original exception.</param>
        public GhostSharpException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GhostSharp.Entities.GhostSharpException"/> class
        /// with a list of errors, deserialized from the Ghost API response.
        /// </summary>
        /// <param name="errors">A list of Ghost API errors.</param>
        public GhostSharpException(List<GhostApiError> errors)
        {
            message = String.Join(Environment.NewLine, errors);
            this.errors = errors;
        }

        readonly string message;
        /// <summary>
        /// Get a concatenation of all errors, if any, or the underlying Exception message if none.
        /// </summary>
        /// <value>The message.</value>
        public override string Message => message ?? base.Message;

        readonly List<GhostApiError> errors;
        /// <summary>
        /// Get the list of errors, if any. If no errors, then the list is empty (not null).
        /// </summary>
        /// <value>Returns a list of errors, or an empty list.</value>
        public List<GhostApiError> Errors => errors ?? new List<GhostApiError>();

        public override string ToString() => Message;
    }

    /// <summary>
    /// Represents a single error returned by the Ghost API.
    /// </summary>
    public class GhostApiError
    {
        public string Message { get; set; }
        public string Context { get; set; }
        public string ErrorType { get; set; }

        public override string ToString() => $"{Message}: {Context}";
    }
}

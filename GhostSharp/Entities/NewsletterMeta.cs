using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Holds meta information about the request, such as the 'page' of data that was requested.
    /// </summary>
    public class NewsletterMeta : Meta   
    {
        /// <summary>
        /// Holds updated sender_email value until the new address has been verified by recipient.
        /// </summary>
        /// <remarks>
        /// When updating the sender_email field, email verification is required before emails are sent from the new address.
        /// After updating the property, the sent_email_verification metadata property will be set, containing sender_email.
        /// The sender_email property will remain unchanged until the address has been verified by clicking the link that is sent to the address specified in sender_email.
        /// </remarks>
        /// <seealso cref="https://ghost.org/docs/admin-api/#sender-email-validation"/>
        [JsonProperty("sent_email_verification")]
        public List<string> SentEmailVerification { get; private set; }

        /// <summary>
        /// When opt_in_existing is set to true, this shows the number of members opted-in.
        /// </summary>
        [JsonProperty("opted_in_member_count")]
        public int OptedInMemberCount { get; private set; }
    }
}

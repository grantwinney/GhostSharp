using GhostSharp.Entities;
using RestSharp;
using System.Collections.Generic;

namespace GhostSharp
{
    public partial class GhostAdminAPI
    {
        /// <summary>
        /// Create a webhook
        /// </summary>
        /// <param name="webhook">Webhook to create</param>
        /// <returns>Returns the created webhook</returns>
        /// <seealso cref="https://ghost.org/docs/admin-api/#creating-a-webhook"/>
        public Webhook CreateWebhook(Webhook webhook)
        {
            var request = new RestRequest("webhooks/", Method.Post);
            request.AddJsonBody(new WebhookRequest { Webhooks = new List<Webhook> { webhook } });
            return Execute<WebhookRequest>(request).Webhooks[0];
        }

        /// <summary>
        /// Update a webhook
        /// </summary>
        /// <param name="webhook">Webhook to update</param>
        /// <returns>Returns the updated webhook</returns>
        /// <seealso cref="https://ghost.org/docs/admin-api/#updating-a-webhook"/>
        public Webhook UpdateWebhook(Webhook webhook)
        {
            var request = new RestRequest($"webhooks/{webhook.ID}/", Method.Put);
            request.AddJsonBody(new WebhookRequest { Webhooks = new List<Webhook> { webhook } });
            return Execute<WebhookRequest>(request).Webhooks[0];
        }

        /// <summary>
        /// Delete a webhook
        /// </summary>
        /// <param name="webhook">Webhook to delete</param>
        /// <returns>Returns True if delete succeeds; otherwise False</returns>
        /// <seealso cref="https://ghost.org/docs/admin-api/#deleting-a-webhook"/>
        public bool DeleteWebhook(string webhookID)
        {
            var request = new RestRequest($"webhooks/{webhookID}/", Method.Delete);
            return Execute(request);
        }
    }
}

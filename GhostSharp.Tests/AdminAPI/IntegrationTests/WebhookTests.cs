using GhostSharp.Entities;
using NUnit.Framework;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class WebhookTests : TestBase
    {
        private GhostAdminAPI auth;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);
        }

        [Test]
        public void CreateUpdateDeleteWebhook_Succeeds()
        {
            var webhook = new Webhook
            {
                Event = "post.added",
                TargetURL = "https://example.com",
            };

            var createResponse = auth.CreateWebhook(webhook);

            Assert.IsNotNull(createResponse.APIVersion);  // Seems to default, when not passed
            Assert.AreEqual(webhook.Event, createResponse.Event);
            Assert.AreEqual(webhook.TargetURL, createResponse.TargetURL);
            Assert.IsNull(createResponse.Name);

            // In the UI, webhooks are created under specific integrations, and associated with them.
            // Yet in the API, it ignores the integration_id, if any, and seems to just associate
            //   the new webhook with a random integration (always the same one, but why that one?).
            Assert.IsNotNull(createResponse.IntegrationID);

            createResponse.Event = "post.published.edited";
            createResponse.Name = "integration test";

            var updatedResponse = auth.UpdateWebhook(createResponse);

            Assert.AreEqual(createResponse.Event, updatedResponse.Event);
            Assert.AreEqual(createResponse.Name, updatedResponse.Name);

            var deletedResponse = auth.DeleteWebhook(updatedResponse.ID);

            Assert.IsTrue(deletedResponse);
        }
    }
}

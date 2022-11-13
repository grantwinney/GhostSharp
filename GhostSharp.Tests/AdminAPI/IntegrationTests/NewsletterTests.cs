using GhostSharp.Entities;
using GhostSharp.QueryParams;
using NUnit.Framework;
using System;
using System.Linq;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class NewsletterTests : TestBase
    {
        private GhostAdminAPI auth;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);
        }

        [Test]
        public void GetNewsletters_ReturnsNewsletters()
        {
            var newsletters = auth.GetNewsletters();

            Assert.GreaterOrEqual(newsletters.Newsletters.Count, 1);

            var defaultNewsletter = newsletters.Newsletters.Single(x => x.Slug == "default-newsletter");

            Assert.AreEqual("628a2e48c0c4b7124bf26d9f", defaultNewsletter.ID);
            Assert.AreEqual("a99354a3-4f07-44fd-8690-21f455a6d727", defaultNewsletter.UUID);
        }

        [Test]
        public void GetNewsletters_ReturnsAllNewsletters_WhenNoLimitTrue()
        {
            var newsletters = auth.GetNewsletters(new NewsletterQueryParams { NoLimit = true });

            Assert.IsNotEmpty(newsletters.Newsletters);
        }

        [Test]
        public void GetNewsletters_ReturnsAllNewsletters_WhenLimitMoreThanZero()
        {
            var newsletters = auth.GetNewsletters(new NewsletterQueryParams { Limit = 5 });

            Assert.IsNotEmpty(newsletters.Newsletters);
        }

        [Test]
        public void GetNewsletters_ReturnsSubsetOfNewsletters_WhenLimitIsZero()
        {
            var newsletters = auth.GetNewsletters(new NewsletterQueryParams { Limit = 0 });

            Assert.IsNotEmpty(newsletters.Newsletters);
        }

        [Test]
        public void CreateNewsletter_Succeeds()
        {
            var newsletter = new Newsletter
            {
                Name = $"integration test newsletter ({Guid.NewGuid()})",
                Description = "integration test newsletter description",
                SenderReplyTo = "newsletter",
                Status = "archived",
                SubscribeOnSignup = false,
                ShowHeaderIcon = true,
                ShowHeaderTitle = true,
                ShowHeaderName = true,
                TitleFontCategory = "sans_serif",
                TitleAlignment = "center",
                ShowFeatureImage = true,
                BodyFontCategory = "sans_serif",
                ShowBadge = true
            };

            var response = auth.CreateNewsletter(newsletter);

            Assert.AreNotEqual("", response.ID);
            Assert.AreEqual(newsletter.Name, response.Name);
            Assert.AreEqual(newsletter.Description, response.Description);
            Assert.AreEqual(newsletter.Status, response.Status);
        }

        [Test]
        public void UpdateNewsletter_Succeeds()
        {
            var newsletter = auth.GetNewsletters().Newsletters.Single(x => x.ID == "636fe8555ea61a0793d1250d");

            var updatedDescription = $"integration test newsletter description {Guid.NewGuid()}";

            Assert.AreNotEqual(updatedDescription, newsletter.Description);

            newsletter.Description = updatedDescription;

            var response = auth.UpdateNewsletter(newsletter);

            Assert.AreEqual(updatedDescription, newsletter.Description);
        }
    }
}

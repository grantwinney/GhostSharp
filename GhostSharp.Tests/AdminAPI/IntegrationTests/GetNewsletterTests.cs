using GhostSharp.QueryParams;
using NUnit.Framework;
using System.Linq;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class GetNewsletterTests : TestBase
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

    }
}

using GhostSharp.QueryParams;
using NUnit.Framework;
using System.Linq;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class GetTiersIntgTests : TestBase
    {
        private GhostAdminAPI auth;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);
        }

        [Test]
        public void GetTiers_ReturnsTiers()
        {
            var tiersResponse = auth.GetTiers();

            Assert.GreaterOrEqual(tiersResponse.Tiers.Where(x => x.Active).Count(), 2);

            var freeTier = tiersResponse.Tiers.Single(x => x.Type == "free");

            Assert.AreEqual("Free", freeTier.Name);
            Assert.True(freeTier.Active);
          
            Assert.IsNotNull(freeTier.ID);
            Assert.IsNotNull(freeTier.Slug);
            Assert.IsNotNull(freeTier.Visibility);
            Assert.IsNotNull(freeTier.WelcomePageURL);
            Assert.AreEqual(0, freeTier.TrialDays);

            // Assert.IsEmpty(freeTier.Benefits);  // Benefits are returned even without the 'include' query param
            Assert.IsNotEmpty(freeTier.Benefits);
            Assert.AreEqual("Access to all public posts", freeTier.Benefits[0]);

            Assert.IsNull(freeTier.MonthlyPrice);
            Assert.IsNull(freeTier.YearlyPrice);
            Assert.IsNull(freeTier.Currency);

            var paidTier = tiersResponse.Tiers.Single(x => x.Name == "Ghost Subscription");

            Assert.AreEqual("Ghost Subscription", paidTier.Name);
            Assert.True(paidTier.Active);

            Assert.IsNotNull(paidTier.ID);
            Assert.IsNotNull(paidTier.Slug);
            Assert.IsNotNull(paidTier.Visibility);
            Assert.IsNotNull(paidTier.WelcomePageURL);
            Assert.AreEqual(0, paidTier.TrialDays);

            Assert.IsEmpty(paidTier.Benefits);
            Assert.AreEqual(500, paidTier.MonthlyPrice); // $5
            Assert.AreEqual(5000, paidTier.YearlyPrice); // $50
            Assert.AreEqual("USD", paidTier.Currency);
        }

        [Test]
        public void GetTiersWithIncludes_ReturnsTiersWithExtraData()
        {
            var tiersResponse = auth.GetTiers(new TierQueryParams
            {
                IncludeBenefits = true,
                IncludeMonthlyPrice = true,
                IncludeYearlyPrice = true
            });

            Assert.GreaterOrEqual(tiersResponse.Tiers.Where(x => x.Active).Count(), 2);

            var freeTier = tiersResponse.Tiers.Single(x => x.Type == "free");

            Assert.AreEqual("Free", freeTier.Name);
            Assert.True(freeTier.Active);

            Assert.IsNotEmpty(freeTier.Benefits);
            Assert.AreEqual("Access to all public posts", freeTier.Benefits[0]);

            Assert.IsNull(freeTier.MonthlyPrice);
            Assert.IsNull(freeTier.YearlyPrice);
            Assert.IsNull(freeTier.Currency);
        }
    }
}

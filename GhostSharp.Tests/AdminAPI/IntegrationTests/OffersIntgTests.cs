using GhostSharp.Entities;
using NUnit.Framework;
using System;
using System.Linq;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class OffersIntgTests : TestBase
    {
        private GhostAdminAPI auth;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);
        }

        [Test]
        public void GetOffers_ReturnsOffers()
        {
            var offerResponse = auth.GetOffers();

            Assert.IsNotEmpty(offerResponse.Offers);
        }

        [Test]
        public void CreateOffer_Succeeds()
        {
            var testName = $"test-offer-{Guid.NewGuid().ToString().Substring(0, 18)}";

            var offer = new Offer
            {
                Name = testName,
                Code = testName,
                Cadence = "year",
                Duration = "once",
                Amount = 12,
                Type = "percent",
                Tier = new Tier { ID = "6080dbea66854a2073a68709" }
            };

            var response = auth.CreateOffer(offer);

            Assert.AreEqual(testName, response.Name);
            Assert.AreEqual(offer.Code, response.Code);
            Assert.AreEqual(offer.Cadence, response.Cadence);
            Assert.AreEqual(offer.Duration, response.Duration);
            Assert.AreEqual(offer.Amount, response.Amount);
            Assert.AreEqual(offer.Type, response.Type);
            Assert.AreEqual(offer.Tier.ID, response.Tier.ID);
        }

        // ??? Updates to offers and tiers are throwing a 504 gateway exception

        //[Test]
        //public void UpdateOffer_Succeeds()
        //{
        //    var offer = auth.GetOffers().Offers.Single(x => x.ID == "636fd5c55ea61a0793d123d8");

        //    var testName = $"test-offer-{Guid.NewGuid().ToString().Substring(0, 18)}";

        //    Assert.AreNotEqual(testName, offer.Name);

        //    var updatedOffer = auth.UpdateOffer(offer);   // Throws 504.. why??

        //    Assert.AreEqual(testName, updatedOffer.Name);
        //}
    }
}

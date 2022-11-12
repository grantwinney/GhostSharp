using GhostSharp.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class CreateTiersIntgTests : TestBase
    {
        private GhostAdminAPI auth;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);
        }

        [Test]
        public void CreateTier_Succeeds()  // CreateAndUpdateTier_Succeeds
        {
            var tier = new Tier
            {
                Name = $"integration-test-{Guid.NewGuid()}",
                Description = "test description",
                WelcomePageURL = "/test-integration",
                Visibility = "none",
                Type = "free",
                MonthlyPrice = 1000,
                YearlyPrice = 10000,
                Currency = "usd",
                Benefits = new List<string> {
                    "Benefit 1",
                    "Benefit 2"
                },
                Active = false,
            };

            var createResponse = auth.CreateTier(tier);

            Assert.IsNotNull(createResponse.ID);
            Assert.AreEqual(2, createResponse.Benefits.Count);
            Assert.AreEqual(tier.Name, createResponse.Slug);
            Assert.AreEqual(1000, createResponse.MonthlyPrice);
            Assert.AreEqual(10000, createResponse.YearlyPrice);
            Assert.AreNotEqual("free", createResponse.Type);  // ignored?
            Assert.IsTrue(createResponse.Active);  // New tiers are always set as active when created

            //createResponse.Type = "free";
            //createResponse.MonthlyPrice = 1200;
            //createResponse.YearlyPrice = 11000;
            //createResponse.Active = false;

            //var updateResponse = auth.UpdateTier(createResponse);  // throws a 504 gateway ex - why?

            //Assert.AreEqual(10, createResponse.MonthlyPrice);
            //Assert.IsTrue(createResponse.Active);
        }
    }
}

using GhostSharp.Entities;
using GhostSharp.Enums;
using GhostSharp.QueryParams;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class GetSiteIntgTests : TestBase
    {
        [Test]
        public void GetSite_ReturnsSiteInfo_WhenAuthorized()
        {
            var auth = new GhostAdminAPI(Host, ValidAdminApiKey);

            var site = auth.GetSite();

            Assert.AreEqual(SiteTitle, site.Title);
            Assert.AreEqual(Host, site.Url);
            Assert.IsNotNull(site.Version);
        }
    }
}

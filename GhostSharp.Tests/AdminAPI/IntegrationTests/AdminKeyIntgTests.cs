using GhostSharp.Enums;
using NUnit.Framework;
using System;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class AdminKeyIntgTests : TestBase
    {
        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GhostAdminAPI_AlwaysThrowsException_WhenKeyIsInvalidFormat(ExceptionLevel exceptionLevel)
        {
            var ex = Assert.Throws<ArgumentException>(() => new GhostAdminAPI(Host, InvalidFormattedApiKey) { ExceptionLevel = exceptionLevel });
            Assert.AreEqual("The Admin API Key should consist of an ID and Secret, separated by a colon.", ex.Message);
        }
    }
}

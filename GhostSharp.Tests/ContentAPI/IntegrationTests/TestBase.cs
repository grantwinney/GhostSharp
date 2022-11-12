using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostSharp.Tests.ContentAPI.IntegrationTests
{
    public class TestBase
    {
        // The base host URL of your Ghost site, like https://your-blog.com
        protected static string Host = "https://grantwinney.com/";
        protected static string SiteTitle = "G.Winney 🖱";

        protected static string ValidContentApiKey = Environment.GetEnvironmentVariable("CONTENT_API_KEY");

        protected static string ValidPost1Id = "5e90d3eb1318020e53971c38";
        protected static string ValidPost1Slug = "using-the-ip-geolocation-api-to-find-info-about-an-ip-address";
        protected static string ValidPost1Title = "Using the IP Geolocation API to find info about an IP address";
        protected static string ValidPost1Url = "https://grantwinney.com/using-the-ip-geolocation-api-to-find-info-about-an-ip-address/";
        protected static string ValidPost1Author = "1";
        protected static string ValidPost1PrimaryTag = "5e90d3e71318020e539719f1";
        protected static string ValidPost1CodeInjectionHeader = "<!-- sample code injection header -->";
        protected static string ValidPost1CodeInjectionFooter = "<!-- sample code injection footer -->";

        protected static string ValidPage1Id = "5e90d3eb1318020e53971b5b";
        protected static string ValidPage1Slug = "about";
        protected static string ValidPage1Url = "https://grantwinney.com/about/";
        protected static string ValidPage1Author = "1";

        protected static string ValidAuthor1Id = "1";
        protected static string ValidAuthor1Slug = "grant";
        protected static string ValidAuthor1Name = "Grant Winney";
        protected static string ValidAuthor1Url = "https://grantwinney.com/author/grant/";

        protected static string ValidAuthor2Id = "5e90d3e61318020e539719de";
        protected static string ValidAuthor2Slug = "grant2";
        protected static string ValidAuthor2Name = "Grant Winney";
        protected static string ValidAuthor2Url = "https://grantwinney.com/author/grant2/";

        protected static string ValidAuthorWithNoPublishedPostsSlug = "john";

        protected static string ValidTag1Id = "5e90d3e71318020e53971b03";
        protected static string ValidTag1Slug = "gs-test";
        protected static string ValidTag1Name = "gs-test";
        protected static string ValidTag1Description = "~~~~ Zet another throwaway tag used for testing purposes.";
        protected static string ValidTag1FeatureImage = "https://grantwinney.com/content/images/2019/02/testing-4.jpeg";
        protected static string ValidTag1Visibility = "public";
        protected static string ValidTag1MetaTitle = "sample meta title";
        protected static string ValidTag1MetaDescription = "sample meta description";
        protected static int ValidTag1PostCount = 1;
        protected static string ValidTag1Url = "https://grantwinney.com/tag/gs-test/";

        protected static string ValidTag2Id = "5e90d3e71318020e53971b04";
        protected static string ValidTag2Slug = "gs-test-2";
        protected static string ValidTag2Name = "gs-test-2";
        protected static string ValidTag2Description = "~~~~ Yet another throwaway tag used for testing purposes.";

        protected static string ValidTagWithNoPublishedPostsSlug = "gs-test-4";

        protected static string InvalidApiKey = "aaaaaaaaaaaaaaaaaaaaaaaaaa";
        protected static string InvalidPostId = "aaaaaaaaaaaaaaaaaaaaaaaa";
        protected static string InvalidPostSlug = "invalid-slug-value";
        protected static string InvalidPageId = "aaaaaaaaaaaaaaaaaaaaaaaa";
        protected static string InvalidPageSlug = "invalid-slug-value";
        protected static string InvalidAuthorId = "aaaaaaaaaaaaaaaaaaaaaaaa";
        protected static string InvalidAuthorSlug = "invalid-slug-value";
        protected static string InvalidTagId = "aaaaaaaaaaaaaaaaaaaaaaaa";
        protected static string InvalidTagSlug = "invalid-slug-value";

        private readonly List<string> testValues = new List<string> { ValidContentApiKey, ValidPost1Id, ValidPost1Slug,
                                                                      ValidAuthor1Id, ValidAuthor1Slug, ValidTag1Id, ValidTag1Slug};

        protected TestBase()
        {
            if (testValues.Any(string.IsNullOrWhiteSpace))
                throw new ApplicationException("Fill in test values in TestBase before running tests.");
        }
    }
}

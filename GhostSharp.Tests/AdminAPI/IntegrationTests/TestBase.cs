﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    public class TestBase
    {
        // The base host URL of your Ghost site, like https://your-blog.com
        protected static string Host = "https://grantwinney.com/";
        protected static string SiteTitle = "Grant Winney";

        protected static string ValidAdminApiKey = Environment.GetEnvironmentVariable("ADMIN_API_KEY");

        protected static string ValidPost1Id = "5d707c0b0d116604e3965eb1";
        protected static string ValidPost1Slug = "using-the-ip-geolocation-api-to-find-info-about-an-ip-address";
        protected static string ValidPost1Title = "Using the IP Geolocation API to find info about an IP address";
        protected static string ValidPost1Url = "https://grantwinney.com/using-the-ip-geolocation-api-to-find-info-about-an-ip-address/";
        protected static string ValidPost1Author = "1";
        protected static string ValidPost1PrimaryTag = "5d707c090d116604e3965c87";

        protected static string ValidPage1Id = "5d707c0b0d116604e3965dd4";
        protected static string ValidPage1Slug = "about";
        protected static string ValidPage1Url = "https://grantwinney.com/about/";
        protected static string ValidPage1Author = "1";

        protected static string ValidAuthor1Id = "1";
        protected static string ValidAuthor1Slug = "grant";
        protected static string ValidAuthor1Name = "Grant Winney";
        protected static string ValidAuthor1Url = "https://grantwinney.com/author/grant/";
        protected static string ValidAuthor1Email = Environment.GetEnvironmentVariable("VALID_AUTHOR_1_EMAIL_ADDRESS") ?? "test_email_1";

        protected static string ValidAuthor2Id = "5d707c090d116604e3965c77";
        protected static string ValidAuthor2Slug = "grant2";
        protected static string ValidAuthor2Name = "Grant Winney";
        protected static string ValidAuthor2Url = "https://grantwinney.com/author/grant2/";
        protected static string ValidAuthor2Email = Environment.GetEnvironmentVariable("VALID_AUTHOR_2_EMAIL_ADDRESS") ?? "test_email_2";

        protected static string ValidAuthorWithNoPublishedPostsSlug = "john";

        protected static string ValidTag1Id = "5d707c090d116604e3965d99";
        protected static string ValidTag1Slug = "gs-test";
        protected static string ValidTag1Name = "gs-test";
        protected static string ValidTag1Description = "~~~~ Zet another throwaway tag used for testing purposes.";
        protected static string ValidTag1FeatureImage = "https://grantwinney.com/content/images/2019/02/testing-4.jpeg";
        protected static string ValidTag1Visibility = "public";
        protected static string ValidTag1MetaTitle = "sample meta title";
        protected static string ValidTag1MetaDescription = "sample meta description";
        protected static int ValidTag1PostCount = 6;
        protected static string ValidTag1Url = "https://grantwinney.com/tag/gs-test/";

        protected static string ValidTag2Id = "5d707c090d116604e3965d9a";
        protected static string ValidTag2Slug = "gs-test-2";
        protected static string ValidTag2Name = "gs-test-2";
        protected static string ValidTag2Description = "~~~~ Yet another throwaway tag used for testing purposes.";

        protected static string ValidTagWithNoPublishedPostsSlug = "gs-test-4";

        protected static string InvalidApiKey = "5b2938a839e92877c5910cd9:eb3209a64eddc72268ed4e898a9f181bca42c30cbe5752e64770fb16b9ca50a4";
        protected static string InvalidFormattedApiKey = "aaaaaaaaaaaaaaaaaaaaaaaaaa";
        protected static string InvalidPostId = "aaaaaaaaaaaaaaaaaaaaaaaa";
        protected static string InvalidPostSlug = "invalid-slug-value";
        protected static string InvalidPageId = "aaaaaaaaaaaaaaaaaaaaaaaa";
        protected static string InvalidPageSlug = "invalid-slug-value";
        protected static string InvalidAuthorId = "aaaaaaaaaaaaaaaaaaaaaaaa";
        protected static string InvalidAuthorSlug = "invalid-slug-value";
        protected static string InvalidTagId = "aaaaaaaaaaaaaaaaaaaaaaaa";
        protected static string InvalidTagSlug = "invalid-slug-value";

        private List<string> testValues = new List<string> { ValidAdminApiKey, ValidPost1Id, ValidPost1Slug,
                                                             ValidAuthor1Id, ValidAuthor1Slug, ValidTag1Id, ValidTag1Slug};

        protected TestBase()
        {
            if (testValues.Any(string.IsNullOrWhiteSpace))
                throw new ApplicationException("Fill in test values in TestBase before running tests.");
        }
    }
}

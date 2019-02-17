using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostSharpTests
{
    public class TestBase
    {
        // The base host URL of your Ghost site, like https://your-blog.com
        public static string Host = "https://grantwinney.com";
        public static string SiteTitle = "Grant Winney";

        public static string ValidApiKey = "67674698fdcb50d7f7896981d0";
        public static string ValidPostId = "5c60e815afe8720651f1834d";
        public static string ValidPostSlug = "using-the-ip-geolocation-api-to-find-info-about-an-ip-address";
        public static string ValidAuthorId = "1";
        public static string ValidAuthorSlug = "grant";
        public static string ValidTagId = "5a32a221292eb46fca0fbe38";
        public static string ValidTagSlug = "15-apis-in-15-days";

        public static string InvalidApiKey = "{1D356678-846C-4C1F-97B3-3B87C008A2B2}";
        public static string InvalidPostId = "{1D356678-846C-4C1F-97B3-3B87C008A2B2}";
        public static string InvalidPostSlug = "{1D356678-846C-4C1F-97B3-3B87C008A2B2}";
        public static string InvalidAuthorId = "{1D356678-846C-4C1F-97B3-3B87C008A2B2}";
        public static string InvalidAuthorSlug = "{1D356678-846C-4C1F-97B3-3B87C008A2B2}";
        public static string InvalidTagId = "{1D356678-846C-4C1F-97B3-3B87C008A2B2}";
        public static string InvalidTagSlug = "{1D356678-846C-4C1F-97B3-3B87C008A2B2}";

        private List<string> testValues = new List<string> { ValidApiKey, ValidPostId, ValidPostSlug,
                                                             ValidAuthorId, ValidAuthorSlug, ValidTagId, ValidTagSlug};

        public TestBase()
        {
            if (testValues.Any(string.IsNullOrWhiteSpace))
                throw new ApplicationException("Fill in test values in TestBase before running tests.");
        }
    }
}

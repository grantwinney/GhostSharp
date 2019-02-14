using System;
using System.Linq;

namespace GhostSharpTests
{
    public class TestBase
    {
        /// <summary>
        /// The base host URL of your Ghost site, like https://your-blog.com
        /// </summary>
        public static string Host = "https://grantwinney.com";

        /// <summary>
        /// Content API key
        /// </summary>
        /// <remarks>
        /// Instructions on generating a key: https://docs.ghost.org/api/content/#key
        /// </remarks>
        public static string ValidApiKey = "";
        public static string ValidPostId = "";
        public static string ValidPostSlug = "";
        public static string ValidAuthorId = "1";
        public static string ValidAuthorSlug = "";
        public static string ValidTagId = "";
        public static string ValidTagSlug = "";

        public static string InvalidApiKey = "{1D356678-846C-4C1F-97B3-3B87C008A2B2}";
        public static string InvalidPostId = "{1D356678-846C-4C1F-97B3-3B87C008A2B2}";
        public static string InvalidPostSlug = "{1D356678-846C-4C1F-97B3-3B87C008A2B2}";
        public static string InvalidAuthorId = "{1D356678-846C-4C1F-97B3-3B87C008A2B2}";
        public static string InvalidAuthorSlug = "{1D356678-846C-4C1F-97B3-3B87C008A2B2}";
        public static string InvalidTagId = "{1D356678-846C-4C1F-97B3-3B87C008A2B2}";
        public static string InvalidTagSlug = "{1D356678-846C-4C1F-97B3-3B87C008A2B2}";

        public TestBase()
        {
            var testValues = new[] { ValidApiKey, ValidPostId, ValidPostSlug,
                                     ValidAuthorId, ValidAuthorSlug, ValidTagId, ValidTagSlug};

            if (testValues.Any(x => string.IsNullOrWhiteSpace(x)))
                throw new ApplicationException("Fill in test values in TestBase before running tests.");
        }
    }
}

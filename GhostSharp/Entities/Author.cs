using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Response representing authors and any meta data.
    /// </summary>
    public class AuthorResponse
    {
        /// <summary>
        /// Collection of authors.
        /// </summary>
        public List<Author> Authors { get; set; }

        /// <summary>
        /// Meta data regarding the response.
        /// </summary>
        public Meta Meta { get; set; }
    }

    /// <summary>
    /// Represents an Author.
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Slug
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Profile Image
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// Cover Image
        /// </summary>
        public string CoverImage { get; set; }

        /// <summary>
        /// Biography
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Website
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Facebook
        /// </summary>
        public string Facebook { get; set; }

        /// <summary>
        /// Twitter
        /// </summary>
        public string Twitter { get; set; }

        /// <summary>
        /// Meta Title
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Meta Description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Post Count
        /// </summary>
        public Count Count { get; set; }

        /// <summary>
        /// Profile URL
        /// </summary>
        public string Url { get; set; }
    }
}

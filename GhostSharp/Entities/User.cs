using System.Collections.Generic;

namespace GhostSharp.Entities
{
    public class UserResponse
    {
        public List<User> Users { get; set; }
        public Meta Meta { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string ProfileImage { get; set; }
        public string CoverImage { get; set; }
        public string Bio { get; set; }
        public string Website { get; set; }
        public string Location { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Accessibility { get; set; }
        public string Locale { get; set; }
        public string Visibility { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Tour { get; set; }
    }
}

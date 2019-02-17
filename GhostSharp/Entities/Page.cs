using System.Collections.Generic;

namespace GhostSharp.Entities
{
    public class PageResponse
    {
        public List<Page> Pages { get; set; }
        public Meta Meta { get; set; }
    }

    public class Page : Post
    {
    }
}

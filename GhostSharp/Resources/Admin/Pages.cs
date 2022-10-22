using GhostSharp.ContractResolvers;
using GhostSharp.Entities;
using GhostSharp.QueryParams;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace GhostSharp
{
    public partial class GhostAdminAPI
    {
        /// <summary>
        /// Get a collection of published pages,
        /// including meta data about pagination so you can retrieve data in chunks.
        /// </summary>
        /// <returns>The pages.</returns>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public new PageResponse GetPages(PostQueryParams queryParams = null)
        {
            return base.GetPages(queryParams);
        }

        /// <summary>
        /// Get a specific page based on its ID.
        /// </summary>
        /// <returns>The page matching the given ID. By default, returns MobileDoc format and author and tag data.</returns>
        /// <param name="id">The ID of the page to retrieve.</param>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public new Post GetPageById(string id, PostQueryParams queryParams = null)
        {
            return base.GetPageById(id, queryParams);
        }

        /// <summary>
        /// Get a specific page based on its slug.
        /// </summary>
        /// <returns>The page matching the given slug. By default, returns MobileDoc format and author and tag data.</returns>
        /// <param name="slug">The slug of the page to retrieve.</param>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public new Post GetPageBySlug(string slug, PostQueryParams queryParams = null)
        {
            return base.GetPageBySlug(slug, queryParams);
        }

        /// <summary>
        /// Create a page
        /// </summary>
        /// <param name="page">Page to create</param>
        /// <returns>Returns the same page, along with whatever other data Ghost appended to it (like default values)</returns>
        public Post CreatePage(Post page)
        {
            var serializedPage = JsonConvert.SerializeObject(
                new PageRequest { Pages = new List<Post> { page } },
                new JsonSerializerSettings
                {
                    ContractResolver = CreatePageContractResolver.Instance,
                    NullValueHandling = NullValueHandling.Ignore
                }
             );

            var request = new RestRequest($"pages/", Method.Post);
            request.AddJsonBody(serializedPage);

            // To use HTML as the source for your content instead of mobiledoc, use the source parameter.
            // Ref: https://ghost.org/docs/admin-api/#source-html
            if (!string.IsNullOrEmpty(page.Html))
                request.AddQueryParameter("source", "html");

            return Execute<PageRequest>(request).Pages[0];
        }

        /// <summary>
        /// Update a page
        /// </summary>
        /// <param name="post">Page to update</param>
        /// <returns>Returns the updated page</returns>
        public Post UpdatePage(Post updatedPage)
        {
            // Per the docs, the UpdatedAt field is used to avoid collision detection
            // If an update fails, it might be that someone updated it more recently on site,
            // and you should re-get it and re-apply your changes to it... otherwise you
            // risk unintentionally overwriting later changes on the site.
            // Ref: https://ghost.org/docs/admin-api/#updating-a-post

            var serializedPage = JsonConvert.SerializeObject(
               new PageRequest { Pages = new List<Post> { updatedPage } },
               new JsonSerializerSettings { ContractResolver = UpdatePageContractResolver.Instance }
            );

            var request = new RestRequest($"pages/{updatedPage.Id}/", Method.Put);
            request.AddJsonBody(serializedPage);

            // To use HTML as the source for your content instead of mobiledoc, use the source parameter.
            // Ref: https://ghost.org/docs/admin-api/#source-html
            if (!string.IsNullOrEmpty(updatedPage.Html))
                request.AddQueryParameter("source", "html");

            return Execute<PageRequest>(request).Pages[0];
        }

        /// <summary>
        /// Delete a page
        /// </summary>
        /// <param name="id">The ID of the page to delete</param>
        /// <returns>True if the delete succeeded; otherwise False</returns>
        public bool DeletePage(string id)
        {
            var request = new RestRequest($"pages/{id}/", Method.Delete);

            return Execute(request);
        }
    }
}

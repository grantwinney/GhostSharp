using GcmSharp.Serialization;
using GhostSharp.Entities;
using GhostSharp.QueryParams;
using RestSharp;

namespace GhostSharp
{
    public partial class GhostAdminAPI
    {
        /// <summary>
        /// Get a collection of published posts,
        /// including meta data about pagination so you can retrieve data in chunks.
        /// </summary>
        /// <returns>The posts.</returns>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public new PostResponse GetPosts(PostQueryParams queryParams = null)
        {
            return base.GetPosts(queryParams);
        }

        /// <summary>
        /// Get a specific post based on its ID.
        /// </summary>
        /// <returns>The post matching the given ID. By default, returns MobileDoc format and author and tag data.</returns>
        /// <param name="id">The ID of the post to retrieve.</param>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public new Post GetPostById(string id, PostQueryParams queryParams = null)
        {
            return base.GetPostById(id, queryParams);
        }

        /// <summary>
        /// Get a specific post based on its slug.
        /// </summary>
        /// <returns>The post matching the given slug. By default, returns MobileDoc format and author and tag data.</returns>
        /// <param name="slug">The slug of the post to retrieve.</param>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public new Post GetPostBySlug(string slug, PostQueryParams queryParams = null)
        {
            return base.GetPostBySlug(slug, queryParams);
        }

        public PostRequest CreatePost(PostRequest post)
        {
            var request = new RestRequest($"posts/", Method.POST, DataFormat.Json);
            request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            request.AddJsonBody(post);
            return Execute<PostRequest>(request);
        }

        //public Post UpdatePost(Post post)
        //{
        //    var request = new RestRequest($"posts/{post.Id}/", Method.PUT);
        //    ApplyPostQueryParams(request, queryParams);
        //    return Execute<PostResponse>(request)?.Posts?.Single();

        //}

        //public void DeletePost(string id)
        //{
        //    var request = new RestRequest($"posts/{id}/", Method.DELETE);
        //    return Execute<PostResponse>(request)?.Posts?.Single();
        //}
    }
}


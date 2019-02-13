using GhostSharp.QueryParams;
using RestSharp;

namespace GhostSharp
{
    public partial class GhostAPI
    {
        /// <summary>
        /// Delete a specific post based on its ID.
        /// </summary>
        /// <returns>True if the request was successful; otherwise False.</returns>
        /// <param name="id">The ID of the post to delete.</param>
        public bool DeletePostById(string id)
        {
            var request = new RestRequest($"posts/{id}", Method.DELETE);

            AppendSecurity(request);

            return Execute(request);
        }

        /// <summary>
        /// Delete a post based on its Slug (aka Url).
        /// </summary>
        /// <returns>True if the request was successful; otherwise False.</returns>
        /// <remarks>
        /// Performs a GET request first, to get the post ID, as the Ghost API only allows deletes by ID.
        /// The Slug is labeled "Post Url" in the Ghost admin area.
        /// </remarks>
        /// <param name="slug">The Slug of the post to delete.</param>
        public bool DeletePostBySlug(string slug)
        {
            var post = GetPostBySlug(slug, new PostQueryParams { Status = "all" });

            return DeletePostById(post.Id);
        }

        /// <summary>
        /// Delete a tag based on its ID.
        /// </summary>
        /// <returns>True if the request was successful; otherwise False.</returns>
        /// <param name="id">The ID of the tag to delete.</param>
        public bool DeleteTagById(string id)
        {
            var request = new RestRequest($"tags/{id}", Method.DELETE);

            AppendSecurity(request);

            return Execute(request);
        }

        /// <summary>
        /// Delete a tag based on its Slug (aka Url).
        /// </summary>
        /// <returns>True if the request was successful; otherwise False.</returns>
        /// <remarks>
        /// Performs a GET request first, to get the tag ID, as the Ghost API only allows deletes by ID.
        /// The Slug is labeled "Url" in the Ghost admin area.
        /// </remarks>
        /// <param name="slug">The Slug of the tag to delete.</param>
        public bool DeleteTagBySlug(string slug)
        {
            var tag = GetTagBySlug(slug);

            return DeleteTagById(tag.Id);
        }

        /// <summary>
        /// Delete a user based on their ID.
        /// </summary>
        /// <returns>True if the request was successful; otherwise False.</returns>
        /// <param name="id">The ID of the user to delete.</param>
        public bool DeleteUserById(string id)
        {
            var request = new RestRequest($"users/{id}", Method.DELETE);

            AppendSecurity(request);

            return Execute(request);
        }

        /// <summary>
        /// Delete a user based on their Slug (aka Url).
        /// </summary>
        /// <returns>True if the request was successful; otherwise False.</returns>
        /// <remarks>
        /// Performs a GET request first, to get the user ID, as the Ghost API only allows deletes by ID.
        /// </remarks>
        /// <param name="slug">The Slug of the user to delete.</param>
        public bool DeleteUserBySlug(string slug)
        {
            var user = GetUserBySlug(slug);

            return DeleteUserById(user.Id);
        }
    }
}

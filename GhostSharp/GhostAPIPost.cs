using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GhostSharp.Entities;
using RestSharp;

namespace GhostSharp
{
    public partial class GhostAPI
    {
        /// <summary>
        /// Creates the post. (NOTE: Id is ignored. See remarks.)
        /// </summary>
        /// <returns>The created post.</returns>
        /// <remarks>
        /// The API allows for a custom ID, but a bug prevents the post from being deleted.
        /// GitHub Issue: https://github.com/TryGhost/Ghost/issues/9100
        /// </remarks>
        /// <param name="post">The post to create.</param>
        public Post CreatePost(Post post)
        {         
            var request = new RestRequest("posts", Method.POST);

            AppendSecurity(request);

            var postWithoutAuthor = ConvertToPostWithoutAuthor(post);

            var properties = postWithoutAuthor.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var jsonSegment =
                (from p in properties
                 where p.GetValue(postWithoutAuthor) != null
                   && p.Name != "Id"
                 let val = p.GetValue(postWithoutAuthor)
                 let valType = p.GetValue(postWithoutAuthor).GetType()
                 select $"\"{p.Name.ToLower()}\": " + (valType == typeof(bool) || valType == typeof(int) ? $"{val.ToString().ToLower()}" : $"\"{val}\""));

            var json = "{ \"posts\" : [{ " + string.Join(",", jsonSegment) + " }] }";

            request.AddParameter("application/json",
                                 json,
                                ParameterType.RequestBody);

            var postResponse = Execute<PostResponse<PostWithoutAuthor>>(request);

            return StandardizePostWithoutAuthor(postResponse.Posts.Single());
        }

        /// <summary>
        /// Creates the tag with the given name, slug, and description.
        /// </summary>
        /// <returns>The tag.</returns>
        /// <remarks>
        /// The API allows for a custom ID, but a bug prevents the tag from being deleted.
        /// GitHub Issue: https://github.com/TryGhost/Ghost/issues/9100
        /// </remarks>
        /// <param name="name">Name for tag (required)</param>
        /// <param name="slug">Slug for tag</param>
        /// <param name="description">Description of tag</param>
        public Tag CreateTag(string name, string slug, string description)
        {
            var request = new RestRequest("tags", Method.POST);
            AppendSecurity(request);

            var jsonSegments = new List<string>
            {
                $"\"{nameof(name)}\": \"{name}\"",
                $"\"{nameof(slug)}\": \"{slug}\"",
                $"\"{nameof(description)}\": \"{description}\""
            };

            request.AddParameter("application/json",
                                 "{ \"tags\" : [{ " + string.Join(",", jsonSegments) + " }] }",
                                 ParameterType.RequestBody);

            return Execute<TagResponse>(request)?.Tags?.SingleOrDefault();
        }
    }
}

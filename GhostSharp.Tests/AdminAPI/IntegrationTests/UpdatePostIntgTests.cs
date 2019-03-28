using GhostSharp.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class UpdatePostIntgTests : TestBase
    {
        private GhostAdminAPI auth;
        private Post origPost = null;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);

            origPost = auth.CreatePost(new Post { Title = "Sample post used for update post tests" });
        }

        [TearDown]
        public void TearDown()
        {
            if (origPost != null)
                auth.DeletePost(origPost.Id);

            origPost = null;
        }

        //[Test]
        //public void UpdatePost_CanUpdateAnExistingPost()
        //{
        //    origPost.Title = "Title changed";
        //    origPost.UpdatedAt = origPost.UpdatedAt.Value.AddMinutes(5);

        //    var updatedPost = auth.UpdatePost(origPost);

        //    Assert.AreEqual(origPost.Id, updatedPost.Id);
        //    Assert.AreNotEqual(origPost.Title, updatedPost.Title);


        //}
    }


    // update a single post

    // update a post but change id to invalid

    // what happens if we make up a new post with an id of our choosing? validly formatted id even?

    // updated_at is required - leave it out, set it in past, same time?

    // do a GET and save it right back in?

    // Tag and author relations will be replaced, not merged - test this

    // then copy all of this for pages

    // then implement site - easy - NO AUTH!

}

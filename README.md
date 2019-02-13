[![Travis][travis badge]][travis]
[![License: MIT][license badge]](license)
[![Open Source Helpers][os badge]](os)
[![Twitter][twitter-badge]][twitter-intent]

[![GitHub release][img-version-badge]][repo] [![Gitter][img-gitter-badge]][gitter] [![CodeClimate][img-code-climate-badge]][code-climate] [![Build Status][img-travis-ci]][travis-ci] [![Code of Conduct][coc-badge]][coc] [![PRs Welcome][prs-badge]][prs]  <a href="#patched-fonts" title=""><img src="https://raw.githubusercontent.com/wiki/ryanoasis/nerd-fonts/images/faux-shield-badge-os-logos.svg?sanitize=true" alt="Nerd Fonts - OS Support"></a> [![Twitter][twitter-badge]][twitter-intent]

# GhostSharp

This is a [wrapper](https://grantwinney.com/what-is-an-api-wrapper-and-how-do-i-write-one/) around the [Ghost API](https://api.ghost.org), a RESTful JSON API built into the core of the [Ghost blogging platform](https://ghost.org/).

I like Ghost, C# and [experimenting with APIs](https://grantwinney.com/tag/api/), so this seemed like the perfect intellectual pursuit right now. I don't even have a use for it yet, but if you find one please [let me know](https://twitter.com/GrantWinney)! Check out the [official Ghost API docs](https://api.ghost.org/docs) and read about [my own experience using them](https://grantwinney.com/what-is-the-ghost-api/).

I [tagged the wrapper](https://github.com/grantwinney/GhostSharp/tree/v1.0) as I wrote it to work with v1.0 of the API.

## Usage

### Accessing the Public API

If you only need to access the public API (assuming it's enabled on your site), all you need is the URL of your site and the client id and secret.

If you don't have the client id or secret, view the source code of any post, and look for a block similar to this:

```html
<script type="text/javascript">
ghost.init({
	clientId: "ghost-frontend",
	clientSecret: "53f1f0e99723"
});
</script>
```

Once you have those pieces of information, you can access anything "public", which is the GET action on the various endpoints.

```csharp
var url = "https://your-site.com"
var clientId = "ghost-frontend";
var clientSecret = "1234abcd6789";

var auth = new GhostAPI(url, clientId, clientSecret);

// get a collection of posts
var postResponse = auth.GetPosts();
var posts = postResponse.Posts;

// get a particular post, based on the slug
var post = auth.GetPostBySlug(PostSlug);
```

### Accessing the Private API (need a new auth token)

If the public API is disabled in the site settings, or you need to create or delete data, you'll need an authorization (aka authentication) token.

If you don't have an auth token, you'll need your username and password, as well as your client id and secret (see previous section on where to find those). There's no proper OAuth implementation yet that allows you to request an auth token by logging into Ghost.

Once you have those pieces of information, you can supply them to the constructor, and it'll be used in all subsequent requests on that `GhostAPI` instance.

```csharp
var url = "https://your-site.com"
var clientId = "ghost-frontend";
var clientSecret = "1234abcd6789";
var username = "youremail@somewhere.com";
var password = "some-password";

// internally, this gets an auth token and saves it
var auth = new GhostAPI(url, clientId, clientSecret, username, password);

var post = auth.GetPostBySlug(PostSlug);
```

You can get the generated auth token with `auth.AuthorizationToken`.

### Accessing the Private API (use an existing token)

If you already have an auth token, you can just supply that directly:

```csharp
var url = "https://your-site.com"
var token = "some-auth-token";

var auth = new GhostAPI(url, token);

var post = auth.GetPostBySlug(PostSlug);
```

## Versioning

This wrapper is written around v1.14.0 of the API, currently the latest version. When they update the API in the future (and it's likely they will), I'll probably create a tag for the current release, before updating for the newest one.

## Running the Tests

The tests are setup to run against an actual instance of the Ghost blog, with its public API enabled.

**I made every effort for the tests to not get in the way of real posts, tags or users, and to clean up after themselves.** I ran them about 50 times against my own blog, which is very much production for me. It creates draft posts and deletes them, tags no one will notice and deletes them, etc. There's not too much we can do with users, so I perform a few GET requests to make sure that part works. Still, caveat emptor.

The "GhostSharpTests" class, which contains the tests, is a partial class. The other part of the class is in the "TestBase.cs" file. Find the empty `const` fields and fill in the missing data, and then the tests should run okay.

_Note: Sometimes if you run the whole suite, one or two tests will fail. Run the failed ones individually and they'll pass. I think this has to do with my using the same id for tests, and something isn't completely removed from the system before the next test tries to create it again._

## Implementation Details

### Including Author in the Response

By default, when requesting a post with the Ghost API, you get the author id as a single JSON field named "Author". If you choose to include all info about the author (by adding `?include=author` to the query string), then it adds a JSON block, also named "Author". 

That caused a problem for me, since [RestSharp](http://restsharp.org/) needs a matching class to deserialize the response to, and you can't have a single class with an "Author" property that is both an `int` *and* an `Author` class.

To get around this shortcoming, I created a couple classes under the covers, to temporarily store the data in. By the time the post is returned to you, however, it's wrangled into a consistent format. The `Author` property is `null` if you didn't get full author info; otherwise it's populated. The `AuthorId` field stores the author ID, in either case.

### Ghost Query Language (GQL)

The Ghost API lets you filter results by using their own syntax, called [Ghost Query Language](https://github.com/TryGhost/GQL). [Read the full specs here](https://github.com/TryGhost/Ghost/issues/5604).

GQL might be a little buggy, or else their API doesn't support it completely, as I wasn't able to specify multiple filters when calling the API directly from Postman. Trying an `OR` combination only seemed to apply the last filter, while an `AND` combination returned an error:

```json
{
    "errors": [
        {
            "message": "The request failed validation.",
            "context": "Error parsing filter",
            "errorType": "ValidationError"
        }
    ]
}
```

Just be aware that if you set a value for any of the `xxxxQueryParams.Filter` properties, you may not get the results that you expect.

### Limitations on Creating (POSTing) Posts

For some reason, I have not yet been able to get much more than the post title to create correctly in Ghost, even though running the same query via Postman works okay. I think it has something to do with RestSharp, and the object I'm passing to it, which it then transforms and sends to Ghost. Haven't tracked it down yet.

### Other Issues / Nice-to-haves

I've opened up a few issues with nice-to-haves and whatnots, which anyone can tackle. I may get to them eventually - who knows... probably moreso if anyone says they'd find them helpful.

## Problems?

[Open an issue](https://github.com/grantwinney/GhostSharp/issues/new), and include errors, unexpected behavior, steps to reproduce, etc. The more details, the better!

Feel free to [open a PR](https://github.com/grantwinney/GhostSharp/compare) if you figure out how to fix it.

##  Ideas?

[Open an issue](https://github.com/grantwinney/GhostSharp/issues/new). I can't promise when new features or suggestions will get implemented, but I'll check them out.

<!-- Badges -->
[travis]: https://travis-ci.org/eproxus/meck
[travis badge]: https://img.shields.io/travis/eproxus/meck/master.svg?style=flat-square
[license]: https://opensource.org/licenses/MIT
[license badge]: https://img.shields.io/badge/License-MIT-green.svg
[os badge]: https://www.codetriage.com/grantwinney/ghostsharp/badges/users.svg
[os]: https://www.codetriage.com/grantwinney/ghostsharp
[twitter-intent]:https://twitter.com/intent/tweet?url=https%3A%2F%2Fgithub.com%2grantwinney%2FGhostSharp&via=%40nerdfonts&text=Nerd%20Fonts%20-%20Iconic%20font%20aggregator%2C%20collection%2C%20and%20patcher&hashtags=iconfont%20font%20github
[twitter-badge]:https://img.shields.io/twitter/url/http/shields.io.svg?style=social

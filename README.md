[![Travis][travis badge]][travis]
[![License: MIT][license badge]][license]
[![NuGet][nuget badge]][nuget]
[![CodeFactor][codefactor badge]][codefactor]
[![Open Source Helpers][os badge]][os]
![Language][lang badge]
[![Twitter][twitter-badge]][twitter-intent]

# GhostSharp

This is a [wrapper](https://grantwinney.com/what-is-an-api-wrapper-and-how-do-i-write-one/) around the [Ghost API v2.0](https://docs.ghost.org/api/content/), a RESTful JSON API built into the core of the [Ghost blogging platform](https://ghost.org/). Check out the [official Ghost API docs](https://docs.ghost.org/api/content) and read about [my own experience using them](https://grantwinney.com/what-is-the-ghost-api/).

## Usage

### Accessing the Content API

If you need to access the Content API, all you need is the URL of your site and a Content API Key, [available on the "Integrations" page](https://docs.ghost.org/api/content/#key). Once you have those pieces of information, you can access any "public" content.

### Accessing the Admin API

To access the Admin API, you'll also need an Admin API Key in addition to the two pieces of above data, which is also available on the same "Integrations" page. With that, you can access and modify "private" content.

## Versioning

This wrapper is written around v2.0 of the Content and Admin APIs, currently the latest version. If they update the APIs in the future, I'll probably create a tag for the current release, before updating to the newest one.

## Running the Tests

The tests are setup to run against an actual instance of the Ghost blog, using a valid API key. There are details in the `TestBase.cs` class that you'll need to fill in, such as a valid API key, valid post ID, valid post slug, etc, etc.

## Problems?

[Open an issue](https://github.com/grantwinney/GhostSharp/issues/new), and include errors, unexpected behavior, steps to reproduce, etc. The more details, the better!

Feel free to [open a PR](https://github.com/grantwinney/GhostSharp/compare) if you figure out how to fix it.

##  Ideas?

[Open an issue](https://github.com/grantwinney/GhostSharp/issues/new). I can't promise when new features or suggestions will get implemented, but I'll check them out.

## Release Notes

* 1.0.3 - Covers stable Admin API endpoints, currently posts, pages, images, site.
* 1.0.2 - Beginning work to support Admin API.<br>***Breaking change:** GhostAPI is now split into GhostContentAPI and GhostAdminAPI*
* 1.0.1 - Added comments to aid in intellisense.
* 1.0.0 - Covers the Content API endpoints.

<!-- Badges -->
[travis]: https://travis-ci.org/grantwinney/GhostSharp
[travis badge]:https://img.shields.io/travis/grantwinney/GhostSharp.svg?branch=master
[codefactor]: https://www.codefactor.io/repository/github/grantwinney/ghostsharp
[codefactor badge]:https://www.codefactor.io/repository/github/grantwinney/ghostsharp/badge
[license]: https://opensource.org/licenses/MIT
[license badge]: https://img.shields.io/badge/License-MIT-green.svg
[os badge]: https://www.codetriage.com/grantwinney/ghostsharp/badges/users.svg
[os]: https://www.codetriage.com/grantwinney/ghostsharp
[twitter-intent]:https://twitter.com/intent/tweet?url=https%3A%2F%2Fgithub.com%2Fgrantwinney%2FGhostSharp&text=GhostSharp,%20a%20C%23%20Wrapper%20for%20the%20Ghost%20API&hashtags=tryghost,api
[twitter-badge]:https://img.shields.io/twitter/url/http/shields.io.svg
[nuget]:https://www.nuget.org/packages/GhostSharp
[nuget badge]:https://img.shields.io/nuget/v/GhostSharp.svg
[lang badge]:https://img.shields.io/github/languages/top/grantwinney/GhostSharp.svg

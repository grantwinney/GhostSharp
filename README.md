[![Travis][travis badge]][travis]
[![License: MIT][license badge]][license]
[![NuGet][nuget badge]][nuget]
[![Open Source Helpers][os badge]][os]
[![Twitter][twitter-badge]][twitter-intent]
![Language][lang badge]

# GhostSharp

This is a [wrapper](https://grantwinney.com/what-is-an-api-wrapper-and-how-do-i-write-one/) around the [Ghost API](https://docs.ghost.org/api/content/), a RESTful JSON API built into the core of the [Ghost blogging platform](https://ghost.org/).

I like Ghost, C# and [experimenting with APIs](https://grantwinney.com/tag/api/), so this seemed like an interesting pursuit. I don't have a use for it yet, but if you find one please let me know! Check out the [official Ghost API docs](https://docs.ghost.org/ap/i/content) and read about [my own experience using them](https://grantwinney.com/what-is-the-ghost-api/).

I [tagged the wrapper](https://github.com/grantwinney/GhostSharp/tree/v1.0) as I wrote it to work with v1.14.0 of the API.

## Usage

### Accessing the Public API

If you need to access the public API, all you need is the URL of your site and to [generate an API key](https://docs.ghost.org/api/content/#key).

Once you have those pieces of information, you can access anything "public", which is the GET action on the various endpoints.

## Versioning

This wrapper is written around v2.0 of the API, currently the latest version. If they update the API in the future, I'll probably create a tag for the current release, before updating for the newest one.

## Running the Tests

The tests are setup to run against an actual instance of the Ghost blog, using a valid API key. There are details in the `TestBase.cs` class that you'll need to fill in, such as a valid API key, valid post ID, valid post slug, etc, etc.

## Implementation Details

### Ghost Query Language (NQL)

The [Filter](https://docs.ghost.org/api/content/#filter) parameter depends on NQL, which is [described here](https://docs.ghost.org/api/content/#filtering). That's the only part I haven't implemented in this wrapper... yet.

## Problems?

[Open an issue](https://github.com/grantwinney/GhostSharp/issues/new), and include errors, unexpected behavior, steps to reproduce, etc. The more details, the better!

Feel free to [open a PR](https://github.com/grantwinney/GhostSharp/compare) if you figure out how to fix it.

##  Ideas?

[Open an issue](https://github.com/grantwinney/GhostSharp/issues/new). I can't promise when new features or suggestions will get implemented, but I'll check them out.

<!-- Badges -->
[travis]: https://travis-ci.org/grantwinney/GhostSharp
[travis badge]:https://img.shields.io/travis/grantwinney/GhostSharp.svg?branch=master
[license]: https://opensource.org/licenses/MIT
[license badge]: https://img.shields.io/badge/License-MIT-green.svg
[os badge]: https://www.codetriage.com/grantwinney/ghostsharp/badges/users.svg
[os]: https://www.codetriage.com/grantwinney/ghostsharp
[twitter-intent]:https://twitter.com/intent/tweet?url=https%3A%2F%2Fgithub.com%2Fgrantwinney%2FGhostSharp&text=GhostSharp,%20a%20C%23%20Wrapper%20for%20the%20Ghost%20API&hashtags=tryghost,api
[twitter-badge]:https://img.shields.io/twitter/url/http/shields.io.svg
[nuget]:http://nothingyet.com
[nuget badge]:https://img.shields.io/nuget/v/GhostSharp.svg
[lang badge]:https://img.shields.io/github/languages/top/grantwinney/GhostSharp.svg

[![CI Build](https://github.com/grantwinney/GhostSharp/actions/workflows/dotnet.yml/badge.svg)](https://github.com/grantwinney/GhostSharp/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)
[![NuGet](https://img.shields.io/nuget/v/GhostSharp.svg)](https://www.nuget.org/packages/GhostSharp)
[![CodeFactor](https://www.codefactor.io/repository/github/grantwinney/ghostsharp/badge)](https://www.codefactor.io/repository/github/grantwinney/ghostsharp)
[![Open Source Helpers](https://www.codetriage.com/grantwinney/ghostsharp/badges/users.svg)](https://www.codetriage.com/grantwinney/ghostsharp)
![Language](https://img.shields.io/github/languages/top/grantwinney/GhostSharp.svg)
[![Twitter](https://img.shields.io/twitter/url/http/shields.io.svg)](https://twitter.com/intent/tweet?url=https%3A%2F%2Fgithub.com%2Fgrantwinney%2FGhostSharp&text=GhostSharp,%20a%20C%23%20Wrapper%20for%20the%20Ghost%20API&hashtags=tryghost,api)

# GhostSharp

This is a [wrapper](https://grantwinney.com/what-is-an-api-wrapper-and-how-do-i-write-one/) around the [Ghost API v3.0](https://ghost.org/docs/content-api/), a RESTful JSON API built into the core of the [Ghost blogging platform](https://ghost.org/). Check out the [official Ghost API docs](https://ghost.org/docs/api/) and read about [my own experience using them](https://grantwinney.com/what-is-the-ghost-api/).

## Usage

### Accessing the Content API

If you need to access the Content API, all you need is the URL of your site and a Content API Key, [available on the "Integrations" page](https://ghost.org/docs/content-api/#key). Once you have those pieces of information, you can access any "public" content.

```csharp
var ghost = new GhostSharp.GhostContentAPI("https://grantwinney.com", "a6d33f1b95ff17adf0f787a70a");
var settings = ghost.GetSettings();

Console.WriteLine($"Welcome to {settings.Title}: {settings.Description}\r\n");
Console.WriteLine($"Navigation: {string.Join(", ", settings.Navigation.Select(x => x.Label))}");
```

Output:

```
Welcome to Grant Winney: We learn by doing. We've all got something to contribute.

Navigation: Home, APIs, Lambda, Rasp PI, About Me, CV
```

### Accessing the Admin API

If you need to access the Admin API, all you need is the URL of your site and an Admin API Key, also [available on the "Integrations" page](https://docs.ghost.org/api/content/#key). Once you have those pieces of information, you can access any "private" content.

```csharp
var ghost = new GhostSharp.GhostAdminAPI("https://grantwinney.com", 
    "5cf706fd7d4a33066550627a:9e5ed2b90e40f68573b0ccaf4aef666b047fc9867ad285b2e219eed5503bae53");
var site = ghost.GetSite();

Console.WriteLine($"Welcome to <a href='{site.Url}'>{site.Title}</a>\r\n");
Console.WriteLine($"Running Ghost v{site.Version}");
```

Output:

```
Welcome to <a href='https://grantwinney.com/'>Grant Winney</a>

Running Ghost v2.23
```

## Versioning

This wrapper is written around the [Content](https://ghost.org/docs/content-api/) and [Admin](https://ghost.org/docs/admin-api/) APIs, currently the latest version. As they update the API, I'll create a tag for the current release, before updating to the newest one.

## Running the Tests

The tests are setup to run against an actual instance of the Ghost blog, using a valid API key. There are details in the `TestBase.cs` class that you'll need to fill in, such as a valid API key, valid post ID, valid post slug, etc, etc.

## Problems?

[Open an issue](https://github.com/grantwinney/GhostSharp/issues/new), and include errors, unexpected behavior, steps to reproduce, etc. The more details, the better!

Feel free to [open a PR](https://github.com/grantwinney/GhostSharp/compare) if you figure out how to fix it.

##  Ideas?

[Open an issue](https://github.com/grantwinney/GhostSharp/issues/new). I can't promise when new features or suggestions will get implemented, but I'll check them out.

## Release Notes

* 1.1.0 - ⭐ Upgrade to .NET 6.
* 1.0.9 - 🐛 Noticed that pages aren't being created; other refactoring.<br>***Breaking change:** Eliminated Page in favor of a single Post object again, for simplicity. The split wasn't necessary as a Page is just a Post that doesn't end up in feeds. Also, a few fields in a Post don't play nicely when creating pages, but I figured out a different way to omit those.*
* 1.0.8 - 🐛 Several fields marked as updateable that aren't. (thx for the heads up @unnanego)<br>***Breaking change:** This led to a larger refactoring, which might break some current implementations. Properties in the Post class that cannot be changed now have private setters (instead of just an attribute on them). The Page class was completely separated from the Post class, so RestSharp could correctly deserialize objects coming from Ghost.*
* 1.0.7 - ⭐ Implement the Themes admin endpoint. (thx for pointing me in the right direction @naz)
* 1.0.6 - ⭐ Implement API v3.
* 1.0.4 - 🧹 Code cleanup. Added license to NuGet package.
* 1.0.3 - ⭐ Implement Admin API v2 endpoints (posts, pages, images, site)<br>***Breaking change:** GhostAPI is now split into GhostContentAPI and GhostAdminAPI*
* 1.0.1 - 🧹 Added comments to aid in intellisense.
* 1.0.0 - ⭐ Implement Content API endpoints.

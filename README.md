# GhostSharp

This is a [wrapper](https://grantwinney.com/what-is-an-api-wrapper-and-how-do-i-write-one/) around the [Ghost API](https://api.ghost.org), a RESTful JSON API built into the core of the [Ghost blogging platform](https://ghost.org/).

I like Ghost, and it's been awhile since I really flexed my C# muscles, so I decided to do this as an exercise. I don't even have a use for it yet, so if you do please [let me know](https://twitter.com/GrantWinney)! Check out the [official docs](https://api.ghost.org/docs) and read about [my own experience](https://grantwinney.com/what-is-the-ghost-api/) too.

## Versioning

This wrapper is written around v1.14.0 of the API, currently the latest version. If they update the API in the future, I'll most likely create a tag for the previous release, before updating to the newest one.

## Running the Tests

The tests are setup to run against an actual instance of the Ghost blog, with its public API enabled.

The "GhostSharpTests" class, which contains the tests, is a partial class. The other part of the class is in the "GhostSharpTestsSetup.cs" file. Find the empty `const` fields and fill in the missing data, and then the tests should run okay.

## Implementation Details

### Including Author in the Response

By default, when requesting a post with the Ghost API, you get the author id as a single JSON field named "Author". If you choose to include all info about the author (by adding `?include=author` to the query string), then it adds a JSON block, also named "Author". 

That caused a problem for me, since [RestSharp](http://restsharp.org/) needs a matching class to deserialize the response to, and you can't have a single class with an "Author" property that is both an `int` *and* an `Author` class.

To get around this shortcoming, I automatically include full author information. This could result in more data being sent back then necessary - especially if, for instance, you make a request for 300 posts at once and you're the only author on your blog. It's the best I could come up with for now though.

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

## Problems?

[Open an issue](https://github.com/grantwinney/GhostSharp/issues/new), and include errors, unexpected behavior, steps to reproduce, etc. The more details, the better!

Feel free to [open a PR](https://github.com/grantwinney/GhostSharp/compare) if you figure out how to fix it.

##  Ideas?

[Open an issue](https://github.com/grantwinney/GhostSharp/issues/new). I can't promise when new features or suggestions will get implemented, but I'll check them out.

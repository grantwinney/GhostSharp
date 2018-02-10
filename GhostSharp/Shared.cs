using System;
using RestSharp;
using RestSharp.Authenticators;

namespace GhostSharp
{
    static class Shared
    {
        const string BASE_URI = "/ghost/api/v0.1";

        /// <summary>
        /// Calls the Ghost API and returns the response data.
        /// </summary>
        /// <returns>The API.</returns>
        /// <param name="request">A RestRequest representing the resource being requested.</param>
        /// <typeparam name="T">The type of object being requested</typeparam>
        internal static T Execute<T>(string siteUrl, RestRequest request) where T : new()
        {
            var client = new RestClient { BaseUrl = new Uri(siteUrl + BASE_URI) };

            var response = client.Execute<T>(request);

            //var n = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiFailure>(response.Content);

            //{ "errors":[{"message":"Access denied.","context":"Client credentials were not provided","errorType":"UnauthorizedError"}]}

            if (response.ErrorException != null)
                throw new ApplicationException($"Unable to {request.Method} /{request.Resource}: {response.ResponseStatus}", response.ErrorException);

            return response.Data;
        }
    }
}


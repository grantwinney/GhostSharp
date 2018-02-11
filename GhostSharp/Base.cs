using System;
using System.Collections.Generic;
using GhostSharp.Entities;
using Newtonsoft.Json;
using RestSharp;

namespace GhostSharp
{
    static class Base
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

            TestResponseForErrors(response);
            TestResponseForException(response, request);

            return response.Data;
        }

        /// <summary>
        /// If the response content has one or more error messages, throw an exception.
        /// </summary>
        /// <param name="response">The API response</param>
        static void TestResponseForErrors(IRestResponse response)
        {
            var apiFailure = JsonConvert.DeserializeObject<GhostApiFailure>(response.Content);
            if (apiFailure.Errors != null)
                throw new GhostSharpException(apiFailure.Errors);
        }

        /// <summary>
        /// If the response returns an exception, add a message and throw it.
        /// </summary>
        /// <param name="response">The API response</param>
        /// <param name="request">The original request to the API.</param>
        static void TestResponseForException(IRestResponse response, RestRequest request)
        {
            if (response.ErrorException != null)
                throw response.ErrorException;
                //throw new GhostSharpException($"Unable to {request.Method} /{request.Resource}: {response.ResponseStatus}", response.ErrorException);
        }
    }

    /// <summary>
    /// Represents a collection of errors returned by the Ghost API.
    /// </summary>
    class GhostApiFailure
    {
        public List<GhostApiError> Errors { get; set; }
    }
}


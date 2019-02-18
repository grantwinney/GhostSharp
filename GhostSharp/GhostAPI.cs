using System;
using GhostSharp.Entities;
using Newtonsoft.Json;
using RestSharp;

namespace GhostSharp
{
    /// <summary>
    /// Processing successful and error responses from the Ghost API.
    /// </summary>
    public partial class GhostAPI
    {
        Uri host;
        const string PATH_AND_VERSION = "/ghost/api/v2/content/";

        readonly string key;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GhostSharp.GhostAPI"/> class.
        /// </summary>
        /// <param name="host">The Host for which to access the API.</param>
        /// <param name="key">Content API key.</param>
        public GhostAPI(string host, string key)
        {
            this.key = key;
            this.host = new Uri(new Uri(host), PATH_AND_VERSION);
            ExceptionLevel = ExceptionLevel.All;
        }

        /// <summary>
        /// Specify which exceptions to rethrow, if any. Default is All.
        /// </summary>
        public ExceptionLevel ExceptionLevel { private get; set; }

        /// <summary>
        /// Gets the last exception that was thrown.
        /// </summary>
        /// <value>The last exception.</value>
        public Exception LastException { get; private set; }

        /// <summary>
        /// Calls the Ghost API and returns the response data.
        /// If exceptions are suppressed, returns null on failure.
        /// </summary>
        /// <returns>The API response.</returns>
        /// <param name="request">A RestRequest representing the resource being requested.</param>
        /// <typeparam name="T">The type of object being requested</typeparam>
        T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient { BaseUrl = host };

            try
            {
                var response = client.Execute<T>(request);
                TestResponseForErrors(response, request);
                return response.Data;
            }
            catch (GhostSharpException)
            {
                if (ExceptionLevel == ExceptionLevel.Ghost || ExceptionLevel == ExceptionLevel.All)
                    throw;

                return default;
            }
            catch
            {
                if (ExceptionLevel == ExceptionLevel.NonGhost || ExceptionLevel == ExceptionLevel.All)
                    throw;

                return default;
            }
        }

        /// <summary>
        /// If the response content has one or more error messages, throw an exception.
        /// </summary>
        /// <param name="response">The API response</param>
        void TestResponseForErrors(IRestResponse response, RestRequest request)
        {
            var apiFailure = JsonConvert.DeserializeObject<GhostApiFailure>(response.Content);
            if (apiFailure != null && apiFailure.Errors != null)
            {
                var ex = new GhostSharpException(apiFailure.Errors);
                LastException = ex;
                throw ex;
            }

            if (response.ErrorException != null)
            {
                var ex = new GhostSharpException($"Unable to {request.Method} /{request.Resource}: {response.ResponseStatus}", response.ErrorException);
                LastException = ex;
                throw ex;
            }
        }
    }
}

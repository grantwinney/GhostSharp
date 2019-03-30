using System;
using System.Net.Cache;
using GhostSharp.Entities;
using Newtonsoft.Json;
using RestSharp;

namespace GhostSharp
{
    enum APIType
    {
        Admin,
        Content
    }
  
    public partial class GhostAPI
    {
        internal string key;
        private APIType apiType;
        public IRestClient Client { internal get; set; }

        internal GhostAPI(string host, string key, ExceptionLevel exceptionLevel, string baseUrl, APIType apiType)
            : this(host, exceptionLevel, baseUrl, apiType)
        {
            this.key = key;
        }

        internal GhostAPI(string host, ExceptionLevel exceptionLevel, string baseUrl, APIType apiType)
        {
            this.apiType = apiType;
            Client = new RestClient { BaseUrl = new Uri(new Uri(host), baseUrl) };
            ExceptionLevel = exceptionLevel;
        }

        /// <summary>
        /// Specify which exceptions to rethrow, if any. Default is All.
        /// </summary>
        public ExceptionLevel ExceptionLevel { private get; set; }

        /// <summary>
        /// Gets the last exception that was thrown.
        /// </summary>
        /// <value>The last exception.</value>
        public Exception LastException { get; internal set; }

        /// <summary>
        /// Calls the Ghost API and returns the response data.
        /// If exceptions are suppressed, returns null on failure.
        /// </summary>
        /// <returns>The API response.</returns>
        /// <param name="request">A RestRequest representing the resource being requested.</param>
        /// <typeparam name="T">The type of object being requested</typeparam>
        internal T Execute<T>(RestRequest request) where T : new()
        {
            if (key != null)
                AuthorizeRequest(request);

            try
            {
                var response = Client.Execute<T>(request);
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
        /// Calls the Ghost API and returns the response data.
        /// If exceptions are suppressed, returns null on failure.
        /// </summary>
        /// <returns>The API response.</returns>
        /// <param name="request">A RestRequest representing the resource being requested.</param>
        internal bool Execute(RestRequest request)
        {
            AuthorizeRequest(request);

            try
            {
                var response = Client.Execute(request);
                TestResponseForErrors(response, request);
                return response.StatusCode == System.Net.HttpStatusCode.NoContent;  // vs NotFound
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
        /// If response.Content has one or more error messages (returned from Ghost),
        /// or response.Exception contains an exception (some other exception thrown during request),
        /// create and throw a GhostSharpException with the details.
        /// </summary>
        /// <param name="response">The API response</param>
        private void TestResponseForErrors(IRestResponse response, RestRequest request)
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

        /// <summary>
        /// Add the key as a query parameter or authorization token as a header, depending on the API being used.
        /// </summary>
        /// <param name="request">The request being made</param>
        private void AuthorizeRequest(RestRequest request)
        {
            switch (apiType)
            {
                case APIType.Content:
                    request.AddQueryParameter("key", key);
                    break;
                case APIType.Admin:
                    request.AddHeader("Authorization", $"Ghost {key}");
                    break;
            }
        }
    }
}

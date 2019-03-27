using System;
using System.IO;
using GhostSharp.Entities;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

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
        {
            this.key = key;
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
            switch (apiType)
            {
                case APIType.Content:
                    request.AddQueryParameter("key", key);
                    break;
                case APIType.Admin:
                    request.AddHeader("Authorization", $"Ghost {key}");
                    break;
            }

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
    }

    // Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

        public class NewtonsoftJsonSerializer : ISerializer, IDeserializer
        {
            private Newtonsoft.Json.JsonSerializer serializer;

            public NewtonsoftJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
            {
                this.serializer = serializer;
            }

            public string ContentType
            {
                get { return "application/json"; } // Probably used for Serialization?
                set { }
            }

            public string DateFormat { get; set; }

            public string Namespace { get; set; }

            public string RootElement { get; set; }

            public string Serialize(object obj)
            {
                using (var stringWriter = new StringWriter())
                {
                    using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                    {
                        serializer.Serialize(jsonTextWriter, obj);

                        return stringWriter.ToString();
                    }
                }
            }

            public T Deserialize<T>(RestSharp.IRestResponse response)
            {
                var content = response.Content;

                using (var stringReader = new StringReader(content))
                {
                    using (var jsonTextReader = new JsonTextReader(stringReader))
                    {
                        return serializer.Deserialize<T>(jsonTextReader);
                    }
                }
            }

            public static NewtonsoftJsonSerializer Default
            {
                get
                {
                    return new NewtonsoftJsonSerializer(new Newtonsoft.Json.JsonSerializer()
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                    });
                }
            }
        }
}

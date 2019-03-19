using System;
using GhostSharp.Entities;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using Newtonsoft.Json;
using RestSharp;

namespace GhostSharp
{
    /// <summary>
    /// Initialization for the Ghost Content API.
    /// </summary>
    public class GhostContentAPI : GhostAPI
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GhostSharp.GhostContentAPI"/> class.
        /// </summary>
        /// <param name="host">The Host for which to access the Content API.</param>
        /// <param name="contentApiKey">Content API key.</param>
        public GhostContentAPI(string host, string contentApiKey, ExceptionLevel exceptionLevel = ExceptionLevel.All)
            : base(host, contentApiKey, exceptionLevel, "/ghost/api/v2/content/")
        {
        }
    }

    /// <summary>
    /// Initialization for the Ghost Admin API.
    /// </summary>
    public class GhostAdminAPI : GhostAPI
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GhostSharp.GhostAdminAPI"/> class.
        /// </summary>
        /// <param name="host">The Host for which to access the Admin API.</param>
        /// <param name="adminApiKey">Admin API key.</param>
        public GhostAdminAPI(string host, string adminApiKey, ExceptionLevel exceptionLevel = ExceptionLevel.All)
            : base(host, adminApiKey, exceptionLevel, "/ghost/api/v2/admin/")
        {
            var adminKeyParts = adminApiKey.Split(':');

            if (adminKeyParts.Length != 2)
            {
                var exception = new ArgumentException("The Admin API Key should consist of an ID and Secret, separated by a colon.");
                LastException = exception;

                if (exceptionLevel == ExceptionLevel.All || exceptionLevel == ExceptionLevel.NonGhost)
                    throw exception;
            }

            var unixEpochInSeconds = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

            var token = new JwtBuilder().WithAlgorithm(new HMACSHA256Algorithm())
                                        .WithSecret(StringToByteArray(adminKeyParts[1]))
                                        .AddHeader(HeaderName.KeyId, adminKeyParts[0])
                                        .AddClaim("exp", unixEpochInSeconds + 300)
                                        .AddClaim("iat", unixEpochInSeconds)
                                        .AddClaim("aud", "/v2/admin/")
                                        .Build();

            try
            {
                var json = new JwtBuilder()
                    .WithSecret(StringToByteArray(adminKeyParts[1]))
                    .MustVerifySignature()
                    .Decode(token);
                Console.WriteLine(json);
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            key = token;
        }

        private static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }

    public partial class GhostAPI
    {
        internal string key;
        public IRestClient Client { internal get; set; }

        internal GhostAPI(string host, string key, ExceptionLevel exceptionLevel, string baseUrl)
        {
            this.key = key;
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
        T Execute<T>(RestRequest request) where T : new()
        {
            request.AddQueryParameter("key", key);

            //request.AddHeader("Authorization", $"Ghost {key}");

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

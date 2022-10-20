using System;
using GhostSharp.Enums;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;

namespace GhostSharp
{
    /// <summary>
    /// Initialization for the Ghost Admin API.
    /// </summary>
    /// <remarks>
    /// Documented online: https://docs.ghost.org/api/admin/
    /// </remarks>
    public partial class GhostAdminAPI : GhostAPI
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GhostSharp.GhostAdminAPI"/> class.
        /// </summary>
        /// <param name="host">The Host for which to access the Admin API.</param>
        /// <param name="adminApiKey">Admin API key.</param>
        public GhostAdminAPI(string host, string adminApiKey, ExceptionLevel exceptionLevel = ExceptionLevel.All, string baseUrl = "/ghost/api/v3/admin/")
            : base(host, adminApiKey, exceptionLevel, baseUrl, APIType.Admin)
        {
            var adminKeyParts = adminApiKey.Split(':');

            if (adminKeyParts.Length != 2)
            {
                var exception = new ArgumentException("The Admin API Key should consist of an ID and Secret, separated by a colon.");
                LastException = exception;
                throw exception;
            }

            var id = adminKeyParts[0];
            var secret = adminKeyParts[1];

            var unixEpochInSeconds = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

            var token = new JwtBuilder().WithAlgorithm(new HMACSHA256Algorithm())
                                        .WithSecret(Convert.FromHexString(secret))
                                        .AddHeader(HeaderName.KeyId, id)
                                        .AddHeader(HeaderName.Type, "JWT")
                                        .AddClaim("exp", unixEpochInSeconds + 300)
                                        .AddClaim("iat", unixEpochInSeconds)
                                        .AddClaim("aud", "/v3/admin/")
                                        .Encode();

            try
            {
                var json = new JwtBuilder()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(Convert.FromHexString(secret))
                    .MustVerifySignature()
                    .Decode(token);
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

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GhostSharp.GhostAdminAPI"/> class without auth.
        /// This is only useful if auth is not required, which is only the /site endpoint.
        /// </summary>
        /// <param name="host">The Host for which to access the Admin API.</param>
        public GhostAdminAPI(string host, ExceptionLevel exceptionLevel = ExceptionLevel.All, string baseUrl = "/ghost/api/v3/content/")
            : base(host, exceptionLevel, baseUrl, APIType.Admin)
        {
        }
    }
}

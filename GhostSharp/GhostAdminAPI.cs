using System;
using GhostSharp.Enums;
using JWT;
using JWT.Algorithms;
using JWT.Builder;

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
        public GhostAdminAPI(string host, string adminApiKey, ExceptionLevel exceptionLevel = ExceptionLevel.All)
            : base(host, adminApiKey, exceptionLevel, "/ghost/api/v2/admin/", APIType.Admin)
        {
            var adminKeyParts = adminApiKey.Split(':');

            if (adminKeyParts.Length != 2)
            {
                var exception = new ArgumentException("The Admin API Key should consist of an ID and Secret, separated by a colon.");
                LastException = exception;
                throw exception;
            }

            var unixEpochInSeconds = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

            var token = new JwtBuilder().WithAlgorithm(new HMACSHA256Algorithm())
                                        .WithSecret(Ext.StringToByteArray(adminKeyParts[1]))
                                        .AddHeader(HeaderName.KeyId, adminKeyParts[0])
                                        .AddClaim("exp", unixEpochInSeconds + 300)
                                        .AddClaim("iat", unixEpochInSeconds)
                                        .AddClaim("aud", "/v2/admin/")
                                        .Build();

            try
            {
                var json = new JwtBuilder()
                    .WithSecret(Ext.StringToByteArray(adminKeyParts[1]))
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
        public GhostAdminAPI(string host, ExceptionLevel exceptionLevel = ExceptionLevel.All)
            : base(host, exceptionLevel, "/ghost/api/v2/admin/", APIType.Admin)
        {
        }
    }
}

﻿using GhostSharp.Enums;

namespace GhostSharp
{
    /// <summary>
    /// Initialization for the Ghost Content API.
    /// </summary>
    /// <remarks>
    /// Documented online: https://docs.ghost.org/api/content/
    /// </remarks>
    public partial class GhostContentAPI : GhostAPI
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GhostSharp.GhostContentAPI"/> class.
        /// </summary>
        /// <param name="host">The Host for which to access the Content API.</param>
        /// <param name="contentApiKey">Content API key.</param>
        public GhostContentAPI(string host, string contentApiKey, ExceptionLevel exceptionLevel = ExceptionLevel.All, string baseUrl = "/ghost/api/content/", string minimumVersion = null)
            : base(host, contentApiKey, exceptionLevel, baseUrl, APIType.Content, minimumVersion)
        {
        }
    }
}
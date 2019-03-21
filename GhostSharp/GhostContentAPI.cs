using GhostSharp.Entities;

namespace GhostSharp
{
    /// <summary>
    /// Initialization for the Ghost Content API.
    /// </summary>
    public partial class GhostContentAPI : GhostAPI
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GhostSharp.GhostContentAPI"/> class.
        /// </summary>
        /// <param name="host">The Host for which to access the Content API.</param>
        /// <param name="contentApiKey">Content API key.</param>
        public GhostContentAPI(string host, string contentApiKey, ExceptionLevel exceptionLevel = ExceptionLevel.All)
            : base(host, contentApiKey, exceptionLevel, "/ghost/api/v2/content/", APIType.Content)
        {
        }
    }
}
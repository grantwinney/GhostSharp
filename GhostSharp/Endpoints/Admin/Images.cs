using GhostSharp.Entities;
using GhostSharp.Enums;
using RestSharp;

namespace GhostSharp
{
    public partial class GhostAdminAPI
    {
        /// <summary>
        /// Upload an image to the site
        /// </summary>
        /// <returns>Returns location from which the image can be fetched, and reference string if any.</returns>
        public Image UploadImage(ImageRequest image)
        {
            var request = new RestRequest("images/upload/", Method.POST);

            var mimeType = image.ImageType == ImageType.GIF
                ? "image/gif"
                : image.ImageType == ImageType.ICO
                    ? "image/x-icon"
                    : image.ImageType == ImageType.JPEG
                        ? "image/jpeg"
                        : image.ImageType == ImageType.PNG
                            ? "image/png"
                            : image.ImageType == ImageType.SVG
                                ? "image/svg+xml"
                                : "application/octet-stream";  // Unknown file type

            if (image.FilePath != null)
                request.AddFile("file", image.FilePath, mimeType);
            else
                request.AddFile("file", image.File, image.FileName, mimeType);

            request.AddParameter("purpose", image.Purpose.ToString().ToLower());
            request.AddParameter("ref", image.Reference);

            return Execute<ImageResponse>(request).Images[0];
        }
    }
}


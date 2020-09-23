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

            if (image.FilePath != null)
                request.AddFile("file", image.FilePath, GetMimeType(image.ImageType));
            else
                request.AddFile("file", image.File, image.FileName, GetMimeType(image.ImageType));

            request.AddParameter("purpose", image.Purpose.ToString().ToLower());
            request.AddParameter("ref", image.Reference);

            return Execute<ImageResponse>(request).Images[0];
        }

        private string GetMimeType(ImageType imageType)
        {
            switch (imageType)
            {
                case ImageType.GIF:
                    return "image/gif";
                case ImageType.ICO:
                    return "image/x-icon";
                case ImageType.JPEG:
                    return "image/jpeg";
                case ImageType.PNG:
                    return "image/png";
                case ImageType.SVG:
                    return "image/svg+xml";
                case ImageType.Unknown:
                default:
                    return "application/octet-stream";
            }
        }
    }
}

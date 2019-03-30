using GhostSharp.Entities;
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
            request.AddHeader("Content-Type", "multipart/form-data;");
            request.AddFileBytes("file", image.File, "upload.jpg");
            request.AddParameter("purpose", image.Purpose.ToString());
            request.AddParameter("ref", image.Reference);

            return Execute<ImageResponse>(request).Images[0];
        }


        // what happens when the purpose is invalid?

        // can i upload file using filename? prob not - file is just another blob?
    }
}


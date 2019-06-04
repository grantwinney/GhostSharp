using GhostSharp.Enums;
using Newtonsoft.Json;
using System.IO;

namespace GhostSharp.Entities
{
    public class ImageRequest
    {
        /// <summary>
        /// Read in an image file from the given file path, and specify an optional filename.
        /// </summary>
        /// <param name="filePath">The file path of the image file to load.</param>
        /// <remarks>
        /// The file name and mime type are determined from the filename.
        /// </remarks>
        public ImageRequest(string filePath)
        {
            FilePath = filePath;
            ImageType = ExtractImageTypeFromFilePath(filePath);
        }

        /// <summary>
        /// Use a byte array representing an image file, and specify a filename.
        /// </summary>
        /// <param name="file">The byte array representing the image file.</param>
        /// <param name="fileName">The filename to assign the image.</param>
        /// <param name="imageType">The image type.</param>
        public ImageRequest(byte[] file, string fileName, ImageType imageType)
        {
            File = file;
            FileName = fileName;
            ImageType = imageType;
        }

        /// <summary>
        /// The file path of the image file to load.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// The byte array representing the image file.
        /// </summary>
        public byte[] File { get; }

        /// <summary>
        /// The filename assigned to the image.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// The image type.
        /// </summary>
        public ImageType ImageType { get; }

        /// <summary>
        /// Intended use for the image (affects validation)
        /// </summary>
        /// <remarks>
        /// The supported formats for image and profile_image are JPEG, GIF, PNG and SVG.
        /// Supported formats for icon are ICO and PNG. profile_image must be square.
        /// </remarks>
        [JsonProperty("purpose")]
        public ImagePurpose Purpose { get; set; }

        /// <summary>
        /// Reference or identifier for the image, if one was provided
        /// </summary>
        /// <remarks>
        /// A reference or identifier for the image, e.g. the original filename and path.
        /// Will be returned as-is in the API response, making it useful for finding & replacing local image paths after uploads.
        /// </remarks>
        [JsonProperty("ref")]
        public string Reference { get; set; }

        /// <summary>
        /// Given a filepath, determines the mimetype by looking at the extension.
        /// </summary>
        /// <param name="filePath">A filepath from which to extract the mimetype.</param>
        /// <returns></returns>
        private ImageType ExtractImageTypeFromFilePath(string filePath)
        {
            switch (Path.GetExtension(filePath).ToLower())
            {
                case ".jpe":
                case ".jpeg":
                case ".jpg":
                    return ImageType.JPEG;
                case ".gif":
                    return ImageType.GIF;
                case ".ico":
                    return ImageType.ICO;
                case ".png":
                    return ImageType.PNG;
                case ".svg":
                case ".svgz":
                    return ImageType.SVG;
                default:
                    return ImageType.Unknown;
            }
        }
    }
}

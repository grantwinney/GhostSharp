namespace GhostSharp.Entities
{
    public class ThemeRequest
    {
        /// <summary>
        /// Read in a theme file from the given file path.
        /// </summary>
        /// <param name="filePath">The file path of the theme file to load.</param>
        /// <remarks>
        /// The file name and mime type are determined from the filename.
        /// </remarks>
        public ThemeRequest(string filePath)
        {
            FilePath = filePath;
        }

        /// <summary>
        /// Use a byte array representing an image file, and specify a filename.
        /// </summary>
        /// <param name="file">The byte array representing the image file.</param>
        /// <param name="fileName">The filename to assign the image.</param>
        public ThemeRequest(byte[] file, string fileName)
        {
            File = file;
            FileName = fileName;
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
    }
}

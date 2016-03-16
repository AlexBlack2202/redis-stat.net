namespace redis_stat.net.common.IO
{
    public interface IFileManager
    {
        /// <summary>The read file.</summary>
        /// <param name="path">The path.</param>
        /// <returns>The <see cref="string"/>.</returns>
        string ReadFile(string path);

        /// <summary>The create folder.</summary>
        /// <param name="path">The path.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool CreateFolder(string path);

        /// <summary>The folder exist.</summary>
        /// <param name="path">The path.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool FolderExist(string path);

        /// <summary>The folder exist.</summary>
        /// <param name="folderName">The project type.</param>
        /// <param name="path">The path.</param>
        /// <returns>The <see cref="string"/>.</returns>
        string FolderExist(string folderName, string path);

        /// <summary>The write file.</summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The contents.</param>
        void WriteFile(string path, string contents);
    }
}
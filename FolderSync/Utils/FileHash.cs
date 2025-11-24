using System.Security.Cryptography;

namespace FolderSync.Utils
{
    /// <summary>
    /// Provides hashing utilities for file content using secure algorithms.
    /// </summary>
    public static class FileHash
    {
        /// <summary>
        /// Computes the SHA-256 hash of a file and returns it as a lowercase hexadecimal string.
        /// </summary>
        /// <param name="filePath">The full path of the file to hash.</param>
        /// <returns>The SHA-256 hash as a lowercase hex string.</returns>
        /// <exception cref="ArgumentException">Thrown when the file path is null or empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown when the specified file does not exist.</exception>
        public static string HashSHA256(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found.", filePath);

            using (var sha256 = SHA256.Create())
            using (var stream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite))
            {
                var hash = sha256.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}

using FolderSync.Utils;

public class FileHashTests
{
    [Fact]
    public void HashSHA256_SameFile_ReturnsSameHash()
    {
        // Arrange
        string path = "testfile.txt";
        File.WriteAllText(path, "Hello World");

        // Act
        string hash1 = FileHash.HashSHA256(path);
        string hash2 = FileHash.HashSHA256(path);

        // Assert
        Assert.Equal(hash1, hash2);

        // Cleanup
        File.Delete(path);
    }

    [Fact]
    public void HashSHA256_DifferentFiles_ReturnsDifferentHashes()
    {
        // Arrange
        string path1 = "file1.txt";
        string path2 = "file2.txt";
        File.WriteAllText(path1, "Content A");
        File.WriteAllText(path2, "Content B");

        // Act
        string hash1 = FileHash.HashSHA256(path1);
        string hash2 = FileHash.HashSHA256(path2);

        // Assert
        Assert.NotEqual(hash1, hash2);

        // Cleanup
        File.Delete(path1);
        File.Delete(path2);
    }
}

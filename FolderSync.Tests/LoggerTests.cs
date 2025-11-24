using FolderSync.Utils;

public class LoggerTests
{
    [Fact]
    public void Info_WritesMessageToFileAndConsole()
    {
        // Arrange
        string logPath = "testlog.txt";
        var logger = new Logger(logPath);

        // Act
        logger.Info("Test info message");

        // Assert
        string content = File.ReadAllText(logPath);
        Assert.Contains("Test info message", content);

        // Cleanup
        File.Delete(logPath);
    }

    [Fact]
    public void Error_WritesMessageToFileAndConsole()
    {
        string logPath = "testlog.txt";
        var logger = new Logger(logPath);

        logger.Error("Test error message");

        string content = File.ReadAllText(logPath);
        Assert.Contains("Test error message", content);

        File.Delete(logPath);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.Utils
{
    public class Logger
    {
        private readonly string _logFilePath;

        public Logger(string logFilePath)
        {
            if (string.IsNullOrWhiteSpace(logFilePath))
                throw new ArgumentException("Log file path cannot be empty.");

            _logFilePath = logFilePath;

            string? dir = Path.GetDirectoryName(_logFilePath);
            if (!string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }

            SafeWrite($"[START] Logging started at {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
        }

        public void Info(string message)
        {
            string log = $"[INFO] {DateTime.Now:dd/MM/yyyy HH:mm:ss}: {message}";
            Console.WriteLine(log);
            File.AppendAllText(_logFilePath, log + Environment.NewLine);
        }

        public void Error(string message)
        {
            string log = $"[ERROR] {DateTime.Now:dd/MM/yyyy HH:mm:ss}: {message}";
            Console.WriteLine(log);
            File.AppendAllText(_logFilePath, log + Environment.NewLine);
        }

        private void SafeWrite(string text)
        {
            try
            {
                File.AppendAllText(_logFilePath, text + Environment.NewLine);
            }
            catch
            {
                Console.WriteLine("[LOGGER ERROR] Failed to write to log file.");
            }
        }
    }
}

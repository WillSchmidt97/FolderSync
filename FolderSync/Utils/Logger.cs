using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.Utils
{
    public class Logger
    {
        private static string _logFilePath;

        public Logger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public static void Init(string logFilePath)
        {
            _logFilePath = logFilePath;

            File.WriteAllTextAsync(_logFilePath, $"[START] Logging started at {DateTime.Now}\n").Wait();
        }

        public static void Info(string message)
        {
            string line = $"[INFO] {DateTime.Now}: {message}";
            Console.Write(line);

            File.AppendAllTextAsync(_logFilePath, message + Environment.NewLine).Wait();
        }

        public static void Error(string message, Exception ex = null)
        {
            string line = $"[ERROR] {DateTime.Now}: {message} {ex?.Message}";
            Console.WriteLine(line);
            File.AppendAllTextAsync(_logFilePath, line + Environment.NewLine).Wait();
        }
    }
}

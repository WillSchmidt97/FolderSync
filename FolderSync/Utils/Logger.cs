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
            _logFilePath = logFilePath;

            File.AppendAllText(
                _logFilePath,
                $"[START] Logging started at {DateTime.Now:dd/MM/yyyy HH:mm:ss}{Environment.NewLine}"
            );
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
    }
}

using FolderSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.Utils
{
    public static class CommandLine
    {
        public static SyncConfig Parse(string[] args)
        {
            if (args is null || args.Length == 0)
                throw new ArgumentException("No argument given");

            SyncConfig config = new SyncConfig();

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--source":
                        config.SourcePath = Next(args, ref i);
                        break;

                    case "--replica":
                        config.ReplicaPath = Next(args, ref i);
                        break;

                    case "--interval":
                        if (!int.TryParse(Next(args, ref i), out int interval))
                            throw new ArgumentException("Invalid value for --interval. Please, type a number here.");
                        config.IntervalSeconds = interval;
                        break;

                    case "--log":
                        config.LogFilePath = Next(args, ref i);
                        break;

                    case "--once":
                        config.Once = true;
                        break;

                    default:
                        throw new ArgumentException($"Invalid argument: {args[i]}");
                }
            }


            // Argument validation.

            if (string.IsNullOrWhiteSpace(config.SourcePath))
                throw new ArgumentException("Source path is required.");

            if (string.IsNullOrWhiteSpace(config.ReplicaPath))
                throw new ArgumentException("Replica path is required.");

            if (string.IsNullOrEmpty(config.LogFilePath))
                throw new ArgumentException("Log file path is required.");

            if (!config.Once && config.IntervalSeconds <= 0)
                throw new ArgumentException("Interval must be greater than zero when --once is not used.");


            return config;
        }

        private static string Next(string[] args, ref int i)
        {
            if (i + 1 >= args.Length)
                throw new ArgumentException($"Argument expected after '{args[i]}'");
            return args[++i];
        }
    }
}

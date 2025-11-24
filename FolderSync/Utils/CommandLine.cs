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

            var config = new SyncConfig();

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--source":
                        config.SourcePath = args[++i];
                        break;

                    case "--replica":
                        config.ReplicaPath = args[++i];
                        break;

                    case "--interval":
                        config.IntervalSeconds = int.Parse(args[++i]);
                        break;
                    case "--log":
                        config.LogFilePath = args[++i];
                        break;

                    case "--once":
                        config.Once = true;
                        break;

                    default:
                        throw new ArgumentException($"Invalid argument: {args[i]}");
                }
            }

            return config;
        }
    }
}

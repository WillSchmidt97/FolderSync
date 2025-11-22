using FolderSync.Models;
using FolderSync.Services;
using FolderSync.Utils;
using System;
using System.Threading.Tasks;

namespace FolderSync
{
    internal class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var config = CommandLine.Parse(args);
            var logger = new Logger(config.LogFilePath);
            var syncService = new SyncService(config, logger);

            logger.Info("Settings successfully loaded.");
            logger.Info($"Source: {config.SourcePath}");
            logger.Info($"Replica: {config.ReplicaPath}");
            logger.Info($"Interval: {config.IntervalSeconds} seconds");

            while (true)
            {
                syncService.PerformSync();
                await Task.Delay(config.IntervalSeconds * 1000);
            }
        }
    }
}

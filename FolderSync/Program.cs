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

            if (config.Once)
            {
                logger.Info($"Once: set");
                syncService.PerformSync();
                logger.Info("Single synchronization finished.");

                return 0;
            }

            using var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                logger.Info("Cancel requested via console.");
                cts.Cancel();
            };

            try
            {
                while (!cts.IsCancellationRequested)
                {
                    syncService.PerformSync();

                    await Task.Delay(config.IntervalSeconds * 1000, cts.Token);
                }
            }
            catch (TaskCanceledException)
            {
                logger.Info("Cancellation detected. Exiting gracefully.");
            }
            catch (Exception ex)
            {
                logger.Error($"Fatal error in loop: {ex.Message}");
                return 1;
            }

            return 0;
        }
    }
}

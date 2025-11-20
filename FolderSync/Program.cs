using FolderSync.Models;
using FolderSync.Utils;
using System;
using System.Threading.Tasks;

namespace FolderSync
{
    internal class Program
    {
        public static async Task<int> Main(string[] args)
        {
            // Parse args
            SyncConfig syncConfig = CommandLine.Parse(args);

            // Initialize logger
            Logger.Init(syncConfig.LogFilePath);
            Logger.Info("Settings successfully loaded.");
            Logger.Info($"Source: {syncConfig.SourcePath}");
            Logger.Info($"Replica: {syncConfig.ReplicaPath}");
            Logger.Info($"Interval: {syncConfig.IntervalSeconds} seconds");

            return 0;
        }
    }
}

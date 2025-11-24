using FolderSync.Models;
using FolderSync.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.Services
{
    public class SyncService
    {
        private readonly SyncConfig _config;
        private readonly Logger _logger;

        public SyncService(SyncConfig config, Logger logger)
        {
            _config = config;
            _logger = logger;
        }

        public void PerformSync()
        {
            try
            {
                _logger.Info("Start synchronization.");

                // Creating the given source path if does not exist.
                if (!Directory.Exists(_config.SourcePath))
                    Directory.CreateDirectory(_config.SourcePath);

                // Creating the given replica path if it does not exist.
                if (!Directory.Exists(_config.ReplicaPath))
                    Directory.CreateDirectory(_config.ReplicaPath);

                SyncDirectory(_config.SourcePath, _config.ReplicaPath);
                RemovedDeletedFiles(_config.SourcePath, _config.ReplicaPath);

                _logger.Info("Synchronization finished successfully.");
            }
            catch(Exception ex)
            {
                _logger.Error($"Unexpected error: {ex.Message}");
            }
        }

        private void SyncDirectory(string source, string replica)
        {
            foreach (string sourceFile in Directory.GetFiles(source))
            {
                string fileName = Path.GetFileName(sourceFile);
                string replicaFile = Path.Combine(replica, fileName);

                if (!File.Exists(replicaFile) || DifferentFiles(sourceFile, replicaFile)) 
                {
                    File.Copy(sourceFile, replicaFile, true);
                    _logger.Info($"File copied: {fileName}");
                }
            }

            foreach (string sourceDir in Directory.GetDirectories(source))
            {
                string dirName = Path.GetFileName(sourceDir);
                string replicaDir = Path.Combine(replica, dirName);

                if (!Directory.Exists(replicaDir))
                {
                    Directory.CreateDirectory(replicaDir);
                    _logger.Info($"Directory created: {dirName}");
                }

                SyncDirectory(sourceDir, replicaDir);
            }
        }

        private void RemovedDeletedFiles(string source, string replica)
        {
            foreach (string replicaFile in Directory.GetFiles(replica))
            {
                string fileName = Path.GetFileName(replicaFile);
                string sourceFile = Path.Combine(source, fileName);

                if (!File.Exists(sourceFile))
                {
                    File.Delete(replicaFile);
                    _logger.Info($"File removed: {fileName}");
                }
            }

            foreach (string replicaDir in Directory.GetDirectories(replica))
            {
                string dirName = Path.GetFileName(replicaDir);
                string sourceDir = Path.Combine(source, dirName);

                if (!Directory.Exists(sourceDir))
                {
                    Directory.Delete(replicaDir, true);
                    _logger.Info($"Directory removed: {dirName}");
                }
                else
                {
                    RemovedDeletedFiles(sourceDir, replicaDir);
                }
            }
        }

        private bool DifferentFiles(string sourceFile, string replicaFile)
        {
            string sourceHash = FileHash.HashSHA256(sourceFile);
            string replicaHash = FileHash.HashSHA256(replicaFile);

            return sourceHash != replicaHash;
        }
    }
}

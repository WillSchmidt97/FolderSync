using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.Models
{
    public class SyncConfig
    {
        public string SourcePath { get; set; } = string.Empty;
        public string ReplicaPath { get; set; } = string.Empty;
        public int IntervalSeconds { get; set; }
        public string LogFilePath { get; set; } = string.Empty;
        public bool Once {  get; set; }
    }
}

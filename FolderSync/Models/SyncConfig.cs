using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.Models
{
    public class SyncConfig
    {
        public string? SourcePath { get; set; }
        public string? ReplicaPath { get; set; }
        public int IntervalSeconds { get; set; }
        public string? LogFilePath { get; set; }
        public bool Once {  get; set; }
    }
}

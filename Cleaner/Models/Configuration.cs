using System;
using System.Collections.Generic;

namespace Cleaner.Models
{
    [Serializable]
    public class Configuration
    {
        public List<string> DirectoryList { get; } = new List<string>();
        public List<string> FolderList { get; } = new List<string>();
    }
}
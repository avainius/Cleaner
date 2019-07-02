using System.IO;

namespace Cleaner.Helpers
{
    public static class FileManager
    {
        public static void CreateFileIfNotExists(string fullPath)
        {
            if (File.Exists(fullPath)) return;
            string directory = Path.GetDirectoryName(fullPath);

            CreateDirIfNotExists(directory);
            File.Create(fullPath).Close();
        }

        public static void CreateDirIfNotExists(string logDirectory)
        {
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }

        public static void DeleteFileIfExists(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
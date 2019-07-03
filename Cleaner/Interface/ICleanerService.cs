using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cleaner.Interface
{
    public interface ICleanerService
    {
        Task DeleteFolder(string path);
        Task DeleteFolder(List<string> paths);
        Task DeleteFolder(string directory, List<string> folderNames);
        Task DeleteFolder(List<string> directories, string folderName);
        Task DeleteFolder(List<string> directories, List<string> folderNames);
        Task DeleteDirectory(string directory);
        Task<string[]> GetFolderDirectories(string path, string folder);
    }
}
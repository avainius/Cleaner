using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cleaner.Interface
{
    public interface ICleanerService
    {
        Task DeleteFolder(string path);
        Task DeleteFolder(IEnumerable<string> paths);
        Task DeleteFolder(string directory, IEnumerable<string> folderNames);
        Task DeleteFolder(IEnumerable<string> directories, string folderName);
        Task DeleteFolder(IEnumerable<string> directories, IEnumerable<string> folderNames);
        Task DeleteDirectory(string directory);
        Task<string[]> GetFolderDirectories(string path, string folder);
    }
}
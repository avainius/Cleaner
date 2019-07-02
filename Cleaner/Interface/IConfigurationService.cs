using System.Collections.Generic;
using System.Threading.Tasks;
using Cleaner.Models;

namespace Cleaner.Interface
{
    public interface IConfigurationService
    {
        Configuration Configuration { get; }
        Task SaveConfiguration();
        Task SaveConfigurationAndRestart();
        Task AddDirectory(string directory);
        Task AddFolderName(string folderName);
        Task<List<string>> GetDirectories();
        Task<List<string>> GetFolders();
        Task<Configuration> GetConfiguration();
        Task DeleteConfiguration();
    }
}
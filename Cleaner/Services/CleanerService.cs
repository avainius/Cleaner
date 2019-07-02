using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cleaner.Helpers;
using Cleaner.Interface;

namespace Cleaner.Services
{
    public class CleanerService : ICleanerService
    {
        public async Task DeleteFolder(string path)
        {
            await DeleteDirectory(path).ConfigureAwait(false);
        }

        public async Task DeleteFolder(IEnumerable<string> paths)
        {
            foreach (string path in paths)
            {
                await DeleteDirectory(path).ConfigureAwait(false);
            }
        }

        public async Task DeleteFolder(string directory, IEnumerable<string> folderNames)
        {
            var tasks = new List<Task>();
            foreach (string folderName in folderNames)
            {
                string[] fullPaths = await GetFolderDirectories(directory, folderName).ConfigureAwait(false);
                tasks.AddRange(fullPaths.Select(DeleteDirectory));
            }

            Task.WaitAll(tasks.ToArray());
        }

        public async Task DeleteFolder(IEnumerable<string> directories, string folderName)
        {
            var tasks = new List<Task>();
            foreach (string directory in directories)
            {
                string[] fullPaths = await GetFolderDirectories(directory, folderName).ConfigureAwait(false);
                tasks.AddRange(fullPaths.Select(DeleteDirectory));
            }

            Task.WaitAll(tasks.ToArray());
        }

        public async Task DeleteFolder(IEnumerable<string> directories, IEnumerable<string> folderNames)
        {
            var tasks = new List<Task>();
            foreach (string folderName in folderNames)
            {
                foreach (string directory in directories)
                {
                    string[] fullPaths = await GetFolderDirectories(directory, folderName).ConfigureAwait(false);
                    tasks.AddRange(fullPaths.Select(DeleteDirectory));
                }
            }

            Task.WaitAll(tasks.ToArray());
        }

        public async Task DeleteDirectory(string directory)
        {
            try
            {
                Directory.Delete(directory, true);
                await Logger.Log($"Deleted: {directory}").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.Log(ex).ConfigureAwait(false);
                await Logger.Log($"Failed to delete: {directory}").ConfigureAwait(false);
            }
        }

        public async Task<string[]> GetFolderDirectories(string path, string folder)
        {
            string[] fullPaths = Directory.GetDirectories(path, folder, SearchOption.AllDirectories);
            foreach (string fullPath in fullPaths)
            {
                await Logger.Log($"Found: {fullPath}").ConfigureAwait(false);
            }

            return fullPaths;
        }
    }
}
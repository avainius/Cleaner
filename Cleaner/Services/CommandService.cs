using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cleaner.Helpers;
using Cleaner.Interface;

namespace Cleaner.Services
{
    public class CommandService : ICommandService
    {
        private readonly IConfigurationService _configurationService;
        private readonly ICleanerService _cleanerService;
        private readonly string InvalidCommandText = "Invalid command exception";

        public CommandService(IConfigurationService configurationService, ICleanerService cleanerService)
        {
            _configurationService = configurationService;
            _cleanerService = cleanerService;
        }

        public async Task ProcessCommand(string command)
        {

        }

        private async Task Execute()
        {
            List<string> directories = await _configurationService.GetDirectories().ConfigureAwait(false);
            List<string> folders = await _configurationService.GetFolders().ConfigureAwait(false);
            await _cleanerService.DeleteFolder(directories, folders).ConfigureAwait(false);
        }

        private async Task AddDirectory(string command)
        {
            string[] values = command.Split(' ');
            if (values.Length != 2)
            {
                throw new Exception($"{InvalidCommandText}: {command}");
            }

            await _configurationService.AddDirectory(values[1]).ConfigureAwait(false);
            await _configurationService.SaveConfiguration().ConfigureAwait(false);
        }



        private async Task Help()
        {
            await Logger.Log("Available commands:").ConfigureAwait(false);
        }
    }
}
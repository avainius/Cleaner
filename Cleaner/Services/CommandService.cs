using System;
using System.Collections.Generic;
using System.Globalization;
using Cleaner.Defaults;
using Cleaner.Helpers;
using Cleaner.Interface;

namespace Cleaner.Services
{
    public class CommandService : ICommandService
    {
        private IConfigurationService configurationService;
        private IEnumerable<ICleanerService> cleanerServices;
        private readonly string InvalidCommandText = "Invalid command exception";

        public CommandService(IConfigurationService configurationService, IEnumerable<ICleanerService> cleanerServices)
        {
            this.configurationService = configurationService;
            this.cleanerServices = cleanerServices;
        }

        public void ProcessCommand(string command)
        {
            if (command.StartsWith(SupportedCommands.AddDirectory, StringComparison.CurrentCultureIgnoreCase))
            {
                AddDirectory(command);
            }
            else if (command.StartsWith(SupportedCommands.Help, StringComparison.CurrentCultureIgnoreCase))
            {
                Help();
            }
        }

        private void AddDirectory(string command)
        {
            string[] values = command.Split(' ');
            if (values.Length != 2)
            {
                throw new Exception($"{InvalidCommandText}: {command}");
            }

            configurationService.AddDirectory(values[1]);
            configurationService.SaveConfiguration();
        }

        private void Help()
        {
            Logger.Log("Available commands:");
            Logger.Log(SupportedCommands.AddDirectory);
            Logger.Log(SupportedCommands.AddFolder);
            Logger.Log(SupportedCommands.Execute);
        }
    }
}
using System;
using System.Threading.Tasks;
using Cleaner.Helpers;
using Cleaner.Interface;
using Cleaner.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cleaner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ServiceProvider serviceProvider = serviceCollection
                .AddLogging()
                .AddSingleton<ICleanerService, CleanerService>()
                .AddSingleton<IConfigurationService, ConfigurationService>()
                .AddSingleton<ICommandService, CommandService>()
                .BuildServiceProvider();
            
            ICommandService commandService = serviceProvider.GetService<ICommandService>();

            string command = Console.ReadLine();
            while (command != "quit")
            {
                await commandService.ProcessCommand(command).ConfigureAwait(false);
                command = Console.ReadLine();
            }

            await Logger.Log("Done").ConfigureAwait(false);
        }
    }
}

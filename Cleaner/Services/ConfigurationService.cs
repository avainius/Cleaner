using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Cleaner.Helpers;
using Cleaner.Interface;
using Cleaner.Models;

namespace Cleaner.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private string ConfigurationDirectory => $"{AppDomain.CurrentDomain.BaseDirectory}";
        private string FullPath => $"{ConfigurationDirectory}configurations.conf";
        public Configuration Configuration { get; }

        public ConfigurationService()
        {
            Configuration = GetConfiguration().Result;
        }

        public async Task SaveConfiguration()
        {
            try
            {
                var xmlDocument = new XmlDocument();
                var serializer = new XmlSerializer(Configuration.GetType());
                using (var stream = new MemoryStream())
                {
                    serializer.Serialize(stream, Configuration);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(FullPath);
                }
            }
            catch (Exception ex)
            {
                await Logger.Log(ex).ConfigureAwait(false);
            }
        }

        public async Task SaveConfigurationAndRestart()
        {
            try
            {
                await SaveConfiguration().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.Log(ex).ConfigureAwait(false);
            }
            finally
            {
                System.Diagnostics.Process.Start(AppDomain.CurrentDomain.FriendlyName);
                Environment.Exit(0);
            }
        }

        public Task AddDirectory(string directory)
        {
            Configuration.DirectoryList.Add(directory);
            return Task.CompletedTask;
        }

        public Task AddFolderName(string folderName)
        {
            Configuration.FolderList.Add(folderName);
            return Task.CompletedTask;
        }

        public Task<List<string>> GetDirectories()
        {
            return Task.FromResult(Configuration.DirectoryList);
        }

        public Task<List<string>> GetFolders()
        {
            return Task.FromResult(Configuration.FolderList);
        }

        public async Task<Configuration> GetConfiguration()
        {
            Configuration objectOut = null;

            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(FullPath);
                string xmlString = xmlDocument.OuterXml;

                using (var read = new StringReader(xmlString))
                {
                    Type outType = typeof(Configuration);
                    var serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (Configuration)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                await Logger.Log(ex).ConfigureAwait(false);
            }

            return objectOut ?? new Configuration();
        }

        public async Task DeleteConfiguration()
        {
            try
            {
                FileManager.DeleteFileIfExists(FullPath);
            }
            catch (Exception ex)
            {
                await Logger.Log(ex).ConfigureAwait(false);
            }
        }
    }
}
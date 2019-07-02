using System;
using System.IO;
using System.Threading.Tasks;

namespace Cleaner.Helpers
{
    public static class Logger
    {
        private static string LogDirectory => $@"{AppDomain.CurrentDomain.BaseDirectory}Log\";
        private static string FullPath => $"{LogDirectory}Log {DateTime.Now.Date:yyyy-MM-dd}.txt";

        public static Task Log(Exception e)
        {
            Task result;
            try
            {
                FileManager.CreateFileIfNotExists(FullPath);
                AppendLine(e.Message);
                AppendLine(e.StackTrace);
                result = Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(TimeStampedMessage($"Log error: {ex.Message}"));
                result = Task.FromException(ex);
            }

            Console.WriteLine(e.Message);

            return result;
        }

        public static Task Log(string message)
        {
            Task result;
            try
            {
                FileManager.CreateFileIfNotExists(FullPath);
                AppendLine(message);
                Console.WriteLine(message);
                result = Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Log(ex);
                result = Task.FromException(ex);
            }

            return result;
        }

        private static void AppendLine(string line)
        {
            using (StreamWriter streamWriter = File.CreateText(FullPath))
            {
                streamWriter.WriteLine(TimeStampedMessage(line));
            }
        }

        private static string TimeStampedMessage(string line)
        {
            return $"[{DateTime.Now}] {line}";
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Common;

namespace Asynchronous
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var t = HttpClientTester.AccessTheWebAsync();
            t.Wait();
            //CopyFiles();
        }

        private static void CopyFiles()
        {
            const string sourceTFolderTextOnly = @"C:\TestData\source";
            const string sourceTFolderMkvOnly = @"C:\TestData\source\1";

            const string destinationFolder = @"C:\TestData\destination";
            const string txtFilter = @"*.txt";
            const string allFilter = @"*.*";

            ClearDestinationFolder(destinationFolder);

            List<Task> taskList = new List<Task>();

            FileReader reader = new FileReader();
            taskList.Add(reader.ReadCopyFolderAsync(sourceTFolderTextOnly, destinationFolder, txtFilter));
            taskList.Add(reader.ReadCopyFolderAsync(sourceTFolderMkvOnly, destinationFolder, allFilter));
            Console.WriteLine("Returned to Main");

            Task.WaitAll(taskList.ToArray());

            Console.WriteLine("All done.");
        }

        private static void ClearDestinationFolder(string folder)
        {
            foreach (var fileName in Directory.EnumerateFiles(folder))
            {
                File.Delete(fileName);
            }
        }
    }
}
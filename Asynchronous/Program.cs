using System;
using System.IO;
using Common;

namespace Asynchronous
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            const string sourceFolder = @"C:\TestData\source";
            const string destinationFolder = @"C:\TestData\destination";
            const string filter = @"*.txt";

            ClearDestinationFolder(destinationFolder);
            FileReader reader = new FileReader();
            reader.ReadCopyFolderAsync(sourceFolder, destinationFolder, filter);
        }

        private static void ClearDestinationFolder(string folder)
        {
            foreach(var fileName in Directory.EnumerateFiles(folder))
            {
                File.Delete(fileName);
            }
        }
    }
}

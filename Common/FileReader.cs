using System;
using System.IO;
using System.Threading.Tasks;

namespace Common
{
    public class FileReader
    {
        public async Task ReadCopyFolderAsync(string sourceFolder, string destinationFolder, string filter)
        {
            foreach(string fileName in Directory.EnumerateFiles(sourceFolder, filter))
            {
                string destinationFile = Path.Combine(destinationFolder, Path.GetFileName(fileName));
                using(var sourceStream = File.Open(fileName, FileMode.Open))
                {
                    using(var destinationStream = File.Create(destinationFile))
                    {
                        Console.WriteLine("Copying file, from '{0}' to '{1}'", fileName, destinationFile);
                        await sourceStream.CopyToAsync(destinationStream);
                        Console.WriteLine("Copied file, from '{0}' to '{1}'", fileName, destinationFile);
                    }
                }
            }
        }
    }
}
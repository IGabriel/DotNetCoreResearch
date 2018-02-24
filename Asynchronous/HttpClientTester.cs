using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchronous
{
    public class HttpClientTester
    {
        public static async Task<int> AccessTheWebAsync()
        {
            List<string> urlList = new List<string>
            {
                "http://msdn.microsoft.com",
                "http://msdn.microsoft.com/library/hh290138.aspx",
                "http://msdn.microsoft.com/library/hh290140.aspx",
                "http://msdn.microsoft.com/library/dd470362.aspx",
                "http://msdn.microsoft.com/library/aa578028.aspx",
                "http://msdn.microsoft.com/library/ms404677.aspx",
                "http://msdn.microsoft.com/library/ff730837.aspx"
            };

            StringBuilder content = new StringBuilder();
            CancellationTokenSource source = new CancellationTokenSource();
            HttpClient client = new HttpClient();

            var downloadTasksQuery = (from url in urlList select  ProcessURLAsync(url, client, source.Token)).ToArray();
            // Task<int> firstFinishTask = await Task.WhenAny(downloadTasksQuery);
            //int length = await firstFinishTask;
            //source.Cancel();
            //Console.WriteLine($"First finish length: {length}");

            // Task<int[]> firstFinishTask = Task.WhenAll<int>(downloadTasksQuery);
            // int[] lengths = await firstFinishTask;
            //Console.WriteLine($"First finish length: {lengths.Sum()}");
            int totalLength = (await Task.WhenAll<int>(downloadTasksQuery)).Sum();
            Console.WriteLine($"First finish length: {totalLength}");

            return totalLength;
        }

        private static async Task<int> ProcessURLAsync(string url, HttpClient client, CancellationToken ct)  
        {
            Console.WriteLine($"Downloading url: {url}");

            // GetAsync returns a Task<HttpResponseMessage>.   
            HttpResponseMessage response = await client.GetAsync(url, ct);  
            // Retrieve the website contents from the HttpResponseMessage.  
            byte[] urlContents = await response.Content.ReadAsByteArrayAsync();  

            Console.WriteLine($"Downloaded. url: {url}, length: {urlContents.Length}");
            return urlContents.Length;  
        }  
    }
}
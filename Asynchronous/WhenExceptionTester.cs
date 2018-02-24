using System;
using System.Threading.Tasks;

namespace Asynchronous
{
    public class WhenExceptionTester
    {
        public static async Task DoMultipleAsync()
        {
            Task theTask1 = ExcAsync(info: "First Task");
            Task theTask2 = ExcAsync(info: "Second Task");
            Task theTask3 = ExcAsync(info: "Third Task");

            Task allTasks = Task.WhenAll(theTask1, theTask2, theTask3);

            try
            {
                await allTasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Task IsFaulted: " + allTasks.IsFaulted);
                foreach (var inEx in allTasks.Exception.InnerExceptions)
                {
                    Console.WriteLine("Task Inner Exception: " + inEx.Message);
                }
            }
        }

        private static async Task ExcAsync(string info)
        {
            Random r = new Random();
            int delay = r.Next(10, 3000);
            await Task.Delay(delay);
            Console.WriteLine($"Sleep {delay} milliseconds");

            throw new Exception($"Error-{info}");
        }

    }
}
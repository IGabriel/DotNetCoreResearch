

using System;
using System.Threading.Tasks;

namespace Asynchronous
{    
    public class ExceptionTester
    {
        
        private static async Task<string> DelayAsync()
        {
            await Task.Delay(100);

            // Uncomment each of the following lines to
            // demonstrate exception handling.

            //throw new OperationCanceledException("canceled");
            throw new Exception("Something happened.");
            //return "Done";
        }

        public static async Task DoSomethingAsync()
        {
            Task<string> theTask = DelayAsync();

            try
            {
                string result = await theTask;
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Message: " + ex.Message);
            }
            Console.WriteLine("Task IsCanceled: " + theTask.IsCanceled);
            Console.WriteLine("Task IsFaulted:  " + theTask.IsFaulted);
            if (theTask.Exception != null)
            {
                Console.WriteLine("Task Exception Message: " +
                    theTask.Exception.Message);
                Console.WriteLine("Task Inner Exception Message: " +
                    theTask.Exception.InnerException.Message);
            }
        }
    }
}
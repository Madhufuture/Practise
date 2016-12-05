using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Practise
{
    public class Program
    {
        static void Main()
        {
            Task task = new Task(ProcessDataAsync);
            task.Start();
            task.Wait();


            Console.ReadLine();
        }

        static async void ProcessDataAsync()
        {
            Task<int> task = HandleFileAsync(@"C:\Users\Madhusudhana\Desktop\AysncAwaitTest.txt");

            Console.WriteLine("Please wait while processing something");
            int x = await task;
            Console.WriteLine("count: "+ x);

        }

        static async Task<int> HandleFileAsync(string file)
        {
            Console.WriteLine("HandleFile enter");
            int count = 0;

            // Read in the specified file.
            // ... Use async StreamReader method.
            using (StreamReader reader = new StreamReader(file))
            {
                string v = await reader.ReadToEndAsync();

                // ... Process the file data somehow.
                count += v.Length;

                // ... A slow-running computation.
                //     Dummy code.
                for (int i = 0; i < 10000; i++)
                {
                    int x = v.GetHashCode();
                    if (x == 0)
                    {
                        count--;
                    }
                }
            }
            Console.WriteLine("HandleFile exit");
            return count;
        }

    }
}


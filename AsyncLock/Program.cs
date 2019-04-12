using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncLock
{
    class Program
    {
        static SemaphoreSlim sl = new SemaphoreSlim(1);

        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    // sb.Clear();
                    await Go();
                }
            });
            Console.ReadLine();
        }

        static async Task Go()
        {
            var token = CancellationToken.None;

            var tl = new List<Task>();

            for (int i = 0; i < 100; i++)
            {
                int k = i;

                /*var task = new Task<Task<int>>(async () =>
                {
                    await SyncFunc(k);
                    return 0;
                }, token);*/

                var task = Task.Run(async () =>
                {
                    await SyncFunc(k);
                });

                tl.Add(task);

                //  task.Start();
            }

            await Task.WhenAll(tl).ContinueWith(t =>
            {
                // lock(locker)
                Console.Write(sb.ToString());
            });//, TaskContinuationOptions.OnlyOnRanToCompletion);
               //.ContinueWith(task => Console.WriteLine("error"), TaskContinuationOptions.OnlyOnFaulted);

        }

        static async Task SyncFunc(object name)
        {
            try
            {

                int d = new Random().Next(1, 1000);
                await Task.Delay(d);
               // Thread.Sleep(d);

                await sl.WaitAsync();

                sb.AppendFormat("Begin_{0}: ", name);
                sb.AppendFormat("Name: {0} ", name);
                sb.AppendFormat("End_{0}{1}", name, Environment.NewLine);

            }
            finally
            {
                sl.Release();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSync
{
    public class MutexTest
    {
        static Mutex mut = new Mutex();

        static StringBuilder sb = new StringBuilder();

        static public void Go()
        {
            var token = CancellationToken.None;

            var tl = new List<Task>();

            for (int i = 0; i < 100; i++)
            {
                var task = new Task(SyncFunc, i, token);

                tl.Add(task);

                task.Start();
            }

            Task.WhenAll(tl).ContinueWith(t => { Console.Write(sb.ToString()); });

        }

        static void SyncFunc(object name)
        {
            try
            {
                mut.WaitOne();

                sb.AppendFormat("Begin_{0}: ", name);
                sb.AppendFormat("Name: {0} ", name);
                sb.AppendFormat("End_{0}{1}", name, Environment.NewLine);
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }
    }
}

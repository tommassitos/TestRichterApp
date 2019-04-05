using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UserSync
{
    class Program
    {
        static SpinLock sp = new SpinLock();

        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            Go();
            Console.ReadLine();
        }

        static void Go()
        {
            var token = new CancellationTokenSource(5000).Token;

            var tl = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                var task = new Task(SyncFunc, i, token);

                tl.Add(task);

                task.Start();
            }

            Task.WhenAll(tl).ContinueWith(t => { Console.Write(sb.ToString()); });

        }

        static void SyncFunc(object name)
        {
            Thread.Sleep(3000);

            bool gotLock = false;
            try
            {
                sp.Enter(ref gotLock);

                sb.AppendFormat("Begin_{0}: ", name);
                sb.AppendFormat("Name: {0} ", name);
                sb.AppendFormat("End_{0}{1}", name, Environment.NewLine);
            }
            finally
            {
                if (gotLock)
                    sp.Exit();
                else
                    Console.WriteLine("Not gotLock" + Environment.NewLine);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example4();
            ExampleMultyTask();
            Console.ReadLine();
        }

        static void Example4()
        {
            var parent = new Task(() =>
            {
                var cts = new CancellationTokenSource();
                var tf = new TaskFactory<int>(
                    cts.Token,
                    TaskCreationOptions.AttachedToParent,
                    TaskContinuationOptions.ExecuteSynchronously,
                    TaskScheduler.Default);

                var childTasks = new[]
                {
                tf.StartNew(()=> Sum(cts.Token,10000)),
                tf.StartNew(()=> Sum(cts.Token,20000)),
                tf.StartNew(()=> Sum(cts.Token,int.MaxValue))
            };

                for (var task = 0; task < childTasks.Length; task++)
                {
                    childTasks[task].ContinueWith(t => cts.Cancel(), TaskContinuationOptions.OnlyOnFaulted);
                }

                tf.ContinueWhenAll(childTasks,
                    completedTasks => completedTasks.Where(t => !t.IsFaulted && !t.IsCanceled).Max(t => t.Result), CancellationToken.None)
                    .ContinueWith(t => Console.WriteLine(t.Result), TaskContinuationOptions.ExecuteSynchronously);

            });

            parent.ContinueWith(p =>
            {
                var sb = new StringBuilder("Errors:" + Environment.NewLine);

                foreach (var e in p.Exception.Flatten().InnerExceptions)
                    sb.AppendLine(e.GetType().ToString());

                Console.WriteLine(sb.ToString());
            }, TaskContinuationOptions.OnlyOnFaulted);

            parent.Start();

            Console.ReadLine();
        }

        static void Example3()
        {
            var parent = new Task<int[]>(() =>
            {
                var results = new int[3];

                new Task(() => { results[0] = Sum(10000); }, TaskCreationOptions.AttachedToParent).Start();
                new Task(() => { results[1] = Sum(20000); }, TaskCreationOptions.AttachedToParent).Start();
                new Task(() => { results[2] = Sum(30000); }, TaskCreationOptions.AttachedToParent).Start();

                return results;
            });

            parent.ContinueWith(parentTask => Array.ForEach(parentTask.Result, Console.WriteLine));

            parent.Start();

            Console.ReadLine();
        }

        static void Example2()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            var t = Task.Run(() => Sum(cts.Token, 10000), cts.Token);

            t.ContinueWith(task => Console.WriteLine(task.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
            t.ContinueWith(task => Console.WriteLine("error"), TaskContinuationOptions.OnlyOnFaulted);
            t.ContinueWith(task => Console.WriteLine("cancel"), TaskContinuationOptions.OnlyOnCanceled);

            //cts.Cancel();

            Console.ReadLine();
        }

        static void Example1()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<int> t = new Task<int>(() => Sum(cts.Token, 100000), cts.Token);

            t.Start();

            Thread.Sleep(10);

            cts.Cancel();

            try
            {
                Console.WriteLine(t.Result);
            }
            catch (AggregateException x)
            {
                x.Handle(e => e is OperationCanceledException);

                Console.WriteLine("Sum was canceled");
            }

            Console.ReadLine();
        }

        static int Sum(CancellationToken ct, int n)
        {
            int sum = 0;
            for (; n > 0; n--)
            {
                ct.ThrowIfCancellationRequested();

                checked
                {
                    sum += n;
                }
            }

            return sum;
        }
        static int Sum(int n)
        {
            int sum = 0;
            for (; n > 0; n--)
            {
                checked
                {
                    sum += n;
                }
            }

            return sum;
        }

        static HttpClient http = new HttpClient();

        static async Task ExampleMultyTask()
        {
            int pages = 100;
            while (pages-- > 0)
            {
                var tasks = new List<Task>();

                int count = 10;

                while (count-- > 0)
                {
                    var task = Task.Run(async () =>
                    {
                        try
                        {
                            var item = await http.GetStringAsync("https://ya.ru/");
                            //парсинг item
                            // асинхронная запись в бд через, например await context.SaveChangesAsync(); 
                            Console.Write("записано" + Environment.NewLine);
                        }
                        catch
                        {
                            //
                            Console.Write("ошибка" + Environment.NewLine);
                        }
                    });

                    tasks.Add(task);
                }

                await Task.WhenAll(tasks);

                //переход на след страницу             
                Console.Write("следующая страница" + Environment.NewLine);

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPL
{
    class Program
    {
        static void Main(string[] args)
        {
            E2("J://","*a*",SearchOption.TopDirectoryOnly);

            Console.ReadLine();
        }

        static void E1()
        {
            Parallel.For(1, 10, i => Sum(i));
                        
            Console.WriteLine("# 1");

            Parallel.ForEach(Enumerable.Range(1, 10), i => Sum(i));

            Console.WriteLine("# 2");

            Parallel.Invoke(
                () => Sum(10),
                () => Sum(11),
                () => Sum(12),
                () => Sum(13)
                );

            Console.WriteLine("# 3");
        }

        static long E2(string path, string searchPattern, SearchOption searchOption)
        {
            var files = Directory.EnumerateFiles(path, searchPattern, searchOption);
            long masterTotal = 0;

            ParallelLoopResult result = Parallel.ForEach<string, long>(
                files,
                () =>
                {
                    return 0;
                },
                (file, loopState, index, taskLocalTotal) =>
                {
                    long fileLength = 0;
                    FileStream fs = null;
                    try
                    {
                        fs = File.OpenRead(file);
                        fileLength = fs.Length;
                    }
                    catch (IOException) { }                    
                    finally
                    {
                        fs?.Dispose();
                    }

                    return taskLocalTotal += fileLength;
                },
                taskLocalTotal =>
                {
                    Interlocked.Add(ref masterTotal, taskLocalTotal);
                });

            Console.WriteLine(masterTotal);

            return masterTotal;
        }

        static int Sum(int n)
        {
            int k = n;           

            int sum = 0;
            for (; n > 0; n--)
            {
                checked
                {
                    sum += n;
                }
            }

            Console.WriteLine("{0} {1,9}", k, sum);

            return sum;
        }
    }
}

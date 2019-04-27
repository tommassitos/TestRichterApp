using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Test123
{
    class Program
    {
        static void Main(string[] args)
        {
            g2();

            Console.ReadLine();
        }

        static void g2()
        {
            var arr = new int[1];

            //((ICollection<int>)arr).Add(2);

            ICollection<int> col = arr;

            Console.WriteLine(col.IsReadOnly);

            Console.WriteLine(string.Join(", ", arr));
        }

        static void g1()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    Process.GetProcessesByName("chrome")
                    .ToList().ForEach(p => p.Kill());

                    await Task.Delay(new TimeSpan(0, 0, 10));
                }
            });
        }
    }
}

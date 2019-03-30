using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferma
{
    class Program
    {
        static void Main(string[] args)
        {
            CalcFerma();

            Console.WriteLine("Complete");
            Console.ReadLine();
        }

      //  [Conditional("DEBUG")]
        static void CalcFerma()
        {
            for (long n = 3; n < 100; n++)
                for (long a = 1; a < 100; a++)
                    for (long b = 1; b < 100; b++)
                        for (long c = 1; c < 100; c++)
                        {
                            long A = (long)Math.Pow(a, n);
                            long B = (long)Math.Pow(b, n);
                            long C = (long)Math.Pow(c, n);
                            if (A + B == C)
                            {

                                Console.WriteLine("{0,9}^{3} + {1,9}^{3} = {2,9}^{3}", a, b, c, n);
                                Console.WriteLine("{0,9:d} + {1,9:d} = {2,9:d}", A, B, C);
                                return;
                            }
                        }
        }
    }
}

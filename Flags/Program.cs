using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flags
{
    [Flags]
    enum CustomFlags
    {
        f1 = 1,
        f2 = 1 << 1,
        f3 = 1 << 2,
        f4 = 1 << 3,
        f5 = f1 | f2 | f3 | f4
    }

    class Program
    {
        static void Main(string[] args)
        {
            var sb = new StringBuilder();

            sb.Append(Convert.ToString((int)CustomFlags.f4, 2));

            Console.Write(sb);
            Console.ReadLine();
        }
    }
}

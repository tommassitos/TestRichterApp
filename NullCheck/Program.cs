using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            Go();
            Console.ReadLine();
        }

        static void Go()
        {
            A a = new A();

            int? t = a?.t;

            Console.WriteLine(t?.ToString() ?? "null");
        }
    }

    class A
    {
        public int t;
    }
}

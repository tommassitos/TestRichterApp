using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            int length = 10;

            unsafe
            {
                int* arr = stackalloc int[length];

                for (int i = 0; i < length; i++)
                {
                    Console.WriteLine(arr[i]);
                }

            }

            Console.WriteLine();

            fixed (int* arr = new int[length])
            {
                for (int i = 0; i < length; i++)
                {
                    Console.WriteLine(arr[i]);
                }
            }

            Console.ReadLine();
        }
    }
}

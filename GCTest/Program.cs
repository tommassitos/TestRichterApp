using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Go();
        }

        unsafe public static void Go()
        {
            for (int x = 0; x < 1000; x++)
            {
                new object();
            }

            IntPtr originalMemoryAddress;

            byte[] bytes = new byte[1000];

            fixed (byte* pbytes = bytes)
            {
                originalMemoryAddress = (IntPtr)pbytes;
            }

            GC.Collect();

            fixed (byte* pbytes = bytes)
            {
                Console.WriteLine(originalMemoryAddress == (IntPtr)pbytes);
            }

            Console.ReadLine();
        }
    }
}

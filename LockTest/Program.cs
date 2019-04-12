using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockTest
{
    class Program
    {
        static object lockObj = new object();
        static void Main(string[] args)
        {
            try
            {
                throw new Exception("from try");
            }
            finally
            {
                Console.WriteLine("err1");
            }

            lock (lockObj)
            {
                throw new Exception("from lock");
            }

            Console.ReadLine();
        }
    }
}

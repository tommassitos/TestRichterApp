using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionTest
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("try");
                throw new Exception();
                //Environment.FailFast("");
            }
            catch
            {
                Console.WriteLine("catch");
            }
            finally
            {
                //throw new Exception();
                Console.WriteLine("finally");
            }

            Console.ReadLine();
        }
    }
}

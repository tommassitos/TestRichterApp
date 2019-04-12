using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwaitVoidTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Go();
            Console.ReadLine();
        }

        static async void Go()
        {
            await VoidReturn();

            Console.WriteLine("from Go");
        }

        static async Task VoidReturn() {

            await Task.Delay(1000);

            Console.WriteLine("from VoidReturn");
        }
        
    }
}

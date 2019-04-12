using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSync
{
    class Program
    {
        static void Main(string[] args)
        {
            //EventsAutoReset.Go();
            //EventsManualReset.Go();
            //SemaphoreTest.Go();
            MutexTest.Go();
            Console.ReadLine();
        }
    }
}

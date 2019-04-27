using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesChainTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SwitcjExample();
            Console.ReadLine();           
        }


        private static string SwitcjExample()
        {
            Action act = null;
            var someThing = "thing";

            switch (someThing)
            {
                case "some":
                    act += () =>
                    {
                        // кусок кода 1
                        Console.WriteLine("some" + Environment.NewLine);
                    };
                    break;
                case "thing":
                    act += () =>
                    {
                        // кусок кода 2
                        Console.WriteLine("thing" + Environment.NewLine);
                    };
                    break;
                default:
                    return "Default";
            }

            if (act != null)
            {
                act = (Action)Delegate.Combine(new Action(() => { someFunction(someThing); }), act);
                act();
            }

            return "";

        }

        private static void someFunction(string someThing)
        {
            Console.WriteLine("someFunction" + Environment.NewLine);
        }
    }
}

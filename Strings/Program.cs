using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strings
{
    class Program
    {
        static void Main(string[] args)
        {
            var s1 = string.Intern("s1");
            var s2 = string.Intern("s1");

            var s3 = "s1";
            var s4 = "s1";

            Console.WriteLine(ReferenceEquals(s1, s2));
            Console.WriteLine(ReferenceEquals(s3, s4));

            Console.WriteLine(string.Compare("Straße", "Strasse", StringComparison.Ordinal));
            Console.WriteLine(string.Compare("Straße", "Strasse", StringComparison.InvariantCulture));

            Console.ReadLine();
        }
    }
}

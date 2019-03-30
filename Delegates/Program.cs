using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<object, string> fA = new Func<object, string>(a => a.ToString());

            Func<string, object> fB = fA;

            Delegate<string, object> f1 = new Delegate<object, string>(a => a.ToString());
        }

        delegate TResult Delegate<in T, out TResult>(T arg);
    }
}

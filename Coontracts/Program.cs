#define CONTRACTS_FULL

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coontracts
{
    class Program
    {
        static void Main(string[] args)
        {
            new Ctest().Add("4");

            Console.Write("Complete");
            Console.ReadLine();
        }
    }

    class Ctest
    {
        List<string> c_list = new List<string> { "1", "2", "3" };
        decimal cost = 10;

        public void Add(string item)
        {
            Add(c_list, item, ref cost);
        }

        public void Add(List<string> list, string item, ref decimal cost)
        {
            Contract.Requires(item != null);
            Contract.Requires(Contract.ForAll(list, i => i != item));

            Contract.Ensures(Contract.Exists(list, i=> i == item));
            Contract.Ensures(cost >= Contract.OldValue(cost));
            Contract.EnsuresOnThrow<IOException>(cost == Contract.OldValue(cost));

            c_list.Add(item);
            cost += -110;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(cost >= 0);
        }
    }
}

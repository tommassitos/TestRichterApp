using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DomainTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string exeAssembly = Assembly.GetEntryAssembly().FullName;

            AppDomain ad2 = AppDomain.CreateDomain("AD 2", null, null);

            MarshalByRefType mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly, typeof(MarshalByRefType).FullName);

            mbrt.ThrowException();

            Console.ReadLine();

            AppDomain.Unload(ad2);
        }
    }

    public sealed class MarshalByRefType : MarshalByRefObject
    {
        public MarshalByRefType()
        {
            Console.WriteLine("ctor MarshalByRefType {0} {1}",
                this.GetType().ToString(),
                Thread.GetDomain().FriendlyName);
        }

        public void ThrowException()
        {
           /* ThreadPool.QueueUserWorkItem(state =>
            {
                throw new Exception("test");
            });            */
        }
    }
}
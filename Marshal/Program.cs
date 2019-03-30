using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Marshal
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain callingThreadDomain = Thread.GetDomain();

            string callingThreadDomainName = callingThreadDomain.FriendlyName;

            Console.WriteLine("callingThreadDomainName {0}", callingThreadDomainName);

            string exeAssembly = Assembly.GetEntryAssembly().FullName;

            Console.WriteLine("exeAssembly {0}", exeAssembly);

            AppDomain ad2 = null;

            ad2 = AppDomain.CreateDomain("AD 2", null, null);

            MarshalByRefType mbrt = null;

            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly, typeof(MarshalByRefType).FullName);

            Console.WriteLine("mbrt.GetType() {0}", mbrt.GetType());

            Console.WriteLine("RemotingServices.IsTransparentProxy(mbrt) {0}", RemotingServices.IsTransparentProxy(mbrt));

            mbrt.SomeMethod();

            AppDomain.Unload(ad2);

            try
            {
                mbrt.SomeMethod();
            }
            catch (AppDomainUnloadedException)
            {
                Console.WriteLine("AppDomainUnloadedException");
            }

            ad2 = AppDomain.CreateDomain("AD 2", null, null);

            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly, typeof(MarshalByRefType).FullName);

            MarshalByValType mbvt = mbrt.MethodWithReturn();

            Console.WriteLine("RemotingServices.IsTransparentProxy(mbvt) {0}", RemotingServices.IsTransparentProxy(mbvt));

            Console.WriteLine("mbvt {0}", mbvt);

            AppDomain.Unload(ad2);

            try
            {
                Console.WriteLine("mbvt {0}", mbvt);
            }
            catch (AppDomainUnloadedException)
            {
                Console.WriteLine("AppDomainUnloadedException");
            }

            ad2 = AppDomain.CreateDomain("AD 2", null, null);

            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly, typeof(MarshalByRefType).FullName);

            try
            {
                NonMarshalableType nmt = mbrt.MethodArgAndReturn(callingThreadDomainName);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
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

        public void SomeMethod()
        {
            Console.WriteLine("SomeMethod {0}", Thread.GetDomain().FriendlyName);
        }

        public MarshalByValType MethodWithReturn()
        {
            Console.WriteLine("MethodWithReturn {0}", Thread.GetDomain().FriendlyName);
            return new MarshalByValType();
        }

        public NonMarshalableType MethodArgAndReturn(string callingDomainName)
        {
            Console.WriteLine("MethodArgAndReturn {0} {1}", callingDomainName,
                Thread.GetDomain().FriendlyName);

            return new NonMarshalableType();
        }
    }

    [Serializable]
    public sealed class MarshalByValType
    {
        DateTime creationTime = DateTime.Now;

        public MarshalByValType()
        {
            Console.WriteLine("ctor MarshalByValType {0} {1} {2:D}",
                this.GetType().ToString(),
                Thread.GetDomain().FriendlyName,
                creationTime);
        }

        public override string ToString()
        {
            return creationTime.ToLongDateString();
        }
    }

    public sealed class NonMarshalableType
    {
        public NonMarshalableType()
        {
            Console.WriteLine("ctor NonMarshalableType {0}", Thread.GetDomain().FriendlyName);
        }
    }
}

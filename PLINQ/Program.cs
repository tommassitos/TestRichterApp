using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            ObsoleteMehtods(Assembly.GetAssembly(typeof(string)));
            Console.ReadLine();
        }
        static void ObsoleteMehtods(Assembly assembly)
        {
            var query =
                from type in assembly.GetExportedTypes().AsParallel()
                from method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)

                let obsoleteAttrType = typeof(ObsoleteAttribute)

                where Attribute.IsDefined(method, obsoleteAttrType)

                orderby type.FullName

                let obsoleteAttrObj = (ObsoleteAttribute)Attribute.GetCustomAttribute(method, obsoleteAttrType)

                select string.Format("Type {0}\nMethod {1}\nMessege {2}", type.FullName, method, obsoleteAttrObj.Message);
            /*
            foreach (var result in query)
                Console.WriteLine(result);
                */
            query.ForAll(Console.WriteLine);
        }

        static void ObsoleteMehtods2(Assembly assembly)
        {
            var query =
                from type in assembly.GetExportedTypes().AsParallel()
                from method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)

                let obsoleteAttrType = typeof(ObsoleteAttribute)

                where Attribute.IsDefined(method, obsoleteAttrType)

                orderby type.FullName

                let obsoleteAttrObj = CustomAttributeData.GetCustomAttributes(method).Where(a => a.AttributeType == obsoleteAttrType)

                select string.Format("Type {0}\nMethod {1}", type.FullName, method);

            foreach (var result in query)
                Console.WriteLine(result);
        }
    }
}

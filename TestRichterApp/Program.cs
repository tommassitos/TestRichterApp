#define TEST
//#define TEST2

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestRichterApp
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    [Conditional("TEST")]
    public sealed class CondAttribute : Attribute
    {
        private string tostrdata;

        public int na;
        public int nb;
        public CondAttribute(int a, int b)
        {
            tostrdata = a + "_" + b;
        }

        public override string ToString()
        {
            return tostrdata;
        }
    }

    [type: Cond(11, 23, na = 32, nb = 224)]
    [type: Cond(1, 2, na = 3, nb = 4)]
    class Program
    {
        static void Main(string[] args)
        {
            foreach(var attr in typeof(Program).GetCustomAttributes<Attribute>())
            {
                Console.WriteLine("{0,9} {1,9}", attr.GetType().ToString(), attr.ToString());                
            }

            Console.WriteLine("\nCustomAttributeData\n");

            foreach (var attr in CustomAttributeData.GetCustomAttributes(typeof(Program)))
            {
                Console.WriteLine("{0,9}", attr.AttributeType.ToString());

                foreach (var arg in attr.ConstructorArguments)
                {
                    Console.WriteLine("{0,9} {1,9}", arg.ArgumentType, arg.Value);
                }

                foreach (var arg in attr.NamedArguments)
                {
                    Console.WriteLine("{0,9} {1,9} {2,9}", arg.TypedValue.ArgumentType, arg.TypedValue.Value, arg.MemberInfo.Name);
                }

            }

            //Console.WriteLine("{0}", Attribute.IsDefined(typeof(Program), typeof(CondAttribute)));
            Console.ReadLine();
        }
    }
}

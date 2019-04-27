using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EqualsTest
{
    class Program
    {
        static void Main(string[] args)
        {
           // B.f();
           // Console.ReadLine(); return;


            var r = EqualityComparer<A>.Default.Equals(new A("qwe"), new A("qwe"));
            Console.WriteLine(r);

            r = new A("qwe").Equals(new A("qwe"));
            Console.WriteLine(r);

            r = new A("qwe") == new A("qwe");
            Console.WriteLine(r);

            Console.ReadLine();
        }
    }

    public class A
    {
        string name { get; }
        public A(string Name)
        {
            name = Name;
        }
        public override bool Equals(object obj)
        {
            return obj is A a
                && StringComparer.Ordinal.Equals(name, a.name);
        }

        public override int GetHashCode()
        {
            return StringComparer.Ordinal.GetHashCode(name);
        }
    }

    class B
    {
        public static void f()
        {
            var str = JsonConvert.SerializeObject(new Dictionary<string, string> {
               { "key","value"},
                { "key2","value2"}});

            File.WriteAllText("config.json", str);

            str = File.ReadAllText("config.json");

            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
        }
    }
}

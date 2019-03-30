using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NullabeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            NullableTest<int> i = null;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NullableTest<T> where T : struct
    {
        private bool hasValue;
        internal T value;

        public NullableTest(T value)
        {
            this.hasValue = true;
            this.value = value;
        }

        public static implicit operator NullableTest(object value)
        {
            if(value == null)
            {
                return new NullableTest<T>();
            }

            throw new Exception();
        }

        public static implicit operator NullableTest<T>(T value)
        {
            return new NullableTest<T>(value);
        }

        public static explicit operator T(NullableTest<T> value)
        {
            return value.value;
        }
    }
}

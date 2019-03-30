using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StateMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            MyMethodAsync(2);
            Console.ReadLine();
        }
        [DebuggerStepThrough, AsyncStateMachine(typeof(StateMachine))]
        private static Task<string> MyMethodAsync(int argument)
        {
            StateMachine stateMachine = new StateMachine()
            {
                m_builder = AsyncTaskMethodBuilder<string>.Create(),
                m_state = 1,
                m_argunet = argument
            };

            stateMachine.m_builder.Start(ref stateMachine);
            return stateMachine.m_builder.Task;
        }
        [CompilerGenerated, StructLayout(LayoutKind.Auto)]
        private struct StateMachine : IAsyncStateMachine
        {
            public AsyncTaskMethodBuilder<string> m_builder;
            public int m_state;

            public int m_argunet, m_local, m_x;
            public Type1 m_resultType1;
            public Type2 m_resultType2;

            TaskAwaiter<Type1> m_awaiterType1;
            TaskAwaiter<Type2> m_awaiterType2;

            void IAsyncStateMachine.MoveNext()
            {
                string result = null;

                try
                {
                    bool executeFinally = true;

                    if (m_state == 1)
                    {
                        m_local = m_argunet;
                    }

                    try
                    {
                        TaskAwaiter<Type1> awaiterType1;
                        TaskAwaiter<Type2> awaiterType2;

                        switch (m_state)
                        {
                            case 1:
                                {

                                    awaiterType1 = Method1Async().GetAwaiter();
                                    if (!awaiterType1.IsCompleted)
                                    {
                                        m_state = 0;
                                        m_awaiterType1 = awaiterType1;
                                        m_builder.AwaitUnsafeOnCompleted(ref awaiterType1, ref this);

                                        executeFinally = false;

                                        return;
                                    }
                                }
                                break;
                            case 0:
                                {
                                    awaiterType1 = m_awaiterType1;
                                }
                                break;
                            case 2:
                                {
                                    awaiterType2 = m_awaiterType2;
                                    goto ForLoopEpilog;
                                }
                        }

                        m_resultType1 = awaiterType1.GetResult();

                        Console.WriteLine(m_resultType1);

                        ForLoopPrologue:
                        m_x = 0;
                        goto ForLoopBody;

                        ForLoopEpilog:
                        m_resultType2 = awaiterType2.GetResult();
                        Console.WriteLine(m_resultType2);
                        m_x++;

                        ForLoopBody:
                        if (m_x < 3)
                        {
                            awaiterType2 = Method2Async(m_x).GetAwaiter();
                            if (!awaiterType2.IsCompleted)
                            {
                                m_state = 2;
                                m_awaiterType2 = awaiterType2;
                                m_builder.AwaitUnsafeOnCompleted(ref awaiterType2, ref this);
                                executeFinally = false;

                                return;
                            }

                            goto ForLoopEpilog;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Catch");
                    }
                    finally
                    {
                        if (executeFinally)
                        {
                            Console.WriteLine("Finally");
                        }
                    }
                    result = "Done";
                }
                catch (Exception exception)
                {
                    m_builder.SetException(exception);
                    return;
                }
                m_builder.SetResult(result);
            }

            private Task<Type1> Method1Async()
            {
                var t = new Task<Type1>(() => { return new Type1(); }, CancellationToken.None);
                t.Start();
                return t;
            }

            private Task<Type2> Method2Async(int i)
            {
                var t = new Task<Type2>(() => { return new Type2(i); }, CancellationToken.None);
                t.Start();
                return t;
            }

            [DebuggerHidden]
            public void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                m_builder.SetStateMachine(stateMachine);
            }
        }
    }

    public class Type2
    {
        readonly int i;
        public Type2(int i)
        {
            this.i = i;
        }
        public override string ToString()
        {
            return string.Format("Type2 : {0}", i);
        }
    }

    public class Type1
    {
        public override string ToString()
        {
            return "Type1";
        }
    }
}

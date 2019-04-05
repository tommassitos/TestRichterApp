using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StateMachine
{
    public sealed class EventAwaiter<TeventArgs> : INotifyCompletion
    {       

        ConcurrentQueue<TeventArgs> m_events = new ConcurrentQueue<TeventArgs>();
        Action m_continuation;

        public EventAwaiter<TeventArgs> GetAwaiter() => this;
        public bool IsCompleted => m_events.Any();

        public void OnCompleted(Action continuation)
        {
            Volatile.Write(ref m_continuation, continuation);
        }

        public TeventArgs GetResult()
        {
            TeventArgs e;
            m_events.TryDequeue(out e);
            return e;
        }

        public void EventRaised(object sender, TeventArgs eventArgs)
        {
            m_events.Enqueue(eventArgs);

            Action continuation = Interlocked.Exchange(ref m_continuation, null);

            continuation?.Invoke();
        }
    }

    public class TestAwaiter
    {
        static async void ShowExceptions()
        {
            var eventAwaiter = new EventAwaiter<FirstChanceExceptionEventArgs>();

            AppDomain.CurrentDomain.FirstChanceException += eventAwaiter.EventRaised;

            while (true)
            {
                Console.WriteLine("AppDomain exception: {0}", (await eventAwaiter).Exception.GetType());
            }
        }

        public static void Go()
        {
            ShowExceptions();

            for(int i=0; i<3; i++)
            {
                try
                {
                    switch (i)
                    {
                        case 0: throw new InvalidOperationException();
                        case 1: throw new ObjectDisposedException("");
                        case 2: throw new ArgumentOutOfRangeException();
                    }
                }
                catch
                {

                }
            }
        }
    }
}

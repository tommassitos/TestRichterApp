using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    public class AsyncDownoader
    {
        static async Task<string> AwaitWebClient(Uri uri)
        {
            var wc = new WebClient();

            var tcs = new TaskCompletionSource<string>();

            wc.DownloadStringCompleted += (s, e) =>
            {
                if (e.Cancelled)
                {
                    tcs.SetCanceled();
                }
                else
                {
                    if (e.Error != null)
                    {
                        tcs.SetException(e.Error);
                    }
                    else
                    {
                        tcs.SetResult(e.Result);
                    }
                }
            };

            wc.DownloadStringAsync(uri);

            string result = await tcs.Task;

            return result;
        }
    }
}

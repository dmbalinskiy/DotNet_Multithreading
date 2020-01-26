using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part1
{
    class TaskWaiting_1 : ITaskExample
    {
        public int Id => 5;
        public string Description => "Task waiting example - WaitHandle";

        public void Execute()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            Task.Factory.StartNew(() =>
                {
                    
                    Console.WriteLine("Press any key within 5 second time interval");
                    bool canceled = token.WaitHandle.WaitOne(5000);
                    Console.WriteLine(canceled ? "Disarmed" : "Boom");

                }, token);

            Console.ReadKey();
            cts.Cancel();
        }
    }
}

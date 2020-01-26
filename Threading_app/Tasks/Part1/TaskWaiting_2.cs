using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part1
{
    class TaskWaiting_2 : ITaskExample
    {
        public int Id => 6;
        public string Description => "Task waiting example - WaitAll/WaitAny";

        public void Execute()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t1 = new Task(() =>
            {
                Console.WriteLine("I take 5 seconds");
                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }

                Console.WriteLine("task1  - I'm done");
            }, token);
            t1.Start();

            var t2 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("task2  - I'm done");
            }, token);
            
            Task.WaitAll(new [] {t1, t2}, 4000, token);
            Console.WriteLine($"task1 status is {t1.Status}");
            Console.WriteLine($"task2 status is {t2.Status}");
        }
    }
}

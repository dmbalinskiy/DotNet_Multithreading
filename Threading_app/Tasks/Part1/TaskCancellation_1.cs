using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part1
{
    class TaskCancellation_1 : ITaskExample
    {
        public int Id => 3;
        public string Description => "Task cancellation";

        public void Execute()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            //all registered items will be called!
            token.Register(() =>
            {
                Console.WriteLine("Cancellation is requested_1!");
            });
            token.Register(() =>
            {
                Console.WriteLine("Cancellation is requested_2!");
            });
            token.Register(() =>
            {
                Console.WriteLine("Cancellation is requested_3!");
            });


            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    //if (token.IsCancellationRequested)
                    //{
                    //    //1st way (preferable) - throw an exception - token should be used as a parameter!
                    //    throw  new OperationCanceledException(token);
                    //
                    //    //2nd way - stop execution in other way
                    //    break;
                    //}
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                }
            }, token);
            t.Start();

            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait handle released, cancellation was requested");
            });

            Console.ReadKey();
            cts.Cancel();
        }
    }
}

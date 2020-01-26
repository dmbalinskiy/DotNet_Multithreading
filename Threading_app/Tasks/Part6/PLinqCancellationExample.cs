using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part6
{
    class PLinqCancellationExample : ITaskExample
    {
        public int Id => 30;
        public string Description => "Example of parallel Linq cancellation";
        public void Execute()
        {
            var cts = new CancellationTokenSource();

            var items = ParallelEnumerable.Range(1, 20);
            var results = items.WithCancellation(cts.Token).Select(i =>
            {
                double result = Math.Log10(i);
                Console.WriteLine($"i = {i}, tid = {Task.CurrentId}");
                //if(i > 10)
                //{
                //    throw new InvalidOperationException();
                //}
                return result;
            });

            try
            {
                foreach(var c in results)
                {
                    if(c > 1)
                    {
                        cts.Cancel();
                    }
                    Console.WriteLine($"result = {c}");
                }
            }
            catch(AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                    return true;
                });
            }
            catch(OperationCanceledException e)
            {
                Console.WriteLine("Canceled");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part5
{
    class ParallelStopAndBreak : ITaskExample
    {
        public int Id => 26;
        public string Description => "An example of parallel loop stopping and breaking";
        public void Execute()
        {
            try
            {
                Demo();
            }
            catch(AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e.Message);
                    return true;
                });
            }
            catch(OperationCanceledException e)
            {
                Console.WriteLine("Operation was canceled!");
            }
        }

        private void Demo()
        {
            var cts = new CancellationTokenSource();
            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = cts.Token;

            ParallelLoopResult result = 
            Parallel.For(0, 20, po, (int x, ParallelLoopState state) =>
            {
                Console.WriteLine($"{x} \t [{Task.CurrentId}]\t");
                cts.Cancel();

                if (x == 10)
                {
                    //throw new Exception();
                    //state.Stop();
                    //state.Break();
                }
            });

            Console.WriteLine($"The loop was {(result.IsCompleted ? "" : "in")}completed");
            if (result.LowestBreakIteration.HasValue)
                Console.WriteLine($"Lowest break iteration is {result.LowestBreakIteration}");
        }
    }
}

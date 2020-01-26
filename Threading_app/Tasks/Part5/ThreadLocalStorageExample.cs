using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part5
{
    class ThreadLocalStorageExample : ITaskExample
    {
        public int Id => 27;
        public string Description => "Thread local storage usage example";
        public void Execute()
        {
            Console.WriteLine("Counting from 1 to 10001 in parallel...");
            int sum = 0;

            Parallel.For(1, 101, 
                
                //local storage initializing
                () => 0,

                //iteration function - per each thread
                //x - current iteration value
                //state - parallel loop state
                //tlsValue - current value of the tls
                (x, state, tlsValue) => 
                {
                    Console.WriteLine($"Task {Task.CurrentId} has sum {tlsValue} + {x} = {tlsValue + x}");
                    tlsValue += x;
                    return tlsValue;
                },

                //together
                partialSum =>
                {
                    Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
                    Interlocked.Add(ref sum, partialSum);
                });

            Console.WriteLine($"Result is {sum}");
        }
    }
}

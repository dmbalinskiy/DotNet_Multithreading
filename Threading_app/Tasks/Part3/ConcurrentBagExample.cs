using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Threading_app.Tasks.Part3
{
    class ConcurrentBagExample : ITaskExample
    {
        public int Id => 17;
        public string Description => "Example of ConcurrentBag<T> usage";
        public void Execute()
        {
            //stack LIFO
            //queue FIFO
            //bag -> no ordering 

            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();
            for(int i = 0; i < 10; i++)
            {
                var i1 = i;
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    bag.Add(i1);
                    Console.WriteLine($"Task {Task.CurrentId} has added {i1}");

                    int result;
                    if(bag.TryPeek(out result))
                    {
                        Console.WriteLine($"Task {Task.CurrentId} has peeked the value {result} ");
                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());

            int last;
            if(bag.TryTake(out last))
            {
                Console.WriteLine($"I got {last}");
            }
        }
    }
}

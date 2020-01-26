using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part4
{
    class SemaphoreExample : ITaskExample
    {
        public int Id => 24;
        public string Description => "Example of SemaphoreSlim usage";

        SemaphoreSlim _sm = new SemaphoreSlim(2, 10);
        public void Execute()
        {
            for(int i = 0; i < 20; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}");
                    _sm.Wait();
                    Console.WriteLine($"Task {Task.CurrentId} is processed");
                });
            }

            while(_sm.CurrentCount <= 2)
            {
                Console.WriteLine($"Semaphore count: {_sm.CurrentCount}");
                Console.ReadKey();
                _sm.Release(2);
            }
        }
    }
}

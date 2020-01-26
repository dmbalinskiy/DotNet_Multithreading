using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part4
{
    class CountdownEventExample : ITaskExample
    {
        public int Id => 22;
        public string Description => "Example of CountdowEvent usage";

        private static int _taskCnt = 5;
        CountdownEvent _evt = new CountdownEvent(_taskCnt);
        private Random random = new Random();

        public void Execute()
        {
            for(int i = 0; i < _taskCnt; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}");
                    Thread.Sleep(random.Next(2000, 4000));
                    _evt.Signal();
                    Console.WriteLine($"Exiting task {Task.CurrentId}");
                });
            }

            var lastTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Waiting for other tasks to complete in {Task.CurrentId}");
                _evt.Wait();
                Console.WriteLine("Task is completed");
            });

            lastTask.Wait();
        }
    }
}

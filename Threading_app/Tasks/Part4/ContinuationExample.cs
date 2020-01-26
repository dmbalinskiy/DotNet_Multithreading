using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part4
{
    class ContinuationExample : ITaskExample
    {
        public int Id => 19;
        public string Description => "Example of task continuations";


        public void Execute()
        {
            var task = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Boiling water!");
                });

            var task2 = task.ContinueWith(t =>
            {
                Console.WriteLine($"Completed task {t.Id}, pour water into cup");
            });

            task2.Wait();

            var t3 = Task.Factory.StartNew(() => "Task 1");
            var t4 = Task.Factory.StartNew(() => "Task 2");

            var t5 = Task.Factory.ContinueWhenAll(new[] { t3, t4 },
                (tasks) =>
                {
                    Console.WriteLine("Tasks completed!");
                    foreach(var t in tasks)
                    {
                        Console.WriteLine($"Task {t.Id} - {t.Result}");
                    }
                    Console.WriteLine("Tasks done!");
                });
        }
    }
}

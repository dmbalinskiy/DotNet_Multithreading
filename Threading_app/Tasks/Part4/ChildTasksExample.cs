using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part4
{
    class ChildTasksExample : ITaskExample
    {
        public int Id => 20;
        public string Description => "Parent and child tasks example";
        public void Execute()
        {
            var parent = new Task(
                () =>
                {
                    Console.WriteLine("Parent task starting...");

                    //detached task - no continuation enum
                    var child = new Task(() =>
                    {
                        Console.WriteLine("Child task starting...");
                        Thread.Sleep(3000);
                        Console.WriteLine("Child taks finished");
                        throw new Exception();
                    }, TaskCreationOptions.AttachedToParent);

                    var completionHandler = child.ContinueWith(t =>
                    {
                        Console.WriteLine($"Completed: Task {t.Id} has a state {t.Status}");
                    }, TaskContinuationOptions.AttachedToParent | 
                        TaskContinuationOptions.OnlyOnRanToCompletion);

                    var failHandler = child.ContinueWith(t =>
                    {
                        Console.WriteLine($"Fault: Task {t.Id} has a state {t.Status}");
                    }, TaskContinuationOptions.AttachedToParent |
                        TaskContinuationOptions.OnlyOnFaulted);

                    child.Start();
                });

            var t2 = parent.ContinueWith(t => Console.WriteLine("Parent task finished"));

            try
            {
                parent.Start();
                t2.Wait();
            }
            catch(AggregateException ex)
            {
                ex.Handle(e => true);
            }
        }
    }
}

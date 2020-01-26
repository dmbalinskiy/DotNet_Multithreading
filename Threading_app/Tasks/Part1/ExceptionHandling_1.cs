using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part1
{
    class ExceptionHandling_1 : ITaskExample
    {
        public int Id => 7;
        public string Description => "Exception handling for tasks";
        public void Execute()
        {
            var t1 = Task.Factory.StartNew(() => throw new InvalidOperationException("Can't do this'"){Source = "t1"});
            var t2 = Task.Factory.StartNew(() => throw new AccessViolationException("Can't access this'"){ Source = "t2"});

            try
            {
                try
                {
                    Task.WaitAll(t1, t2);
                }
                catch (AggregateException e)
                {
                    //if this call will be entirely commented, nothing except foreach loop will be executed
                    e.Handle(ie =>
                    {
                        //if any of inner exceptions will not be handled here -> it will be rethrown
                        if (ie is InvalidOperationException)
                        {
                            Console.WriteLine("Invalid exception is handled");
                            return true;
                        }

                        //comment this line to rethrown an access violation exception
                        //else if (ie is AccessViolationException)
                        //{
                        //    Console.WriteLine("Access violation exception is handled");
                        //    return true;
                        //}
                        return false;
                    });
                    foreach (var ei in e.InnerExceptions)
                    {
                        Console.WriteLine($"exception of {e.GetType()} from {e.Source}");
                    }
                }
            }
            catch (AggregateException ex)
            {
                Console.WriteLine($"STILL UNCAUGHT exception of {ex.GetType()} from {ex.Source}");
                foreach (var ei in ex.InnerExceptions)
                {
                    Console.WriteLine($"exception of {ei.GetType()} from {ei.Source}");
                }
            }
            
            

        }
    }
}

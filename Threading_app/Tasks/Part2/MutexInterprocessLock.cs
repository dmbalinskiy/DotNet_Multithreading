using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part2
{
    class MutexInterprocessLock : ITaskExample
    {
        public int Id => 13;
        public string Description => "Example of multiprocess mutex usage";

        static Mutex s_mt;

        public void Execute()
        {
            Console.WriteLine();
            Console.WriteLine("This is an example of multiprocess mutex. " +
                "To use it, try to run 2 instances of this example in different windows");

            const string AppName = "app_Mutex";
            bool acquired = false;
            try
            {
                s_mt = Mutex.OpenExisting(AppName);
                Console.WriteLine($"Mutex {AppName} is already run");
            }
            catch(WaitHandleCannotBeOpenedException e)
            {
                Console.WriteLine("We can run the program");
                s_mt = new Mutex(false, AppName);
                acquired = true;
                s_mt.WaitOne();
            }

            Console.WriteLine("Press any key to release mutex");
            Console.ReadKey();

            if(acquired)
            {
                s_mt.ReleaseMutex();
            }

            
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part2
{
    class SpinLock_recursive : ITaskExample
    {
        public int Id => 11;
        public string Description => "Spin locking - lock recursion";

        public void Execute()
        {
            doLockRecursion(5);
        }

        void doLockRecursion(int x)
        {
            
            bool lockTaken = false;
            try
            {
                _sl.Enter(ref lockTaken);
            }
            catch(LockRecursionException ex)
            {
                Console.WriteLine($"An exception {ex} is thrown");
            }
            finally
            {
                if(lockTaken)
                {
                    Console.WriteLine($"lock is taken, x = {x}");
                    doLockRecursion(--x);
                    _sl.Exit();
                }
                else
                {
                    Console.WriteLine($"An error on lock taken, x = {x}");
                }
            }
        }

        private SpinLock _sl = new SpinLock(false);
    }
}

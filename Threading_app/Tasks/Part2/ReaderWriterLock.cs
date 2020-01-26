using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part2
{
    class ReaderWriterLock : ITaskExample
    {
        public int Id => 14;
        public string Description => "Reader-Writer lock example";
        private ReaderWriterLockSlim _slimRwl = new ReaderWriterLockSlim(
            LockRecursionPolicy.SupportsRecursion);

        public void Execute()
        {
            int x = 0;
            var tasks = new List<Task>();
            for(int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    //_slimRwl.EnterReadLock();
                    _slimRwl.EnterUpgradeableReadLock();

                    if(i % 2 == 0)
                    {
                        _slimRwl.EnterWriteLock();
                        x = 555;
                        _slimRwl.ExitWriteLock();
                    }

                    Console.WriteLine($"Enter a read lock, x = {x}");
                    Thread.Sleep(5000);

                    //_slimRwl.ExitReadLock();
                    _slimRwl.ExitUpgradeableReadLock();

                    Console.WriteLine($"Exit from a read lock, x = {x}");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch(AggregateException e)
            {
                e.Handle(ex => { Console.WriteLine(ex); return true; });
            }

            //Random rnd = new Random();
            //while(true)
            //{
            //    Console.ReadKey();
            //    _slimRwl.EnterWriteLock();
            //    Console.Write("Write lock acquired");
            //    int newValue = rnd.Next(10);
            //    x = newValue;
            //    Console.WriteLine($"Set x = {x}");
            //    _slimRwl.ExitWriteLock();
            //    Console.WriteLine("Write lock released");
            //}

        }
    }
}

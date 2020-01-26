using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part2
{
    class MutexExample : ITaskExample
    {
        public int Id => 12;
        public string Description => "Mutex example using bank account";
        public void Execute()
        {
            var tasks = new List<Task>();
            var ba1 = new BankAccount();
            Mutex mutex1 = new Mutex();

            var ba2 = new BankAccount();
            Mutex mutex2 = new Mutex();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool locked = mutex1.WaitOne();
                        try
                        {
                            ba1.Deposit(1);
                        }
                        finally
                        {
                            if (locked) mutex1.ReleaseMutex();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool locked = mutex2.WaitOne();
                        try
                        {
                            ba2.Deposit(1);
                        }
                        finally
                        {
                            if (locked) mutex2.ReleaseMutex();
                        }
                        
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = WaitHandle.WaitAll(new[] { mutex1, mutex2 });
                        try
                        {
                            ba1.Transfer(ba2, 1);
                        }
                        finally
                        {
                            if(haveLock)
                            {
                                mutex1.ReleaseMutex();
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                }));

                Task.WaitAll(tasks.ToArray());
                Console.WriteLine($"1st account - Iteration {i + 1}: {ba1}");
                Console.WriteLine($"2nd account - Iteration {i + 1}: {ba2}");
            }
        }

        class BankAccount
        {
            public int Balance { get; private set; }

            public void Deposit(int amount)
            {
                Balance += amount;
            }

            public void Withdraw(int amount)
            {
                Balance -= amount;
            }

            public void Transfer(BankAccount where, int amount)
            {
                where.Deposit(amount);
                Withdraw(amount);
            }

            public override string ToString()
            {
                return $"Balance is {Balance}";
            }
        }
    }
}

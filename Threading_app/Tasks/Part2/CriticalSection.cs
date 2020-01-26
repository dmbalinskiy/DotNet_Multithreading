using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part2
{
    class CriticalSection : ITaskExample
    {
        public int Id => 8;
        public string Description => "Critical section - bank account example";

        public void Execute()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                    {
                        for (int j = 0; j < 1000; j++)
                        {
                            ba.Deposit(100);
                        }
                    }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Withdraw(100);
                    }
                }));

                Task.WaitAll(tasks.ToArray());
                Console.WriteLine($"Iteration {i + 1}: {ba}");
            }
        }

        class BankAccount
        {
            public object padlock = new object();
            public int Balance { get; private set; }

            public void Deposit(int amount)
            {
                lock (padlock)
                {
                    Balance += amount;
                }
            }

            public void Withdraw(int amount)
            {
                lock (padlock)
                {
                    Balance -= amount;   
                }
            }

            public override string ToString()
            {
                return $"Balance is {Balance}";
            }
        }

    }
}

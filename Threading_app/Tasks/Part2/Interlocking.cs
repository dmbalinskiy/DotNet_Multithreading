using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part2
{
    class Interlocking : ITaskExample
    {
        public int Id => 9;
        public string Description => "Interlocked example - bank account example";

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
            public int Balance => _balance;
            private int _balance;

            public void Deposit(int amount)
            {
                Interlocked.Add(ref _balance, amount);
            }

            public void Withdraw(int amount)
            {
                Interlocked.Add(ref _balance, -amount);
            }

            public override string ToString()
            {
                return $"Balance is {Balance}";
            }
        }
    }
}

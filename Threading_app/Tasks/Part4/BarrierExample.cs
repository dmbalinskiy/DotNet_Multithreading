using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part4
{
    class BarrierExample : ITaskExample
    {
        public int Id => 21;
        public string Description => "Example of Barrier synchronization";

        Barrier barrier = new Barrier(2, 
            b => 
            {
                Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished by {b.ParticipantsRemaining}");
            });

        private void Water()
        {
            Console.WriteLine("Putting the kettle on (takes a bit longer)");
            Thread.Sleep(2000);
            barrier.SignalAndWait();

            Console.WriteLine("Pouring water into cup");
            barrier.SignalAndWait();

            Console.WriteLine("Putting the kettle away");
            
        }

        private void Cup()
        {
            Console.WriteLine("Finding the nicest cup th tea");
            barrier.SignalAndWait();

            Console.WriteLine("Adding tea");
            Thread.Sleep(1000);
            barrier.SignalAndWait();

            Console.WriteLine("Adding sugar");
            barrier.RemoveParticipant();
            barrier.SignalAndWait();

            Console.WriteLine("Mixing it up...");
            Thread.Sleep(1000);
        }

        public void Execute()
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);
            var tea = Task.Factory.ContinueWhenAll(new [] { water, cup}, 
                tasks =>
                {
                    Console.WriteLine("Enjoy your cup of tea");
                });

            tea.Wait();
        }
    }
}

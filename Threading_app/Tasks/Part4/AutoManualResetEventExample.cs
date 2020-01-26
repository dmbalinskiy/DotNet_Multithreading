using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part4
{
    class AutoManualResetEventExample : ITaskExample
    {
        public int Id => 23;
        public string Description => "Auto- and ManualReset events usage";
        
        public void Execute()
        {
            Console.WriteLine("\nManual reset event example\n");

            var evt = new ManualResetEventSlim();
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water...");
                Thread.Sleep(2000);
                evt.Set();
            });
            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water");
                evt.Wait();
                Console.WriteLine("Here is tea");
            });

            makeTea.Wait();

            Console.WriteLine("\nAuto reset event example\n");
            var autoEvt = new AutoResetEvent(false);

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water...");
                Thread.Sleep(2000);
                autoEvt.Set();
            });
            var makeTeaAuto = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water...");
                autoEvt.WaitOne();
                Console.WriteLine("Almost done...");

                bool ok = autoEvt.WaitOne(1000);
                if(ok)
                {
                    Console.WriteLine("Enjoy the tea");
                }
                {
                    Console.WriteLine("No tea :(");
                }
            });

            makeTeaAuto.Wait();
        }
    }
}

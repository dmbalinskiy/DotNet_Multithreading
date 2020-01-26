using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part5
{
    class PartitioningExample : ITaskExample
    {
        public int Id => 28;
        public string Description => "Example of partitioning pallel usage";
        public void Execute()
        {
            
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            Console.WriteLine("No-chunked - start...");
            sw.Start();
            SquareEachValue();
            sw.Stop();
            Console.WriteLine($"No-chunked - finish on {sw.ElapsedMilliseconds}");
            sw.Reset();
            Console.WriteLine();
            Console.WriteLine("Chunked - start...");
            sw.Start();
            SquareEachValueChunked();
            sw.Stop();
            Console.WriteLine($"Chunked - finish on {sw.ElapsedMilliseconds}");
        }


        void SquareEachValue()
        {
            const int count = 10000000;
            var values = Enumerable.Range(0, count);
            var results = new double[count];
            Parallel.ForEach(values,
                x => { results[x] = Math.Pow(x, 2); });
        }

        void SquareEachValueChunked()
        {
            const int count = 10000000;
            var values = Enumerable.Range(0, count);
            var results = new double[count];

            var part = Partitioner.Create(0, count, 100000);

            Parallel.ForEach(part,
                range =>
                {
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        results[i] = Math.Pow(i, 2);
                    }
                });
        }



    }
}

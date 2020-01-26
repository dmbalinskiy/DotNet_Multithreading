using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part5
{
    class ParallelLoopExample : ITaskExample
    {
        public int Id => 25;
        public string Description => "Example of Parallel Invoke/For/Foreach";
        public void Execute()
        {
            Console.WriteLine("Parallel.Invoke example");
            Console.WriteLine();

            var a = new Action(() => Console.WriteLine($"First task id: {Task.CurrentId}"));
            var b = new Action(() => Console.WriteLine($"Second task id: {Task.CurrentId}"));
            var c = new Action(() => Console.WriteLine($"Third task id: {Task.CurrentId}"));
            Parallel.Invoke(a, b, c);

            Console.WriteLine();
            Console.WriteLine("Parallel.For example");
            Console.WriteLine();

            Parallel.For(1, 11, (i) =>
            {
                Console.WriteLine($"{i * i}\t");
            });


            Console.WriteLine();
            Console.WriteLine("Parallel.ForEach example");
            Console.WriteLine();

            string[] words = { "oh", "what", "a", "night" };
            Parallel.ForEach(words, (str) =>
            {
                Console.WriteLine($"{str} has length {str.Length} (task {Task.CurrentId})");
            });

            Parallel.ForEach(Range(1, 20, 3), Console.WriteLine);
        }

        private static IEnumerable<int> Range(int start, int end, int step)
        {
            for(int i = 0; i < end; i+= step)
            {
                yield return i;
            }
        }
    }
}

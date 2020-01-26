using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part6
{
    class AsParallelAndParallelQueryExample : ITaskExample
    {
        public int Id => 29;
        public string Description => "Example of AsParallel and ParallelLibrary usage";
        public void Execute()
        {
            const int count = 100;
            var items = Enumerable.Range(1, count).ToArray();
            var result = new int[count];

            items.AsParallel().ForAll(x =>
            {
                int newValue = x * x * x;
                Console.WriteLine($"Cube of {x} is {newValue} from {Task.CurrentId}");
                result[x - 1] = newValue;
            });

            //Console.WriteLine();
            //Console.WriteLine();
            //foreach(var i in result)
            //{
            //    Console.WriteLine($"{i}");
            //}
            //Console.WriteLine();

            var cubes = items.AsParallel()
                //.AsOrdered()
                .Select(
                x =>
                {
                    int cube = x*x*x;
                    Console.WriteLine($"Cube of {x} is {cube} from {Task.CurrentId}");
                    return cube;
                });

            Console.WriteLine();
            Console.WriteLine();
            foreach(var i in cubes)
            {
                Console.Write($"{i} \t");
            }
            Console.WriteLine();
        }
    }
}

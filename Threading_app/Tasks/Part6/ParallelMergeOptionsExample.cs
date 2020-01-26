using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part6
{
    class ParallelMergeOptionsExample : ITaskExample
    {
        public int Id => 31;
        public string Description => "Example of MergeOptions usage in PLinq";
        public void Execute()
        {
            var numbers = Enumerable.Range(1, 50).ToArray();
            var results = numbers.AsParallel()
                .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                .Select(x =>
                {
                    var result = Math.Log10(x);
                    Console.WriteLine($"Produced {result} from {Task.CurrentId} \t");
                    return result;
                });

            foreach(var result in results)
            {
                Console.WriteLine($"Consumed {result}\t");
            }
        }
    }
}

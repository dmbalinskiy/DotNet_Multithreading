using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part6
{
    class CustomAggregationExample : ITaskExample
    {
        public int Id => 32;
        public string Description => "Example of custom aggregation in PLinq";
        public void Execute()
        {
            //var sum = Enumerable.Range(1, 1000).Sum();

            //var sum = Enumerable.Range(1, 1000).Aggregate(0, (i, acc) => i + acc);

            var sum = ParallelEnumerable.Range(1, 1000)
                .Aggregate(
                //seed
                0,

                //per-step fcn
                (acc, i) => acc + i,

                //per-task subtotals processing
                (total, subtotal) => 
                {
                    total += subtotal;
                    Console.WriteLine($"subtotal/total : {subtotal}/{total} from {Task.CurrentId}");
                    return total;
                },

                //post-processing
                i => i);
            Console.WriteLine($"Sum = {sum}");


        }
    }
}

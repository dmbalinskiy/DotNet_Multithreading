using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Threading_app.Tasks.Part3
{
    class ConcurrentStackExample : ITaskExample
    {
        public int Id => 16;
        public string Description => "Example of ConcurrentStack<T> usage";
        public void Execute()
        {
            var stack = new ConcurrentStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            Console.WriteLine("Values 1..4 are pushed into stack");

            int result;
            if (stack.TryPeek(out result))
                Console.WriteLine($"result is on top {result}");

            if (stack.TryPop(out result))
                Console.WriteLine($"Popped {result}");

            var items = new int[5];
            if(stack.TryPopRange(items, 0, 5) > 0)
            {
                var text = string.Join(", ", items.Select(i => i.ToString()));
                Console.WriteLine($"Popped these items: {text}");
            }
        }
    }
}

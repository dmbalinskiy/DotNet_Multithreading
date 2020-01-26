using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part1
{
    class TaskCreation_2 : ITaskExample
    {
        public int Id => 2;
        public string Description => "Task creation example - generic";

        public void Execute()
        {
            string text1 = "Testing", text2 = "this";
            var task1 = new Task<int>(TxLength, text1);
            task1.Start();

            var task2 = Task.Factory.StartNew(TxLength, text2);
            Console.WriteLine($"Len of text 1 is {task1.Result}");
            Console.WriteLine($"Len of text 2 is {task2.Result}");
        }

        private int TxLength(object o)
        {
            Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}...");
            return o.ToString().Length;
        }
    }
}

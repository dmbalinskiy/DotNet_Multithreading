using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading_app.Tasks.Part1
{
    class TaskCreation_1 : ITaskExample
    {
        public int Id => 1;
        public string Description => "Task creation example - non-generic";

        public void Execute()
        {   
            var t = new Task(Write, "=");
            t.Start();
            Task.Factory.StartNew(Write, "!");
        }

        private void Write(char c)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(c);
            }
        }

        private void Write(object o)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(o);
            }
        }
    }
}

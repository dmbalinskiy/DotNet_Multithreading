using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Threading_app.Tasks
{
    class TaskContainer 
    {
        private List<ITaskExample> _examples;
        public TaskContainer()
        {
            _examples =
                Assembly
                    .GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.GetInterface("Threading_app.Tasks.ITaskExample") != null)
                    .Select(t =>
                    {
                        return Activator.CreateInstance(t) as ITaskExample;
                    })
                    .OrderByDescending(e => e.Id)
                    .ToList();
        }

        public ITaskExample GetTaskExample()
        {
            Console.WriteLine("\n");
            int nmb = -1;
            ITaskExample result = null;
            while (nmb == -1)
            {
                Console.WriteLine("Type number of the example or \"exit\"");
                int i = 0;
                foreach (var taskExample in _examples)
                {
                    ++i;
                    Console.WriteLine($"{i}.\t{taskExample.Description}");
                }
                string input = Console.ReadLine().ToUpper();
                if (input == "EXIT")
                {
                    nmb = int.MaxValue;
                    break;
                }

                
                try
                {
                    nmb = Convert.ToInt32(input) - 1;
                    if (nmb < 0 || nmb >= _examples.Count)
                    {
                        Console.WriteLine("Number is out of range");
                        nmb = -1;
                    }
                    else
                    {
                        result = _examples[nmb];
                        break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Number can't be parsed");
                    nmb = -1;
                }
            }

            return result;


        }
    }
}

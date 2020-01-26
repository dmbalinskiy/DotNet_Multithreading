using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Threading_app.Tasks;
using Threading_app.Tasks.Part1;

namespace Threading_app
{
    class Program
    {

        static void Main(string[] args)
        {
            var container = new TaskContainer();
            ITaskExample example = null;
            while ((example =  container.GetTaskExample()) != null)
            {
                example.Execute();
                Console.WriteLine("Example is processed");
            }


        }
    }
}

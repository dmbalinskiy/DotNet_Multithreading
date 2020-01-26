using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading_app.Tasks
{
    interface ITaskExample
    {
        int Id { get; }
        string Description { get; }
        void Execute();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Threading_app.Tasks.Part3
{
    class ConcurrentDictionaryExample : ITaskExample
    {
        public int Id => 15;
        public string Description => "Concurrent Dictionary usage example";

        private ConcurrentDictionary<string, string> _capitals =
            new ConcurrentDictionary<string, string>();

        private void AddParis()
        {
            bool res = _capitals.TryAdd("France", "Paris");
            string who = Task.CurrentId.HasValue? ("Task " + Task.CurrentId) : "Main thread";
            Console.WriteLine($"{who} {  (res ? "added" : "did not add")} the element");
        }

        public void Execute()
        {
            Task.Factory.StartNew(AddParis).Wait();
            AddParis();

            _capitals["Russia"] = "Leningrad";
            _capitals.AddOrUpdate("Russia", "Moscow", (key, ov) => { return $"{ov} --> Moscow"; });

            //_capitals["Sweden"] = "Uppsala";
            _capitals.GetOrAdd("Sweden", "Stockholm");

            Console.WriteLine($"The capital of Russia is {_capitals["Russia"]}");
            Console.WriteLine($"The capital of Sweden is {_capitals["Sweden"]}");

            const string strToRemove = "Russia";
            string removed;
            var isRemoved = _capitals.TryRemove(strToRemove, out removed);
            if (isRemoved)
            {
                Console.WriteLine($"We just removed {removed}");
            }
            else
            {
                Console.WriteLine($"Failed to remove {strToRemove}");
            }

            //can be expensive!
            //_capitals.Count

            foreach(var kvp in _capitals)
            {
                Console.WriteLine($" - {kvp.Value} is the capital of {kvp.Key}");
            }

            ConcurrentQueue<string> q = new ConcurrentQueue<string>();
        }
    }
}

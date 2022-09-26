using Processor.Controllers;

namespace Processor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = ".\\data.json";
            var data = helpers.DataFileInterface.MakeDataFromFile(filePath);

            if(data.datasets.Count < 1 || data.datasets.Count < 1)
            {
                return;
            }

            var manager = new Manager(data);
            var startMs = DateTime.Now.Millisecond;

            while(true)
            {
                var now = DateTime.Now;
                (var keepGoing, var output) = manager.Tick();
                foreach(var line in output)
                {
                    var timestamp = now.ToString("HH:mm:ss");
                    Console.WriteLine(timestamp + " " + line);
                }

                if (!keepGoing)
                {
                    break;
                }

                int waitTimeMs = (1000 - now.Millisecond) + startMs;
                Thread.Sleep(waitTimeMs);
            }
        }

    }
}
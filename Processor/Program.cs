using Processor.Controllers;

namespace Processor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = ".\\data.json";
            var data = helpers.DataFileInterface.MakeDataFromFile(filePath);

            var datasets = data.datasets;
            var generatorSettings = data.generators;

            var generators = new List<Generator>();

            generatorSettings.ForEach(gs =>
            {
                if (gs.name == null || gs.interval == 0 || gs.operation == null)
                { //TODO: Say which setting is bad in error message
                    throw new Exception("Problem with generator definition");
                }

                generators.Add(new Generator(gs.name, gs.interval, gs.operation, datasets));
            });

            var start = DateTime.Now;
            while(generators.Count > 0)
            {
                var secondsSinceStart = (DateTime.Now - start).Seconds;

                var finishedGenerators = new List<Generator>();
                generators.ForEach(g =>
                {
                    if(secondsSinceStart % g.interval == 0)
                    {
                        var result = g.Generate();
                        if(result == null)
                        {
                            finishedGenerators.Add(g);
                        }
                        else
                        { //TODO: batch up results so that there's only 1 WriteLine per while loop. This will help with WPF refactoring.
                            Console.WriteLine(result);
                        }
                    }
                });

                finishedGenerators.ForEach(fg =>
                {
                    generators.Remove(fg);
                });

                Thread.Sleep(1000); //sleep 1 second
            }
            
        }

    }
}
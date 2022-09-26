using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor.Controllers
{
    public class GeneratorManager
    {
        private List<Generator> generators;
        private DateTime startTime;
        private bool started = false;

        public GeneratorManager(MdtTakeHome.models.Data data)
        {
            generators = new List<Generator>();
            MakeGenerators(data);
        }

        //Tick needs to be called every second
        //Returns null if there are generators left
        public string? Tick()
        {
            if(generators.Count == 0)
            {
                return null;
            }
            
            if(!started)
            {
                started = true;
                startTime = DateTime.Now;
            }

            var secondsSinceStart = (DateTime.Now - startTime).Seconds;

            var output = new StringBuilder();
            var finishedGenerators = new List<Generator>();
            generators.ForEach(g =>
            {
                if (secondsSinceStart % g.interval == 0)
                {
                    var result = g.Generate();
                    if (result == null)
                    {
                        finishedGenerators.Add(g);
                    }
                    else
                    {
                        output.Append(result + Environment.NewLine);
                    }
                }
            });

            finishedGenerators.ForEach(fg =>
            {
                generators.Remove(fg);
                Console.WriteLine($"Removed {fg.interval}");
            });

            return output.ToString();
        }

        private void MakeGenerators(MdtTakeHome.models.Data data)
        {
            var generatorSettings = data.generators;
                        
            generatorSettings.ForEach(gs =>
            {
                if (gs.name == null || gs.interval == 0 || gs.operation == null)
                { //TODO: Say which setting is bad in error message
                    throw new Exception("Problem with generator definition");
                }

                generators.Add(new Generator(gs.name, gs.interval, gs.operation, data.datasets));
            });
        }

    }
}

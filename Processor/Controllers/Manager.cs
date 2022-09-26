using MdtTakeHome.Models;

namespace Processor.Controllers
{
    public class Manager
    {
        private List<Generator> generators;
        public List<List<double>> datasets;
        private int tickNum = -1;

        //keeps track of which dataset the generator should use next
        private Dictionary<string, int> genToDataset;

        public Manager(Data data)
        {
            generators = data.generators;
            datasets = data.datasets;

            genToDataset = new Dictionary<string, int>();
            for (int i = 0; i < generators.Count; i++)
            {
                genToDataset.Add(generators[i].name, 0);
            }            
        }

        //Returns true if there are unfinished generators.
        //Lets the calling program know when to end its loop.
        public (bool, List<string>) Tick()
        { 
            tickNum++;

            var output = new List<string>();
            var finishedGenerators = new List<Generator>();
            for(int i = 0; i < generators.Count; i++)
            {
                var g = generators[i];
                if (tickNum % g.interval == 0)
                {
                    var result = g.Generate(datasets[genToDataset[g.name]]);
                    output.Add(g.name + " " + result);

                    //See if this generator is done
                    genToDataset[g.name]++;
                    if (genToDataset[g.name] >= datasets.Count)
                    {
                        finishedGenerators.Add(g);
                    }
                }
            };

            finishedGenerators.ForEach(fg =>
            {
                generators.Remove(fg);
            });

            return (generators.Any(), output);
        }

    }
}

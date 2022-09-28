using MdtTakeHome.Models;

namespace Processor.Controllers
{
    public class Manager
    {
        private readonly List<Generator> m_generators;
        private readonly List<List<double>> m_datasets;
        private int m_tickNum = -1;

        //keeps track of which dataset the generator should use next
        private Dictionary<string, int> m_genToDataset;

        public Manager(Data data)
        {
            m_generators = data.generators;
            m_datasets = data.datasets;

            m_genToDataset = new Dictionary<string, int>();
            for (int i = 0; i < m_generators.Count; i++)
            {
                m_genToDataset.Add(m_generators[i].name, 0);
            }
        }

        //Returns true if there are unfinished generators.
        //Lets the calling program know when to end its loop.
        public (bool, List<string>) Tick()
        {
            m_tickNum++;

            var output = new List<string>();
            var finishedGenerators = new List<Generator>();
            for(int i = 0; i < m_generators.Count; i++)
            {
                var g = m_generators[i];
                if (m_tickNum % g.interval == 0)
                {
                    var result = g.Generate(m_datasets[m_genToDataset[g.name]]);
                    output.Add(g.name + " " + result);

                    //See if this generator is done
                    m_genToDataset[g.name]++;
                    if (m_genToDataset[g.name] >= m_datasets.Count)
                    {
                        finishedGenerators.Add(g);
                    }
                }
            };

            finishedGenerators.ForEach(fg =>
            {
                m_generators.Remove(fg);
            });

            return (m_generators.Any(), output);
        }

    }
}

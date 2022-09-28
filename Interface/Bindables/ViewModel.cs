using MdtTakeHome.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Interface.Bindables
{
    public class ViewModel : INotifyPropertyChanged
    {
        public IEnumerable<DataSetBindable> Datasets { get; set; }
        public IEnumerable<GeneratorBindable> Generators { get; set; }
        public string Output {
            get { return m_output; }
            set
            {
                m_output = value;
                OnPropertyChanged();
            }
        }
        private string m_output;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void SetDatasets(Data data)
        {
            var datasetsBindable = new List<DataSetBindable>();
            for (int i = 0; i < data.datasets.Count; i++)
            {
                var ds = data.datasets[i];
                var datasetString = string.Join(",", ds.Select(n => n.ToString()).ToArray());
                datasetsBindable.Add(new DataSetBindable(i, datasetString));
            }
            Datasets = datasetsBindable;
        }

        public void SetGenerators(Data data)
        {
            var generatorsBindable = new List<GeneratorBindable>();
            for (int i = 0; i < data.generators.Count; i++)
            {
                var g = data.generators[i];
                var intervalString = g.interval.ToString();
                generatorsBindable.Add(new GeneratorBindable(g.name, intervalString, g.operation));
            }
            Generators = generatorsBindable;
        }

        public List<Generator> ParseGenerators()
        {
            var generators = new List<Generator>(Generators.Count());
            foreach (var generatorBindable in Generators)
            {
                var generator = new Generator
                {
                    interval = int.Parse(generatorBindable.Interval),
                    name = generatorBindable.Name,
                    operation = generatorBindable.Operation,
                };
                generators.Add(generator);
            }
            return generators;
        }

        public List<List<double>> ParseDatasets()
        {
            var datasets = (new List<double>[Datasets.Count()]).ToList();
            foreach (var datasetBindable in Datasets)
            { //TODO: Add error checking
                var dataset = datasetBindable.DatasetString.Split(',').Select(x => double.Parse(x)).ToList();
                datasets[datasetBindable.ID] = dataset;
            }
            return datasets;
        }
    }
}
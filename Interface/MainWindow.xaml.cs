using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Processor.Helpers;
using MdtTakeHome.Models;
using Interface.Bindables;

namespace Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModel viewModel;
        private readonly string FilePath = ".\\data.json";

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new ViewModel();
            DataContext = viewModel;

            var data = DataFileInterface.MakeDataFromFile(FilePath);
            CreateDatasets(data);
            CreateGenerators(data);

            viewModel.Output = "Click Run to start simulation and Save to save modified inputs.";
        }

        private void CreateDatasets(Data data)
        {
            var datasetsBindable = new List<DataSetBindable>();
            for (int i = 0; i < data.datasets.Count; i++)
            {
                var ds = data.datasets[i];
                var datasetString = string.Join(",", ds.Select(n => n.ToString()).ToArray());
                datasetsBindable.Add(new DataSetBindable(i, datasetString));
            }
            viewModel.Datasets = datasetsBindable;
        }

        private void CreateGenerators(Data data)
        {
            var generatorsBindable = new List<GeneratorBindable>();
            for(int i = 0; i < data.generators.Count; i++)
            {
                var g = data.generators[i];
                var intervalString = g.interval.ToString();
                generatorsBindable.Add(new GeneratorBindable(g.name, intervalString, g.operation));
            }
            viewModel.Generators = generatorsBindable;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            var data = new Data();

            data.datasets = ParseDatasets();
            data.generators = ParseGenerators();

            DataFileInterface.WriteDataToFile(FilePath, data);
        }

        private List<List<double>> ParseDatasets()
        {
            var datasets = (new List<double>[viewModel.Datasets.Count()]).ToList();
            foreach (var datasetBindable in viewModel.Datasets)
            { //lol who needs error checking
                var dataset = datasetBindable.DatasetString.Split(',').Select(x => double.Parse(x)).ToList();
                datasets[datasetBindable.ID] = dataset;
            }
            return datasets;
        }

        private List<Generator> ParseGenerators()
        {
            var generators = new List<Generator>(viewModel.Generators.Count());
            foreach (var generatorBindable in viewModel.Generators)
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

        private void Run_Button_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}

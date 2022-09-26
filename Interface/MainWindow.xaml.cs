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

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new ViewModel();
            DataContext = viewModel;

            string filePath = ".\\data.json";
            var data = DataFileInterface.MakeDataFromFile(filePath);
            CreateDatasets(data);
            CreateGenerators(data);

            
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

        }

        private void Run_Button_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Processor.Helpers;
using MdtTakeHome.Models;
using Interface.Bindables;
using Processor.Controllers;
using System.Threading;
using System;

namespace Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModel m_viewModel;
        private readonly string m_filePath = ".\\data.json";
        private readonly BackgroundWorker m_simWorker = new BackgroundWorker();
        private Manager? m_manager;

        public MainWindow()
        {
            InitializeComponent();

            m_viewModel = new ViewModel();
            DataContext = m_viewModel;

            var data = DataFileInterface.MakeDataFromFile(m_filePath);
            CreateDatasets(data);
            CreateGenerators(data);

            m_viewModel.Output = "Click Run to start simulation and Save to save modified inputs to '.\\data.json'.";

            m_simWorker.DoWork += SimWorker_DoWork;
            m_simWorker.WorkerReportsProgress = true;
            m_simWorker.ProgressChanged += SimWorker_ProgressChanged;
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
            m_viewModel.Datasets = datasetsBindable;
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
            m_viewModel.Generators = generatorsBindable;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            var data = new Data();

            data.datasets = ParseDatasets();
            data.generators = ParseGenerators();

            DataFileInterface.WriteDataToFile(m_filePath, data);
        }

        private List<List<double>> ParseDatasets()
        {
            var datasets = (new List<double>[m_viewModel.Datasets.Count()]).ToList();
            foreach (var datasetBindable in m_viewModel.Datasets)
            { //TODO: Add error checking
                var dataset = datasetBindable.DatasetString.Split(',').Select(x => double.Parse(x)).ToList();
                datasets[datasetBindable.ID] = dataset;
            }
            return datasets;
        }

        private List<Generator> ParseGenerators()
        {
            var generators = new List<Generator>(m_viewModel.Generators.Count());
            foreach (var generatorBindable in m_viewModel.Generators)
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
            if(m_manager == null)
            {
                m_viewModel.Output = "";
                m_simWorker.RunWorkerAsync();
            }
        }

        private void SimWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            var data = new Data();
            data.generators = ParseGenerators();
            data.datasets = ParseDatasets();

            m_manager = new Manager(data);
            var startMs = DateTime.Now.Millisecond;

            while (true)
            {
                var result = "";
                var now = DateTime.Now;
                (var keepGoing, var output) = m_manager.Tick();
                foreach (var line in output)
                {
                    var timestamp = now.ToString("HH:mm:ss");
                    result += timestamp + " " + line + Environment.NewLine;
                }

                m_simWorker.ReportProgress(0, result);

                if (!keepGoing)
                {
                    break;
                }

                int waitTimeMs = (1000 - now.Millisecond) + startMs;
                Thread.Sleep(waitTimeMs);
            }

            m_manager = null;
        }

        private void SimWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            var output = e.UserState as string;
            m_viewModel.Output += output;
        }

    }
}

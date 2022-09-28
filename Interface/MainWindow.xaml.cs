using System.ComponentModel;
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
            m_viewModel.SetDatasets(data);
            m_viewModel.SetGenerators(data);

            m_viewModel.Output = "Click Run to start simulation and Save to save modified inputs to '.\\data.json'.";

            m_simWorker.DoWork += SimWorker_DoWork;
            m_simWorker.WorkerReportsProgress = true;
            m_simWorker.ProgressChanged += SimWorker_ProgressChanged;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            var data = new Data();

            data.datasets = m_viewModel.ParseDatasets();
            data.generators = m_viewModel.ParseGenerators();

            DataFileInterface.WriteDataToFile(m_filePath, data);
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
            data.generators = m_viewModel.ParseGenerators();
            data.datasets = m_viewModel.ParseDatasets();

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

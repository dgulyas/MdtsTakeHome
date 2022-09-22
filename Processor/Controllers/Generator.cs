namespace Processor.Controllers
{
    public class Generator
    {
        public int interval { get; }

        private string name;
        private string operation;

        private List<List<double>> datasets;
        private int nextDataset = 0;

        private List<string> operations = new List<string> { "sum", "average", "min", "max" };

        public Generator(string a_name, int a_interval, string a_operation, List<List<double>> a_datasets)
        {
            name = a_name;
            interval = a_interval;
            operation = a_operation;

            if (!operations.Contains(operation))
            {
                throw new Exception($"Invalid Operations:{operation}");
            }

            datasets = a_datasets;
        }

        //Null return indicates this generator has run out of datasets.
        //It can be deleted/removed from the list of generators.
        public string? Generate()
        {
            if(nextDataset >= datasets.Count)
            {
                return null;
            }

            var timestamp = GetTimestamp();
            var result = GetResult(nextDataset);

            nextDataset++;

            return $"{timestamp} {name} {result}";

        }

        public string GetTimestamp()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        public double GetResult(int targetDataset)
        {
            var dataset = datasets[targetDataset];

            double result;

            if (operation == operations[0])
            {
                result = dataset.Sum();
            }
            else if (operation == operations[1])
            {
                result = dataset.Average();
            }
            else if (operation == operations[2])
            {
                result = dataset.Min();
            }
            else
            {
                result = dataset.Max();
            }
            return result;
        }

    }
}

namespace MdtTakeHome.Models
{
    public class Generator
    {
        public string name { get; set; }
        public int interval { get; set; }
        public string operation { get; set; }

        private List<string> operations = new List<string> { "sum", "average", "min", "max" };

        public double Generate(List<double> dataset)
        {
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

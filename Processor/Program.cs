namespace Processor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string filePath = ".\\data.json";
            var data = helpers.DataFileInterface.MakeDataFromFile(filePath);

            var datasets = data.datasets;
            var generators = data.generators;
        }
    }
}
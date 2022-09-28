using MdtTakeHome.Models;

namespace Tests.Processor
{
    public class GeneratorTests
    {

        //TODO: Might be better to use TestCaseSource?
        [TestCase("sum", 10)]
        [TestCase("average", 2.5)]
        [TestCase("min", 1)]
        [TestCase("max", 4)]
        public void GenerateChoosesCorrectOperation(string operation, double expectedResult)
        {
            var dataset = new List<double> { 1, 2, 3, 4 };

            var generator = new Generator();
            generator.operation = operation;

            Assert.That(generator.Generate(dataset), Is.EqualTo(expectedResult));
        }

    }
}

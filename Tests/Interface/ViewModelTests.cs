using Interface.Bindables;
using MdtTakeHome.Models;
using NUnit.Framework.Internal;

namespace Tests.Interface
{
    internal class ViewModelTests
    {
        [Test]
        public void SetDatasetsCreatesCorrectNumberOfBindables()
        {
            var data = new Data();
            data.datasets = new List<List<double>> {
                new List<double> { 1, 2, 3 },
                new List<double> { 4, 5, 6 }
            };

            var vm = new ViewModel();
            vm.SetDatasets(data);
            Assert.That(vm.Datasets.Count, Is.EqualTo(data.datasets.Count));
        }
    }
}

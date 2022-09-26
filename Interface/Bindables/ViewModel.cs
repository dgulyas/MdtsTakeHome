using System.Collections.Generic;

namespace Interface.Bindables
{
    internal class ViewModel
    {
        public IEnumerable<DataSetBindable> Datasets { get; set; }
        public IEnumerable<GeneratorBindable> Generators { get; set; }
    }
}
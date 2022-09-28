using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Interface.Bindables
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public IEnumerable<DataSetBindable> Datasets { get; set; }
        public IEnumerable<GeneratorBindable> Generators { get; set; }
        public string Output {
            get { return m_output; }
            set
            {
                m_output = value;
                OnPropertyChanged();
            }
        }
        private string m_output;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
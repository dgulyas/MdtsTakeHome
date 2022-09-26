namespace Interface.Bindables
{
    public class GeneratorBindable
    {
        public string Name { get; set; }
        public string Interval { get; set; }
        public string Operation { get; set; }

        public GeneratorBindable(string name, string interval, string operation)
        {
            Name = name;
            Interval = interval;
            Operation = operation;
        }
    }
}

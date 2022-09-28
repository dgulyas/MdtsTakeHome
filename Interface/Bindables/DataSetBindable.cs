namespace Interface.Bindables
{
    public class DataSetBindable
    {
        public int ID { get; set; }
        public string DatasetString { get; set; }

        public DataSetBindable(int id, string datasetString)
        {
            ID = id;
            DatasetString = datasetString;
        }

    }
}

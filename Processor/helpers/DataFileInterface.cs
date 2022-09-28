using MdtTakeHome.Models;
using System.Text.Json;

namespace Processor.Helpers
{
    public static class DataFileInterface
    {
        private const string DeserializationError = "Error during deserialization of file:";
        private const string SerializationError = "Error during serialization to file:";

        public static Data MakeDataFromFile(string FilePath)
        {
            string text = File.ReadAllText(FilePath);

            Data? data = null;
            try
            {
                data = JsonSerializer.Deserialize<Data>(text);
            }
            catch (Exception e)
            {
                throw new Exception($"{DeserializationError}{DeserializationError}", e);
            }

            if (data == null)
            {
                throw new Exception($"{DeserializationError}{FilePath}");
            }

            return data;
        }

        public static void WriteDataToFile(string FilePath, Data data)
        {
            string text;

            try
            {
                text = JsonSerializer.Serialize(data);
            }
            catch (Exception e)
            {
                throw new Exception($"{SerializationError}{FilePath}", e);
            }

            try
            {
                File.WriteAllText(FilePath, text);
            }
            catch (Exception e)
            {
                throw new Exception($"{SerializationError}{FilePath}", e);
            }
        }

    }
}

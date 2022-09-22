using MdtTakeHome.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Processor.helpers
{
    public static class DataFileInterface
    {
        private static string deserializationError = "Error during deserialization of file:";
        private static string serializationError = "Error during serialization to file:";

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
                throw new Exception($"{deserializationError}{deserializationError}", e);
            }

            if (data == null)
            {
                throw new Exception($"{deserializationError}{FilePath}");
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
                throw new Exception($"{serializationError}{FilePath}", e);
            }

            try
            {
                File.WriteAllText(FilePath, text);
            }
            catch (Exception e)
            {
                throw new Exception($"{serializationError}{FilePath}", e);
            }
        }

    }
}

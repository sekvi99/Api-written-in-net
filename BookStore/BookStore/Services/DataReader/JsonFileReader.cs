using System.Text.Json;

namespace BookStore.Services.DataReader
{
    public class JsonFileReader<T> : AbstractFileReader<T>
    {
        // Path to my place in my machine where my base data to seed is located
        private readonly string JsonFilePath = "C:\\Users\\vboxuser\\Desktop\\Net api\\Api-written-in-net\\BookStore\\BookStore\\Services\\Seeder\\SeederData\\base-book-stores.json";

        public override IEnumerable<T> Read()
        {
            try
            {
                // Read the JSON data from the file
                string jsonText = File.ReadAllText(JsonFilePath);

                // Deserialize the JSON data into a list of the provided generic type
                List<T> results = JsonSerializer.Deserialize<List<T>>(jsonText);

                if (results != null)
                {
                    return results;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The JSON file does not exist.");
            }
            catch (JsonException)
            {
                Console.WriteLine("Error deserializing the JSON data.");
            }

            return new List<T>();
        }
    }
}

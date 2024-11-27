using System.Text.Json;
using Domain.Models;

namespace FileData
{
    public class FileContext
    {
        private const string FilePath = "data.json"; // Path to the JSON file
        private DataContainer? dataContainer; // Nullable DataContainer for lazy loading

        public ICollection<BlogPost> BlogPosts
        {
            get
            {
                LoadData(); // Ensure data is loaded before access
                return dataContainer!.BlogPosts;
            }
        }

        public ICollection<User> Users
        {
            get
            {
                LoadData(); // Ensure data is loaded before access
                return dataContainer!.Users;
            }
        }

        /// <summary>
        /// Lazy-loads the data from the JSON file.
        /// </summary>
        private void LoadData()
        {
            // If data is already loaded, return
            if (dataContainer != null) return;

            // If the file does not exist, initialize a new DataContainer
            if (!File.Exists(FilePath))
            {
                dataContainer = new DataContainer
                {
                    BlogPosts = new List<BlogPost>(),
                    Users = new List<User>()
                };
                SaveChanges(); // Create the file with initial empty data
                return;
            }

            // Read data from file and deserialize it into the DataContainer
            string content = File.ReadAllText(FilePath);
            dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
        }

        /// <summary>
        /// Saves the current state of the data to the JSON file.
        /// </summary>
        public void SaveChanges()
        {
            // Serialize the DataContainer to JSON and write it to the file
            string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
            {
                WriteIndented = true // Format the JSON for readability
            });
            File.WriteAllText(FilePath, serialized);

            // Reset dataContainer to allow lazy loading again if needed
            dataContainer = null;
        }
    }
}
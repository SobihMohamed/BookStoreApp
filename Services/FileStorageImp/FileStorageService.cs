using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace BookStoreConsole.Services.FileStorageImp
{
    public class FileStorageService
    {
        //Singleton Implementation
        private static readonly Lazy<FileStorageService> _instance =
            new Lazy<FileStorageService>(() => new FileStorageService());

        public static FileStorageService Instance => _instance.Value;

        private FileStorageService() { }

        // to ensure consistent JSON formatting and case-insensitive property matching during serialization and deserialization.
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public async Task SaveDataAsync<T>(IEnumerable<T> data, string fileName)
        {
            try
            {
                // convert the data to JSON
                string json = JsonSerializer.Serialize(data, _options);
                await File.WriteAllTextAsync(fileName, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data to {fileName}: {ex.Message}");
            }
        }

        // read the JSON content from the file and deserialize it back into a list of objects of type T.
        // If the file doesn't exist or if there's an error during deserialization,
        // it returns an empty list.
        public async Task<List<T>> LoadDataAsync<T>(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    return new List<T>();

                string json = await File.ReadAllTextAsync(fileName);
                return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data from {fileName}: {ex.Message}");
                return new List<T>();
            }
        }
    }
}
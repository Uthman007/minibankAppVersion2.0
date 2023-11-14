using MiniBankApp2.Interfaces;
using MiniBankApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniBankApp2.Implementations
{
    internal class JsonDataService : IPersistenceService
    {
        private const string _jsonFilePath = "C:\\CSharpDemo\\JsonFiles\\Accounts.json";
        private List<Account> _accounts;

        // Define options to customize how we want the object serialized/deserialized
        private JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public JsonDataService()
        {
            _accounts = new List<Account>();
        }

        public List<Account> FetchAllAccounts()
        {
            // First, read all text from the JSON file
            string jsonContent = File.ReadAllText(_jsonFilePath);
            if (string.IsNullOrWhiteSpace(jsonContent))
            {
                Console.WriteLine("Unable to complete the fetch operation. JSON file has no content.");
                return new List<Account> { };
            }

            // Attempt to deserialize the JSON text to obtain bank accounts
            List<Account>? deserializedAccounts = JsonSerializer.Deserialize<List<Account>>(jsonContent, _options);
            if (deserializedAccounts == null)
            {
                Console.WriteLine("Unable to complete the fetch operation. Deserialization failed.");
                return new List<Account> { };
            }

            // Return the bank accounts, if any, or return the empty list 
            _accounts = deserializedAccounts;
            Console.WriteLine("Application initialized successfully.");
            return _accounts;
        }

        public Account FindAccount(int accountNumber)
        {
            // Search the accounts list for an account with the given account number
            var result = _accounts.Find(x => x.AccountNumber == accountNumber);
            return result;            
        }

        public bool SaveAllAccounts(List<Account> accounts)
        {
            // Check if the input list has any items
            if (!accounts.Any())
            {
                Console.WriteLine("Invalid operation! Cannot save an empty list of accounts.");
                return false;
            }
            
            // Serialize the given list into a JSON string
            var jsonContent = JsonSerializer.Serialize(accounts, _options);
            if (string.IsNullOrWhiteSpace(jsonContent))
            {
                Console.WriteLine("Unable to complete the save operation. Serialization failed.");
                return false;
            }

            // Write the JSON contant to the file
            File.WriteAllText(_jsonFilePath, jsonContent);
            Console.WriteLine("Save operation was successful.");
            return true;
        }
    }
}

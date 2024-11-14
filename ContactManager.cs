using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContactBook
{
    public class ContactManager
    {
        private List<Contact> contacts;
        private int nextId = 1;
        private readonly string filePath = "contacts.json";
        private Dictionary<string, Action> menuActions;

        public ContactManager()
        {
            contacts = LoadContactsFromFile();

            // Defines the next ID based on next contacts loaded
            if (contacts.Any())
            {
                nextId = contacts.Max(c => c.Id) + 1;
            }

            // Initiate action menu dictionary
            menuActions = new Dictionary<string, Action>()
            {
                { "1", AddContact },
                { "2", EditContact },
                { "3", DeleteContact },
                { "4", SearchContacts },
                { "5", ExitProgram }
            };
        }

        // Method to execute a choosen option for user
        public void ExecuteOption(string option)
        {
            if (menuActions.TryGetValue(option, out var action)) 
            {
                action.Invoke();
            }
            else
            {
                Console.WriteLine("Invalid Option. Please, choose a valid option.");
            }
        }

        private void AddContact()
        {
            Console.Write("Name: ");
            var name = Console.ReadLine();
            Console.Write("Phone Number: ");
            var phoneNumber = Console.ReadLine();
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Company: ");
            var company = Console.ReadLine();
            Console.Write("Category (Personal, Work, etc.): ");
            var category = Console.ReadLine();

            var contact = new Contact(nextId++, name, phoneNumber, email, company, category);
            contacts.Add(contact);
            SaveContactsToFile();
            Console.WriteLine("Contact added successfully!");
        }

        private void EditContact()
        {
            Console.Write("Contact ID to Edit: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var contact = contacts.FirstOrDefault(c => c.Id == id);
                if (contact != null)
                {
                    Console.Write("Name: ");
                    var name = Console.ReadLine();
                    Console.Write("Phone Number: ");
                    var phoneNumber = Console.ReadLine();
                    Console.Write("Email: ");
                    var email = Console.ReadLine();
                    Console.Write("Company: ");
                    var company = Console.ReadLine();
                    Console.Write("Category: ");
                    var category = Console.ReadLine();

                    SaveContactsToFile();
                    Console.WriteLine("Contact edited successfully!");
                }
                else
                {
                    Console.WriteLine("Contact not found");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        private void DeleteContact()
        {
            Console.Write("Contact ID to Remove: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var contact = contacts.FirstOrDefault(c => c.Id == id);
                if (contact != null)
                {
                    contacts.Remove(contact);
                    SaveContactsToFile();
                    Console.WriteLine("Contact removed successfully!");
                }
                else
                {
                    Console.WriteLine("Contact not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID");
            }
        }

        private void SearchContacts()
        {
            Console.Write("Searches Term: ");
            var searchTerm = Console.ReadLine();

            var results = contacts
                .Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                c.PhoneNumber.Contains(searchTerm) ||
                c.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                c.Company.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                c.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("Any contact founded.");
            }
            else
            {
                Console.WriteLine("Result of search: ");
                foreach ( var contact in results ) 
                {
                    Console.WriteLine(contact);
                }
            }
        }

        private void ExitProgram()
        {
            Console.WriteLine("Finishing program...");
            Environment.Exit(0);
        }

        private void SaveContactsToFile()
        {
            var json = JsonSerializer.Serialize(contacts);
            File.WriteAllText(filePath, json);
        }

        private List<Contact> LoadContactsFromFile()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
            }
            return new List<Contact>();
        }
    }
}

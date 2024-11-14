using ContactBook;

class Program
{
    static void Main(string[] args)
    {
        var contactManager = new ContactManager();

        while (true)
        {
            Console.WriteLine("\nChoose an option: ");
            Console.WriteLine("1. Add Contact: ");
            Console.WriteLine("2. Edit Contact: ");
            Console.WriteLine("3. Remove Contact: ");
            Console.WriteLine("4. Search Contact: ");
            Console.WriteLine("5. Exit: ");
            Console.WriteLine("Option: ");

            var choice = Console.ReadLine();
            contactManager.ExecuteOption(choice);
        }
    }        
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Category { get; set; }

        public Contact(int id, string name, string phoneNumber, string email, string company, string category)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Company = company;
            Category = category;
        }

        public override string ToString()
        {
            return $"{Name} ({Company}) - {Category} | Telefone: {PhoneNumber} | Email: {Email}";
        }
    }
}

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ContactRepository
    {
        private static List<Contact> contacts;

        static ContactRepository()
        {
            contacts = new List<Contact>()
            {
                new Contact(){ Id=Guid.NewGuid().ToString(), Name="张三", PhoneNo="123", EmailAddress="zhangsan@gmail.com" },
                new Contact(){ Id=Guid.NewGuid().ToString(), Name="李四", PhoneNo="456", EmailAddress="lisi@gmail.com" },
            };
        }

        public IEnumerable<Contact> Get()
        {
            return contacts;
        }

        public Contact Get(string id)
        {
            return contacts.FirstOrDefault(t => t.Id == id);
        }

        public void Put(Contact contact)
        {
            contact.Id = Guid.NewGuid().ToString();
            contacts.Add(contact);
        }

        public void Post(Contact contact)
        {
            Delete(contact.Id);
            contacts.Add(contact);
        }

        public void Delete(string id)
        {
            Contact contact = contacts.FirstOrDefault(t => t.Id == id);
            contacts.Remove(contact);
        }
    }
}

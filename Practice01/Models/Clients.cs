using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice01.Models
{
    internal class Clients
    {
        public Clients() { }

        private int id;
        private string nameOfOrganisation;
        private string addressOfOrganisation;
        private string contactName;

        public int Id { get { return id; } set { id = value; } }
        public string NameOfOrganisation { get { return nameOfOrganisation; } set { nameOfOrganisation = value; } }
        public string AddressOfOrganisation { get { return addressOfOrganisation; } set { addressOfOrganisation = value; } }
        public string ContactName { get { return contactName; } set { contactName = value; } }

    }
}

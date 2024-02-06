using Practice01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice01.Interfaces
{
    internal interface IDataClients
    {
        public Task<List<Clients>> ReadClientsDataFromFile(string path);

        public void EditClientInfo(string path, List<Clients> clients);
    }
}

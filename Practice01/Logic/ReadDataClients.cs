using ClosedXML.Excel;
using DocumentFormat.OpenXml.ExtendedProperties;
using Practice01.Interfaces;
using Practice01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice01.Logic
{
    internal class ReadDataClients : IReadDataClients
    {
        public List<Clients> ReadClientsDataFromFile(string path)
        {
            List<Clients> clients = new();

            var wbBook = new XLWorkbook(path);

            var wsl = wbBook.Worksheet(2);
            var countOfRows = wsl.Rows();

            for (int i = 2; i < countOfRows.Count() + 1; i++)
            {
                var row = wsl.Row(i);

                if (row.IsEmpty())
                {
                    break;
                }
                else
                {
                    clients.Add(new Clients
                    {
                        Id = Int32.Parse(row.Cell(1).GetValue<string>()),
                        NameOfOrganisation = row.Cell(2).GetValue<string>(),
                        AddressOfOrganisation = row.Cell(3).GetValue<string>(),
                        ContactName = row.Cell(4).GetValue<string>()
                    });
                }

            }

            //foreach (Clients client in clients)
            //{
            //    Console.WriteLine($"id = {client.Id} name = {client.NameOfOrganisation} numeric = {client.AddressOfOrganisation} price = {client.ContactName}");
            //}

            return clients;
        }
    }
}

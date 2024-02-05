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
    internal class ReadDataClients : IReadData
    {
        public void ReadDataFromFile(string path)
        {

            var wbBook = new XLWorkbook(path);

            var wsl = wbBook.Worksheet(2);
            var countOfRows = wsl.Rows();

            List<Clients> clients = new List<Clients>();

            int count = 0;

            Console.WriteLine($">>>{countOfRows.Count()}");

            for (int i = 2; i < countOfRows.Count() + 1; i++)
            {
                var row = wsl.Row(i);
                //count++;
                //Console.WriteLine($"id = {row.Cell(1).GetValue<string>()} Organisation = {row.Cell(2).GetValue<string>()} Address = {row.Cell(4).GetValue<string>()} Contact = {row.Cell(4).GetValue<string>()}");
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
            //Console.WriteLine(count) ;

            foreach (Clients client in clients)
            {
                Console.WriteLine($"id = {client.Id} name = {client.NameOfOrganisation} numeric = {client.AddressOfOrganisation} price = {client.ContactName}");
            }
        }
    }
}

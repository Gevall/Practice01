using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
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
    internal class DataClients : IDataClients
    {
        /// <summary>
        /// Получение данных об измененяемой записи и поиск её в базе
        /// </summary>
        /// <param name="path">Путь до файла с книгой Excel</param>
        /// <param name="clients">Список клиентов загруженных из книги в List</param>
        public void EditClientInfo(string path, List<Clients> clients)
        {
            // Вывод списка клиентов на экран
            foreach (Clients client in clients)
            {
                Console.WriteLine($"Клиент: {client.NameOfOrganisation}");
            }
            Console.Write("\nВведите название организации, где нужно изменить контактное лицо: ");
            string editClient = Console.ReadLine();
            // Поиск клиента в базе, если найден будем редактировать, иначе говорим что клиент не найден
            var editebleClient = clients.Find(x => x.NameOfOrganisation.ToLower() == editClient.ToLower());
            if (editebleClient != null)
            {
                Console.Write("\nВведите новое контактное лицо: ");
                string newContactClient = Console.ReadLine();
                editebleClient.ContactName = newContactClient;
                SaveNewContact(path, editebleClient);
            }
            else
            {
                Console.WriteLine("\nЗапись не найдена!");
            }
        }

        /// <summary>
        /// Сораняет данные измененной записи в книгу Excel
        /// </summary>
        /// <param name="path">Путь до файла с книгой Excel</param>
        /// <param name="client">Экземпляр изменённой записи</param>
        private void SaveNewContact(string path, Clients client)
        {
            var wbBook = new XLWorkbook(path);
            var wsl = wbBook.Worksheet(2);

            for (int i  = 2; i < wsl.Rows().Count() + 1 ; i++)
            {
                var row = wsl.Row(i);
                // Поиск нужной строки для изменения
                if (row.Cell(1).Value.ToString() == client.Id.ToString())
                {
                    // Вывод изменений пользователю
                    PrintChanges(row.Cell(4).Value.ToString(), client.ContactName, client.NameOfOrganisation);
                    row.Cell(4).Value = client.ContactName;
                    break;
                }
            }
            wbBook.Save();
            wbBook.Dispose();
        }

        /// <summary>
        /// Вывод изменений на экран
        /// </summary>
        /// <param name="oldContactName">Старые контактные данные клиента</param>
        /// <param name="newContactName">Новые контактные данные клиента</param>
        /// <param name="client">Наименование организации</param>
        private void PrintChanges(string oldContactName, string newContactName, string client)
        {
            Console.WriteLine($"\nКонтактные данные клиента \"{client}\" изменены с \"{oldContactName}\" на \"{newContactName}\"");
        }

        /// <summary>
        /// Получение данных из книги Excel и запись их в List
        /// </summary>
        /// <param name="path"></param>
        /// <returns>ВОзвращает заполненный List</returns>
        public async Task<List<Clients>> ReadClientsDataFromFile(string path)
        {
            List<Clients> clients = new();
            // Создание экземпляра книги Excel
            var wbBook = new XLWorkbook(path);
            // Получение нужного листа из созданной книги
            var wsl = wbBook.Worksheet(2);
            //Получение количества заполненных строк
            var countOfRows = wsl.Rows();
            // чтение данных по строкам
            for (int i = 2; i < countOfRows.Count() + 1; i++)
            {
                var row = wsl.Row(i);
                // Проверка строки на пустосту
                if (row.IsEmpty())
                {
                    break;
                }
                else
                {
                    // Добавление данных строки в List
                    clients.Add(new Clients
                    {
                        Id = Int32.Parse(row.Cell(1).GetValue<string>()),
                        NameOfOrganisation = row.Cell(2).GetValue<string>(),
                        AddressOfOrganisation = row.Cell(3).GetValue<string>(),
                        ContactName = row.Cell(4).GetValue<string>()
                    });
                }

            }
            wbBook.Dispose();

            return clients;
        }

    }
}

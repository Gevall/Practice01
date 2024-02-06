using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Spreadsheet;
using Practice01.Interfaces;
using Practice01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice01.Logic
{
    internal class FindClient : IFindClient
    {
        /// <summary>
        /// Вывод количества заказов по наименованию проудкта
        /// </summary>
        /// <param name="goods">База товаров</param>
        /// <param name="clients">База клиентов</param>
        /// <param name="orders">База заказов</param>
        public void PrintClientsBySearch(List<Goods> goods, List<Clients> clients, List<Orders> orders)
        {
            Console.Write("Введите наименование товара заказы на который нужно вывести: ");
            string input = Console.ReadLine();
            var selectProduct = goods.Find(x => x.NameOfProduct.ToLower() == input.ToLower());
            if (selectProduct != null)
            {
                var selectOrders = orders.FindAll(x => x.ProductId == selectProduct.Id);

                if (selectOrders.Count != 0)
                {
                    foreach (var client in selectOrders)
                    {
                        Console.WriteLine($"\nКлиент: \"{clients.Find(x => x.Id == client.ClientId).NameOfOrganisation}\" " +
                            $"Заказал товар: {input} {client.DateOfOrder.ToShortDateString()} " +
                            $"в количестве: {client.ValueOfProducts} " +
                            $"на сумму: {client.ValueOfProducts * selectProduct.Price} руб. " +
                            $"по цене: {selectProduct.Price} руб.");
                    }
                }
                else
                {
                    Console.WriteLine($"Товар - {input} не заказывался");
                }
            }
            else { Console.WriteLine($"Товар - {input} не найден в базе!"); }
        }

        /// <summary>
        /// Поиск клиента с наибольшим количеством заказов
        /// </summary>
        /// <param name="orders">База с заказами</param>
        /// <param name="clients">База с клиентами</param>
        public void FindGoldenClient(List<Orders> orders, List<Clients> clients)
        {
            Console.WriteLine("За какой период следует искать лучшего клиента?" +
                "\n  1) За месяц." +
                "\n  2) За год.");
            Console.Write("Введите номер строки: ");
            if(Int32.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        FindGoldenClientOfMonth(orders, clients);
                        break;
                    case 2:
                        FindGoldenClientOfYear(orders, clients);
                        break;
                    default:
                        Console.WriteLine("Введен неверный номер строки!");
                        break;
                }
            }
        }

        /// <summary>
        /// Поиск клиента с наибольшим количеством заказов в заданном месяце
        /// </summary>
        /// <param name="orders">База с заказами</param>
        /// <param name="clients">База с клиентами</param>
        private void FindGoldenClientOfMonth(List<Orders> orders, List<Clients> clients)
        {
            Console.WriteLine("Введите номер месяца: ");
            if (Int32.TryParse(Console.ReadLine(),out int month))
            {
                if(month >= 1 &&  month <= 12)
                {
                    // для хранения пары id клиента - количества заказов
                    Dictionary<int, int> clientsOrders = new Dictionary<int, int>();
                    // Переписываем клиентов в словарь и назначаем каждому 0 заказов
                    foreach(Clients client in clients)
                    {
                        clientsOrders.Add(client.Id, 0);
                    }
                    // Считаем количество заказов каждого клиента в этом месяце
                    foreach(Orders order in orders)
                    {
                        if (order.DateOfOrder.Month.ToString() == month.ToString())
                        {
                            clientsOrders[order.ClientId] += 1;
                        }
                    }
                    // Считаем что первый клиент с наибольшим количеством заказов
                    int maxOrderClientId = clientsOrders.First().Key; 
                    // Ищем клиента с наибольшим количеством заказов
                    foreach (KeyValuePair<int, int> pair in clientsOrders)
                    {
                        if (clientsOrders[maxOrderClientId] < pair.Value)
                        {
                            maxOrderClientId = pair.Key;
                        }
                    }

                    Console.WriteLine($"\nКлиент с наибольшим количеством заказов: " +
                        $"\"{clients.Find(x => x.Id == maxOrderClientId).NameOfOrganisation}\"") ;
                }
                else
                {
                    if (month == 0)
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели несуществующий номер месяца!" +
                            "\nВведите цифру от 1 до 12." +
                            "\nИли введите \"0\" для отмены");
                    }
                }
            }
        }
        
        /// <summary>
        /// Поиск клиентов с наибольшим количеством заказов в указанном году
        /// </summary>
        /// <param name="orders">База с заказами</param>
        /// <param name="clients">База с клиентами</param>
        private void FindGoldenClientOfYear(List<Orders> orders, List<Clients> clients)
        {
            Console.Write("Введите год, за который нужно найти клиента с наибольшим числом заказов: ");
            if (Int32.TryParse(Console.ReadLine(), out int yearOfOrder))
            {
                // для хранения пары id клиента - количества заказов
                Dictionary<int, int> clientsOrders = new Dictionary<int, int>(); 
                // Переписываем клиентов в словарь и назначаем каждому 0 заказов
                foreach (Clients client in clients)
                {
                    clientsOrders.Add(client.Id, 0);
                }
                // Проверка хотя бы 1 заказа в заданном году
                bool atLeasOneOrder = false;
                // Считаем количество заказов клиентов в заданном году
                foreach (Orders order in orders)
                {
                    if (order.DateOfOrder.Year.ToString() == yearOfOrder.ToString())
                    {
                        atLeasOneOrder = true;
                        clientsOrders[order.ClientId] += 1;
                    }
                }
                if (atLeasOneOrder)
                {
                    // Считаем что первый клиент с наибольшим количеством заказов
                    int maxOrderClientId = clientsOrders.First().Key;
                    // Ищем клиента с наибольшим количеством заказов
                    foreach (KeyValuePair<int, int> pair in clientsOrders)
                    {
                        if (clientsOrders[maxOrderClientId] < pair.Value)
                        {
                            maxOrderClientId = pair.Key;
                        }
                    }

                    Console.WriteLine($"\nКлиент с наибольшим количеством заказов в {yearOfOrder} году: " +
                        $"\"{clients.Find(x => x.Id == maxOrderClientId).NameOfOrganisation}\"");
                }
                else Console.WriteLine($"\nВ {yearOfOrder} году не было сделано ни одного заказа");
            }
            else
            {
                if (yearOfOrder == 0)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Вы ввели некоректный год!" +
                        "\nВведите корректный номер года. Пример: 2023" +
                        "\nИли введите \"0\" для отмены");
                }
            }
        }

    }
}

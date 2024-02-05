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
        public void PrintClientsBySearch(List<Goods> goods, List<Clients> clients, List<Orders> orders)
        {
            Console.Write("Введите наименование клиента чьи заказы необходимо вывести: ");
            string input = Console.ReadLine();
            var selectProduct = goods.Find(x => x.NameOfProduct.ToLower() == input.ToLower());
            var selectOrders = orders.FindAll(x => x.ProductId == selectProduct.Id);

            //Console.WriteLine($"{selectOrders.Id} {selectOrders.ClientId}");
            if (selectOrders.Count != 0)
            {
                foreach (var client in selectOrders)
                {
                    Console.WriteLine($"Клиент: \"{clients.Find(x => x.Id == client.ClientId).NameOfOrganisation}\" " +
                        $"Заказал товар: {input} {client.DateOfOrder.Date} " +
                        $"в количестве: {client.ValueOfProducts} " +
                        $"на сумму: {client.ValueOfProducts * selectProduct.Price} руб. " +
                        $"по цене: {selectProduct.Price} руб.");
                }
            }
            else
            {
                Console.WriteLine("Данный продукт не заказывался");
            }


        }
    }
}

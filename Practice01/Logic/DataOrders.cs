using ClosedXML.Excel;
using Practice01.Interfaces;
using Practice01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice01.Logic
{
    internal class DataOrders : IDataOrders
    {
        /// <summary>
        /// Получние данных о заказх из Книги Excel
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<List<Orders>> ReadOrdersDataFromFile(string path)
        {
            List<Orders> orders = new();

            var wbBook = new XLWorkbook(path);

            var wsl = wbBook.Worksheet(3);
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
                    orders.Add(new Orders
                    {
                        Id = Int32.Parse(row.Cell(1).GetValue<string>()),
                        ProductId = Int32.Parse(row.Cell(2).GetValue<string>()),
                        ClientId = Int32.Parse(row.Cell(3).GetValue<string>()),
                        PurshaseId = Int32.Parse(row.Cell(4).GetValue<string>()),
                        ValueOfProducts = Int32.Parse(row.Cell(5).GetValue<string>()),
                        DateOfOrder = DateTime.Parse(row.Cell(6).GetValue<string>())
                    });
                }
            }
            return orders;
        }
    }
}

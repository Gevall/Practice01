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
    internal class ReadDataGoods : IReadDataGoods
    {
        /// <summary>
        /// Получение списка товаров и запись их в List
        /// </summary>
        /// <param name="path">Путь к файлу Excel</param>
        public List<Goods> ReadGoodsDataFromFile(string path)
        {
            List<Goods> goods = new();
            var wbBook = new XLWorkbook(path);

            var wsl = wbBook.Worksheet(1);
            var countOfRows = wsl.Rows();
            //Console.WriteLine($">>> {countOfRows.Count()}");

            for (int i = 2; i < countOfRows.Count() + 1; i++)
            {
                var row = wsl.Row(i);
                if (row.IsEmpty())
                {
                    break;
                }
                else
                {
                    goods.Add(new Goods
                    {
                        Id = Int32.Parse(row.Cell(1).GetValue<string>()),
                        NameOfProduct = row.Cell(2).GetValue<string>(),
                        NumericValue = row.Cell(3).GetValue<string>(),
                        Price = Int32.Parse(row.Cell(4).GetValue<string>())
                    });
                }
            }
            //foreach (Goods good in goods)
            //{
            //    Console.WriteLine($"id = {good.Id} name = {good.NameOfProduct} numeric = {good.NumericValue} price = {good.Price}");
            //}

            return goods;
        }

    }
}

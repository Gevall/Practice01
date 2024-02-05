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
    internal class ReadDataGoods : IReadData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void ReadDataFromFile(string path)
        {
            var wbBook = new XLWorkbook(path);

            var wsl = wbBook.Worksheet(1);
            var countOfRows = wsl.Rows();
            //Console.WriteLine($">>> {test.Count()}");

            List<Goods> goods = new List<Goods>();

            for (int i = 2; i < countOfRows.Count() + 1; i++)
            {
                var row = wsl.Row(i);

                goods.Add(new Goods
                {
                    Id = Int32.Parse(row.Cell(1).GetValue<string>()),
                    NameOfProduct = row.Cell(2).GetValue<string>(),
                    NumericValue = row.Cell(3).GetValue<string>(),
                    Price = Int32.Parse(row.Cell(4).GetValue<string>())
                });
            }

            foreach (Goods good in goods)
            {
                Console.WriteLine($"id = {good.Id} name = {good.NameOfProduct} numeric = {good.NumericValue} price = {good.Price}");
            }
        }

    }
}

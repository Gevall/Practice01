using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Practice01.Interfaces;
using Practice01.Logic;
using Practice01.Models;

namespace Practice01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            IReadData goods  = new ReadDataGoods();
            IReadData clients = new ReadDataClients();
            IReadData orders = new ReadDataOrders();
            //goods.ReadDataFromFile(ReadPathFromConsole());
            string path = "test.xlsx";
            //clients.ReadDataFromFile(path);
            orders.ReadDataFromFile(path);
        }

        /// <summary>
        /// Метод получения пути от пользоватля из консоли
        /// </summary>
        /// <returns>Возвращает путь к файлу Excel</returns>
        private static string ReadPathFromConsole()
        {
            Console.Write("Введите путь до файла с базой:");
            string path = Console.ReadLine();
            if (File.Exists(path))
            {
                return path;
            }
            else
            {
                Console.WriteLine("Файл не существует или неверный путь!");
                return null;
            }
        }
    }
}
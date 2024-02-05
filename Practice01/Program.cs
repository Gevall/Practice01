using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Practice01.Interfaces;
using Practice01.Logic;
using Practice01.Models;

namespace Practice01
{
    internal class Program
    {

        public static List<Goods> goodsBase;
        public static List<Clients> clientsBase;
        public static List<Orders> ordersBase;
        static void Main(string[] args)
        {

            IReadDataGoods goods = new ReadDataGoods();
            IReadDataClients clients = new ReadDataClients();
            IReadDataOrders orders = new ReadDataOrders();
            string path = ReadPathFromConsole();
            goodsBase = goods.ReadGoodsDataFromFile(path);
            clientsBase = clients.ReadClientsDataFromFile(path);
            ordersBase = orders.ReadOrdersDataFromFile(path);
            MainMenu(path);

        }

        /// <summary>
        /// Метод получения пути от пользоватля из консоли
        /// </summary>
        /// <returns>Возвращает путь к файлу Excel</returns>
        private static string ReadPathFromConsole()
        {
            Console.Write("Введите путь до файла с базой: ");
            string path = Console.ReadLine() + ".xlsx";  //Так как мы знаем, что файл будет именно формата .xlsx, то не добавляем лишней проверки на расширение файла
            if (File.Exists(path))
            {
                return path;
            }
            else
            {
                Console.WriteLine("Файл не существует или неверный путь!");
                return ReadPathFromConsole();
            }
        }

        private static void MainMenu(string path)
        {
            bool exitFromProgram = true;
            int choice;
            while(exitFromProgram)
            {
                Console.WriteLine("Меню:" +
                    "\n  1) Выберете \"1\" для поиска клиентов по заказанному товару." +
                    "\n  0) Выйти из программы");
                choice = Int32.Parse(Console.ReadLine());
                switch(choice)
                {
                    case 0:
                        exitFromProgram= false;
                        break;
                    case 1:
                        IFindClient find = new FindClient();
                        find.PrintClientsBySearch(goodsBase, clientsBase, ordersBase);
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда! Пожалуйста выберете значение из списка!");
                        break;
                }
            }
            Console.WriteLine("Надеюсь Вам понравилось наше приложение!");
        }
    }
}
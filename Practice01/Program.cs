using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Practice01.Interfaces;
using Practice01.Logic;
using Practice01.Models;

namespace Practice01
{
    internal class Program
    {

        public static List<Goods> goodsBase; // База с данными товаров
        public static List<Clients> clientsBase; // База с данными клиентов
        public static List<Orders> ordersBase; //База с данными заказов

        static void Main(string[] args)
        {
            LoadingDataAsync();
        }


        /// <summary>
        /// Загрузка основных данных
        /// </summary>
        private static async Task LoadingDataAsync()
        {
            IDataGoods goods = new DataGoods();
            IDataClients clients = new DataClients();
            IDataOrders orders = new DataOrders();
            string path = ReadPathFromConsole();
            goodsBase = await goods.ReadGoodsDataFromFile(path);
            clientsBase = await clients.ReadClientsDataFromFile(path);
            ordersBase = await orders.ReadOrdersDataFromFile(path);
            MainMenu(path, goods, clients, orders);
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

        /// <summary>
        /// Меню выбора программы
        /// </summary>
        /// <param name="path">Путь к файлу с книгой Excel</param>
        /// <param name="goods">База с товарами</param>
        /// <param name="clients">База с клиентами</param>
        /// <param name="orders">База с заказами</param>
        private static void MainMenu(string path, IDataGoods goods, IDataClients clients, IDataOrders orders)
        {
            IFindClient find = new FindClient();
            bool exitFromProgram = true;
            while(exitFromProgram)
            {
                Console.WriteLine("\nМеню:" +
                    "\n  1) Выберете \"1\" для поиска клиентов по заказанному товару." +
                    "\n  2) Выберете \"2\" для редактирвоания информации о клиенте." +
                    "\n  3) Выберете \"3\" для поиска клиента с наибольшим количеством заказов" +
                    "\n  0) Выйти из программы");
                if (Int32.TryParse(Console.ReadLine(), out int choice))
                {
                    switch(choice)
                    {
                        case 0:
                            exitFromProgram= false;
                            break;
                        case 1:
                            find.PrintClientsBySearch(goodsBase, clientsBase, ordersBase);
                            break;
                        case 2:
                            clients.EditClientInfo(path, clientsBase);
                            break;
                        case 3:
                            find.FindGoldenClient(ordersBase, clientsBase);
                            break;
                        default:
                            Console.WriteLine("Неизвестная команда! Пожалуйста выберете значение из списка!");
                            break;
                    }
                }
                else { Console.WriteLine("Введен неверный символ! Введите номер строки из списка!"); }
            }
            Console.WriteLine("Надеюсь Вам понравилось наше приложение!");
        }
    }
}
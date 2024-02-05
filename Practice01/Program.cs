using ClosedXML;
using ClosedXML.Excel;

namespace Practice01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            ReadFromFile();
        }

        private static void ReadFromFile()
        {
            var wbBook = new XLWorkbook("test.xlsx");
            
            var wsl = wbBook.Worksheet(1);

            var row = wsl.Row(3);
            var test = wsl.Rows();
            Console.WriteLine($">>> {test.Count()}");
            
            bool empty = row.IsEmpty();

            var cell = row.Cell(1);

            Console.WriteLine(cell.GetValue<string>());
        }
    }
}
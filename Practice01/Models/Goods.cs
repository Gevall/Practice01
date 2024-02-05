using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice01.Models
{
    internal class Goods
    {
        public Goods() { }

        private int id;
        private string nameOfProduct;
        private string numericValue;
        private int price;

        public int Id { get { return id; } set { id = value; } }
        public string NameOfProduct { get { return nameOfProduct; } set { nameOfProduct = value; } }
        public string NumericValue { get { return numericValue; } set { numericValue = value; } }
        public int Price { get { return price; } set { price = value; } }

    }
}

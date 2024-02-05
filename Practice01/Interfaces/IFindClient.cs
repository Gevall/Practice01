using DocumentFormat.OpenXml.Bibliography;
using Practice01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice01.Interfaces
{
    internal interface IFindClient
    {
        public void PrintClientsBySearch(List<Goods> goods, List<Clients> clients, List<Orders> orders);
    }
}

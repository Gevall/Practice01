﻿using Practice01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice01.Interfaces
{
    internal interface IDataOrders
    {
        public Task<List<Orders>> ReadOrdersDataFromFile(string path);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice01.Models
{
    internal class Orders
    {
        public Orders() { }

        private int id;
        private int productId;
        private int clientId;
        private int purshaseId;
        private int valueOfProducts;
        private DateTime dateOfOrder;

        public int Id { get { return id; } set { id = value; } }
        public int ProductId { get {  return productId; } set {  productId = value; } }
        public int ClientId { get { return clientId; } set { clientId = value; } } 
        public int PurshaseId { get { return purshaseId; } set {  purshaseId = value; } }
        public int ValueOfProducts { get { return valueOfProducts; } set { valueOfProducts = value; } }
        public DateTime DateOfOrder { get {  return dateOfOrder; } set {  dateOfOrder = value; } }
    }
}

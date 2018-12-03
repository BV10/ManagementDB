using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementShopDB
{
    static class SqlScripts
    {
        public const string SELECT_CLIENTS = "SELECT * FROM Clients();";
        public const string SELECT_ACCESSORIES = "SELECT * FROM Accessories()";
        public const string SELECT_ORDERS = "SELECT * FROM Orders()";
        public const string SELECT_STOCK_STORES = "SELECT * FROM StockStores()";
        public const string SELECT_ACCESSORY_TYPE = "SELECT * FROM AccessoryTypes()";
        public const string SELECT_PRODUCERS = "SELECT * FROM Producers()";
    }
}

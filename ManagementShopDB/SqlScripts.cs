using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementShopDB
{
    static class SqlScripts
    {
        public const string SELECT_CLIENTS = "SELECT " +
            "Shop.Client.ClientId AS[Id клиента], " +
            "Shop.Client.FirstName AS[Имя клиента], " +
            "Shop.Client.LastName AS[Фамилие клиента], " +
            "Shop.Client.Phone AS[Телефон клиента] " +
            "FROM Shop.Client";

        public const string SELECT_ACCESSORIES = "SELECT " +
              "AccessType.TypeName AS [Тип аксессуара]," +
              " Access.AccessoryName AS [Название аксессуара], " +
              "Access.Color AS [Цвет], " +
              "Access.TypeOfLink AS [Тип соединения], " +
              "Produc.CompanyName AS [Название компании], " +
              "Produc.Country AS [Страна производитель]" +
              "FROM Shop.Accessory AS Access " +
              "INNER JOIN Shop.AccessoryType AS AccessType ON AccessType.AccessoryTypeId = Access.AccessoryTypeId " +
              "INNER JOIN Shop.Producer AS Produc ON Produc.ProducerId = Access.ProducerId";

        public const string SELECT_ORDERS = "SELECT" +
                " Shop.Orders.OrderId AS [Id заказа], " +
                "Shop.Client.FirstName AS [Имя клиента]," +
                " Shop.Client.LastName AS [Фамилие клиента]," +
                " Shop.Client.Phone AS [Номер телефона]," +
                " Shop.Accessory.AccessoryName AS [Аксессуар], " +
                "Shop.Accessory.Color AS [Цвет]," +
                " Shop.Accessory.TypeOfLink AS [Тип соединения], " +
                "Shop.Goods.CountAccessory AS [Количество] FROM Shop.Orders" +
                " INNER JOIN Shop.Client ON Shop.Client.ClientId = Shop.Orders.ClientId" +
                " INNER JOIN Shop.Goods ON Shop.Orders.OrderId = Shop.Goods.OrderId" +
                " INNER JOIN Shop.Accessory ON Shop.Accessory.AccessoryId = Shop.Goods.AccessoryId";

    }
}

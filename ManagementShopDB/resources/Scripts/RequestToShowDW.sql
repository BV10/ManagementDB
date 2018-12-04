USE ShopDW
GO

--выбор всех продаж
SELECT 
Sales.Orders.OrderId AS [Id заказа],
Sales.Orders.City AS [Город доставки],
Sales.Client.FirstName AS [Имя клиента],
Sales.Client.LastName AS [Фамилия клиента],
Sales.Client.Phone AS [Номер телефона],
Sales.Accessory.AccessoryName AS [Аксессуар],
Sales.Accessory.Color AS [Цвет],
Sales.Accessory.TypeOfLink AS [Тип соединения],
Sales.Goods.CountAccessory AS [Количество]
FROM Sales.Facts
INNER JOIN Sales.Orders
ON Sales.Orders.OrderId = Sales.Facts.OrderID
INNER JOIN Sales.Client
ON Sales.Client.ClientId = Sales.Facts.ClientId
INNER JOIN Sales.Goods
ON Sales.Orders.OrderId = Sales.Goods.OrderId
INNER JOIN Sales.Accessory
ON Sales.Accessory.AccessoryId = Sales.Goods.AccessoryId
GO

--выбор всех продаж в городе
CREATE FUNCTION Sales.GetSales(@City nvarchar(20))
RETURNS TABLE
AS
RETURN SELECT 
Sales.Orders.OrderId AS [Id заказа],
Sales.Client.FirstName AS [Имя клиента],
Sales.Client.LastName AS [Фамилия клиента],
Sales.Client.Phone AS [Номер телефона],
Sales.Accessory.AccessoryName AS [Аксессуар],
Sales.Accessory.Color AS [Цвет],
Sales.Accessory.TypeOfLink AS [Тип соединения],
Sales.Goods.CountAccessory AS [Количество]
FROM Sales.Facts
INNER JOIN Sales.Orders
ON Sales.Orders.OrderId = Sales.Facts.OrderID
INNER JOIN Sales.Client
ON Sales.Client.ClientId = Sales.Facts.ClientId
INNER JOIN Sales.Goods
ON Sales.Orders.OrderId = Sales.Goods.OrderId
INNER JOIN Sales.Accessory
ON Sales.Accessory.AccessoryId = Sales.Goods.AccessoryId
WHERE Sales.Orders.City = @City
GO

SELECT * FROM Sales.GetSales('Днепр')
GO

--Запрос, отображающий информацию о продажах определенного типа аксессуара
CREATE FUNCTION Sales.GetSalesOfTypeAccessory(@TypeAccessory nvarchar(20))
RETURNS TABLE
AS
RETURN SELECT 
Sales.Orders.OrderId AS [Id заказа],
Sales.Client.FirstName AS [Имя клиента],
Sales.Client.LastName AS [Фамилия клиента],
Sales.Client.Phone AS [Номер телефона],
Sales.Accessory.AccessoryName AS [Аксессуар],
Sales.Accessory.Color AS [Цвет],
Sales.Accessory.TypeOfLink AS [Тип соединения],
Sales.Goods.CountAccessory AS [Количество]
FROM Sales.Facts
INNER JOIN Sales.Orders
ON Sales.Orders.OrderId = Sales.Facts.OrderID
INNER JOIN Sales.Client
ON Sales.Client.ClientId = Sales.Facts.ClientId
INNER JOIN Sales.Goods
ON Sales.Orders.OrderId = Sales.Goods.OrderId
INNER JOIN Sales.Accessory
ON Sales.Accessory.AccessoryId = Sales.Goods.AccessoryId
INNER JOIN Sales.AccessoryType
ON Sales.AccessoryType.AccessoryTypeId = Sales.Accessory.AccessoryTypeId
WHERE Sales.AccessoryType.TypeName = @TypeAccessory
GO

SELECT * FROM Sales.GetSalesOfTypeAccessory('Мышка');
GO

--Запрос, отображающий информацию о продажах определенного типа аксессуара
CREATE FUNCTION Sales.GetSalesOfNameAccessory(@NameAccessory nvarchar(20))
RETURNS TABLE
AS
RETURN SELECT 
Sales.Orders.OrderId AS [Id заказа],
Sales.Client.FirstName AS [Имя клиента],
Sales.Client.LastName AS [Фамилия клиента],
Sales.Client.Phone AS [Номер телефона],
Sales.Accessory.AccessoryName AS [Аксессуар],
Sales.Accessory.Color AS [Цвет],
Sales.Accessory.TypeOfLink AS [Тип соединения],
Sales.Goods.CountAccessory AS [Количество]
FROM Sales.Facts
INNER JOIN Sales.Orders
ON Sales.Orders.OrderId = Sales.Facts.OrderID
INNER JOIN Sales.Client
ON Sales.Client.ClientId = Sales.Facts.ClientId
INNER JOIN Sales.Goods
ON Sales.Orders.OrderId = Sales.Goods.OrderId
INNER JOIN Sales.Accessory
ON Sales.Accessory.AccessoryId = Sales.Goods.AccessoryId
WHERE Sales.Accessory.AccessoryName = @NameAccessory
GO

SELECT * FROM Sales.GetSalesOfNameAccessory('Наушники M17');
GO
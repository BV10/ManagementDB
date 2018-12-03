USE ShopDB
GO
--Выберем все акссесуары
CREATE FUNCTION Accessories()
RETURNS TABLE
AS
RETURN SELECT 
	   Access.AccessoryId AS [Id],
	   AccessType.TypeName AS [Тип аксессуара], 
	   Access.AccessoryName AS [Название аксессуара], 
	   Access.Color AS [Цвет],
	   Access.TypeOfLink AS [Тип соединения], 	
	   Produc.CompanyName AS [Название компании],   
	   Produc.Country AS [Страна производитель]   
FROM Shop.Accessory AS Access
INNER JOIN Shop.AccessoryType AS AccessType
ON AccessType.AccessoryTypeId = Access.AccessoryTypeId
INNER JOIN Shop.Producer AS Produc
ON Produc.ProducerId = Access.ProducerId
GO

--выберем все типы аксессуаров 
CREATE FUNCTION AccessoryTypes()
RETURNS TABLE
AS
RETURN 
SELECT TypeName FROM Shop.AccessoryType
GO

--выберем всех производителей
CREATE FUNCTION Producers()
RETURNS TABLE
AS
RETURN 
SELECT CompanyName, Country FROM Shop.Producer
GO

-- выберем всех клиентов
CREATE FUNCTION Clients()
RETURNS TABLE
AS
RETURN SELECT 
Shop.Client.ClientId AS [Id клиента],
Shop.Client.FirstName AS [Имя клиента],
Shop.Client.LastName AS [Фамилия клиента],
Shop.Client.Phone AS [Телефон клиента]
 FROM Shop.Client
 GO

--веберем все заказы
CREATE FUNCTION Orders()
RETURNS TABLE
AS
RETURN SELECT Shop.Orders.OrderId AS [Id заказа],
Shop.Client.FirstName AS [Имя клиента],
Shop.Client.LastName AS [Фамилия клиента],
Shop.Client.Phone AS [Номер телефона],
Shop.Accessory.AccessoryName AS [Аксессуар],
Shop.Accessory.Color AS [Цвет],
Shop.Accessory.TypeOfLink AS [Тип соединения],
Shop.Goods.CountAccessory AS [Количество]
FROM Shop.Orders
INNER JOIN Shop.Client
ON Shop.Client.ClientId = Shop.Orders.ClientId
INNER JOIN Shop.Goods
ON Shop.Orders.OrderId = Shop.Goods.OrderId
INNER JOIN Shop.Accessory
ON Shop.Accessory.AccessoryId = Shop.Goods.AccessoryId
GO

--выберем склады
CREATE FUNCTION StockStores()
RETURNS TABLE
AS
RETURN SELECT 
Shop.StockStore.City AS [Город склада],
Shop.StockStore.AddressOfStock AS [Улица склада],
Shop.ManagerStockStore.LastName AS [Фамилия управляющего складом],
Shop.ManagerStockStore.FirstName AS [Имя управляющего складом],
Shop.ManagerStockStore.Phone AS [Телефон управляющего складом]
FROM Shop.StockStore
INNER JOIN Shop.ManagerStockStore
ON Shop.ManagerStockStore.StockId = Shop.StockStore.StockId
GO

SELECT * FROM Shop.Producer
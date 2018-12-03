USE ShopDB
GO
--������� ��� ����������
CREATE FUNCTION Accessories()
RETURNS TABLE
AS
RETURN SELECT 
	   Access.AccessoryId AS [Id],
	   AccessType.TypeName AS [��� ����������], 
	   Access.AccessoryName AS [�������� ����������], 
	   Access.Color AS [����],
	   Access.TypeOfLink AS [��� ����������], 	
	   Produc.CompanyName AS [�������� ��������],   
	   Produc.Country AS [������ �������������]   
FROM Shop.Accessory AS Access
INNER JOIN Shop.AccessoryType AS AccessType
ON AccessType.AccessoryTypeId = Access.AccessoryTypeId
INNER JOIN Shop.Producer AS Produc
ON Produc.ProducerId = Access.ProducerId
GO

--������� ��� ���� ����������� 
CREATE FUNCTION AccessoryTypes()
RETURNS TABLE
AS
RETURN 
SELECT TypeName FROM Shop.AccessoryType
GO

--������� ���� ��������������
CREATE FUNCTION Producers()
RETURNS TABLE
AS
RETURN 
SELECT CompanyName, Country FROM Shop.Producer
GO

-- ������� ���� ��������
CREATE FUNCTION Clients()
RETURNS TABLE
AS
RETURN SELECT 
Shop.Client.ClientId AS [Id �������],
Shop.Client.FirstName AS [��� �������],
Shop.Client.LastName AS [������� �������],
Shop.Client.Phone AS [������� �������]
 FROM Shop.Client
 GO

--������� ��� ������
CREATE FUNCTION Orders()
RETURNS TABLE
AS
RETURN SELECT Shop.Orders.OrderId AS [Id ������],
Shop.Client.FirstName AS [��� �������],
Shop.Client.LastName AS [������� �������],
Shop.Client.Phone AS [����� ��������],
Shop.Accessory.AccessoryName AS [���������],
Shop.Accessory.Color AS [����],
Shop.Accessory.TypeOfLink AS [��� ����������],
Shop.Goods.CountAccessory AS [����������]
FROM Shop.Orders
INNER JOIN Shop.Client
ON Shop.Client.ClientId = Shop.Orders.ClientId
INNER JOIN Shop.Goods
ON Shop.Orders.OrderId = Shop.Goods.OrderId
INNER JOIN Shop.Accessory
ON Shop.Accessory.AccessoryId = Shop.Goods.AccessoryId
GO

--������� ������
CREATE FUNCTION StockStores()
RETURNS TABLE
AS
RETURN SELECT 
Shop.StockStore.City AS [����� ������],
Shop.StockStore.AddressOfStock AS [����� ������],
Shop.ManagerStockStore.LastName AS [������� ������������ �������],
Shop.ManagerStockStore.FirstName AS [��� ������������ �������],
Shop.ManagerStockStore.Phone AS [������� ������������ �������]
FROM Shop.StockStore
INNER JOIN Shop.ManagerStockStore
ON Shop.ManagerStockStore.StockId = Shop.StockStore.StockId
GO

SELECT * FROM Shop.Producer
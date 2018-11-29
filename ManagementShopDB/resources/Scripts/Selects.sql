USE ShopDB
GO
--������� ��� ����������
SELECT 
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

-- ������� ���� ��������
SELECT 
Shop.Client.ClientId AS [Id �������],
Shop.Client.FirstName AS [��� �������],
Shop.Client.LastName AS [������� �������],
Shop.Client.Phone AS [������� �������]
 FROM Shop.Client

--������� ��� ������
SELECT Shop.Orders.OrderId AS [Id ������],
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

--������� ������
SELECT 
Shop.StockStore.City AS ["����� ������"],
Shop.StockStore.AddressOfStock AS ["����� ������"],
Shop.ManagerStockStore.LastName AS ["������� ������������ �������"],
Shop.ManagerStockStore.FirstName AS ["��� ������������ �������"],
Shop.ManagerStockStore.Phone AS ["������� ������������ �������"]
FROM Shop.StockStore
INNER JOIN Shop.ManagerStockStore
ON Shop.ManagerStockStore.StockId = Shop.StockStore.StockId
USE ShopDW
GO

--����� ���� ������
SELECT 
Sales.Orders.OrderId AS [Id ������],
Sales.Orders.City AS [����� ��������],
Sales.Client.FirstName AS [��� �������],
Sales.Client.LastName AS [������� �������],
Sales.Client.Phone AS [����� ��������],
Sales.Accessory.AccessoryName AS [���������],
Sales.Accessory.Color AS [����],
Sales.Accessory.TypeOfLink AS [��� ����������],
Sales.Goods.CountAccessory AS [����������]
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

--����� ���� ������ � ������
CREATE FUNCTION Sales.GetSales(@City nvarchar(20))
RETURNS TABLE
AS
RETURN SELECT 
Sales.Orders.OrderId AS [Id ������],
Sales.Client.FirstName AS [��� �������],
Sales.Client.LastName AS [������� �������],
Sales.Client.Phone AS [����� ��������],
Sales.Accessory.AccessoryName AS [���������],
Sales.Accessory.Color AS [����],
Sales.Accessory.TypeOfLink AS [��� ����������],
Sales.Goods.CountAccessory AS [����������]
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

SELECT * FROM Sales.GetSales('�����')
GO

--������, ������������ ���������� � �������� ������������� ���� ����������
CREATE FUNCTION Sales.GetSalesOfTypeAccessory(@TypeAccessory nvarchar(20))
RETURNS TABLE
AS
RETURN SELECT 
Sales.Orders.OrderId AS [Id ������],
Sales.Client.FirstName AS [��� �������],
Sales.Client.LastName AS [������� �������],
Sales.Client.Phone AS [����� ��������],
Sales.Accessory.AccessoryName AS [���������],
Sales.Accessory.Color AS [����],
Sales.Accessory.TypeOfLink AS [��� ����������],
Sales.Goods.CountAccessory AS [����������]
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

SELECT * FROM Sales.GetSalesOfTypeAccessory('�����');
GO

--������, ������������ ���������� � �������� ������������� ���� ����������
CREATE FUNCTION Sales.GetSalesOfNameAccessory(@NameAccessory nvarchar(20))
RETURNS TABLE
AS
RETURN SELECT 
Sales.Orders.OrderId AS [Id ������],
Sales.Client.FirstName AS [��� �������],
Sales.Client.LastName AS [������� �������],
Sales.Client.Phone AS [����� ��������],
Sales.Accessory.AccessoryName AS [���������],
Sales.Accessory.Color AS [����],
Sales.Accessory.TypeOfLink AS [��� ����������],
Sales.Goods.CountAccessory AS [����������]
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

SELECT * FROM Sales.GetSalesOfNameAccessory('�������� M17');
GO
USE ShopDB;
GO

--добавить клиента
CREATE PROC InsertClient 
	@FName nvarchar(20),
	@LName nvarchar(20), 
	@Phone nvarchar(20) 
AS
BEGIN
	INSERT INTO Shop.Client
	VALUES
	(@FName, @LName, @Phone);
	RETURN SCOPE_IDENTITY(); -- id
END
GO

DROP PROC InsertClient
GO;

-- добавить аксессуар
CREATE PROC InsertAccessory
	@AccessName nvarchar(60),
	@Color nvarchar(30), 
	@TypeOfLink nvarchar(30),
	@ProducerId int,
	@AccessTypeId int
AS
BEGIN
	INSERT INTO Shop.Accessory
	VALUES
	(@AccessName, @Color, @TypeOfLink, @ProducerId, @AccessTypeId);
END
GO

--добавить заказ
CREATE PROC InsertOrder
	@ClientId int,
	@City nvarchar(30),
	@Street nvarchar(30),
	@NumberOfStreet nvarchar(10)
AS
BEGIN
	INSERT INTO Shop.Orders
	VALUES
	(@ClientId, @City, @Street, @NumberOfStreet);
	RETURN SCOPE_IDENTITY(); -- id
END
GO

--добавить товар заказа
CREATE PROC InsertGoods
	@OrderId int,
	@AccessoryId int,
	@CountAccessory smallint
AS
BEGIN
	INSERT INTO Shop.Goods
	VALUES
	(@OrderId, @AccessoryId, @CountAccessory);
END
GO
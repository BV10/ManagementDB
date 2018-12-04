USE ShopDB;
GO

--обновить клиента
CREATE PROC UpdateClient 
	@ClientId int,
	@FName nvarchar(20),
	@LName nvarchar(20), 
	@Phone nvarchar(20) 
AS
BEGIN
	UPDATE  Shop.Client
	SET FirstName = @FName, LastName = @LName, Phone = @PHone
	WHERE ClientId = @ClientId
END
GO

-- обновить аксессуар
CREATE PROC UpdateAccessory
	@AccessId int,
	@AccessName nvarchar(60),
	@Color nvarchar(30), 
	@TypeOfLink nvarchar(30)
AS
BEGIN
	UPDATE  Shop.Accessory
	SET AccessoryName =  @AccessName, 
	Color = @Color,
	TypeOfLink = @TypeOfLink
	WHERE Shop.Accessory.AccessoryId = @AccessId
END
GO
USE ShopDB;
GO

--удаление клиента
CREATE PROC DeleteClient
	@ClientId int
AS
BEGIN
DELETE Shop.Client
WHERE Shop.Client.ClientId = @ClientId;
END
GO


--удаление аксессуара
CREATE PROC DeleteAccessory
	@AccessId int
AS
BEGIN
DELETE Shop.Accessory
WHERE Shop.Accessory.AccessoryId = @AccessId;
END
GO
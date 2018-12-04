USE master;
GO
DROP DATABASE  ShopDW;
GO

 /* Создание Хранилища Данных*/
-------------------------------------------------------------------------

CREATE DATABASE ShopDW    
ON							  
(
	NAME = 'ShopDW',            
	FILENAME = 'G:\Studying\4_course\DataBase\CourseWorkDB\DW\ShopDW.mdf', 
	SIZE = 200MB,                    
	MAXSIZE = 1000MB,				
	FILEGROWTH = 100MB				
)
LOG ON						  
( 
	NAME = 'LogShopDW',            
	FILENAME = 'G:\Studying\4_course\DataBase\CourseWorkDB\DW\LogShopDW.ldf', 
	SIZE = 100MB,                        
	MAXSIZE = 500MB,                    
	FILEGROWTH = 50MB                   
)   
COLLATE Cyrillic_General_CI_AS -- Задаем кодировку для базы данных по умолчанию
-------------------------------------------------------------------------

USE ShopDW;
GO

CREATE SCHEMA Sales
GO

--таблица клиентов
CREATE TABLE Sales.Client
(
	ClientId int PRIMARY KEY IDENTITY(1,1) NOT NULL,		
	FirstName nvarchar(20) NOT NULL,
	LastName nvarchar(20) NOT NULL,
	Phone char(10) UNIQUE
)

-- Создание  ограничения на телефон 
ALTER TABLE Sales.Client
ADD CONSTRAINT CN_ClientsPhone
-- Ограничение CHECK
CHECK (Phone LIKE '0[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') 
GO


--таблица заказов
CREATE TABLE Sales.Orders
(
	OrderId int IDENTITY(1,1) NOT NULL
		PRIMARY KEY,				                           	 
	City nvarchar(30) NOT NULL,
	Street nvarchar(30) NOT NULL,
	NumberOfStreet nvarchar(10) NOT NULL
)
GO

CREATE TABLE Sales.Facts
(
	ClientId int NOT NULL
	FOREIGN KEY REFERENCES Sales.Client(ClientId),
	OrderID int NOT NULL
	FOREIGN KEY REFERENCES Sales.Orders(OrderId)
)
GO

--производитель товаров
CREATE TABLE Sales.Producer
(
	ProducerId int IDENTITY(1,1) NOT NULL
		PRIMARY KEY,
	CompanyName nvarchar(20),
	Country nvarchar(20)
)
GO


--тип аксесуара
CREATE TABLE Sales.AccessoryType
(
	AccessoryTypeId int IDENTITY(1,1) NOT NULL
		PRIMARY KEY,
	TypeName nvarchar(20) NOT NULL
)
GO


--аксесуар
CREATE TABLE Sales.Accessory
(
	AccessoryId int IDENTITY(1,1) NOT NULL
		PRIMARY KEY,
	AccessoryName nvarchar(60) NOT NULL,
	Color nvarchar(30) NOT NULL,
	TypeOfLink nvarchar(30) NOT NULL,
	ProducerId int NOT NULL
		FOREIGN KEY REFERENCES Sales.Producer(ProducerId),
	AccessoryTypeId int NOT NULL
		FOREIGN KEY REFERENCES Sales.AccessoryType(AccessoryTypeId)
)
GO


--товар
CREATE TABLE Sales.Goods
(
	OrderId int NOT NULL,
	AccessoryId int NOT NULL
		FOREIGN KEY REFERENCES Sales.Accessory(AccessoryId),
	CountAccessory smallint NOT NULL
)
GO

ALTER TABLE Sales.Goods
ADD CONSTRAINT FK_OrderId
FOREIGN KEY (OrderId) REFERENCES Sales.Orders(OrderId)
ON DELETE CASCADE ON UPDATE CASCADE
GO


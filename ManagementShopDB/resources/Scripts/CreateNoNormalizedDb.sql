-- Удаляем БазуДанных
USE master;
GO
DROP DATABASE  NoNoralizedShopDB;
GO

 /* Создание Базы Данных*/
-------------------------------------------------------------------------

CREATE DATABASE NoNoralizedShopDB    
ON							  
(
	NAME = 'ShopDB',            
	FILENAME = 'G:\Studying\4_course\DataBase\CourseWorkDB\DB\NoNoralizedShopDB.mdf', 
	SIZE = 200MB,                    
	MAXSIZE = 1000MB,				
	FILEGROWTH = 100MB				
)
LOG ON						  
( 
	NAME = 'LogShopDB',            
	FILENAME = 'G:\Studying\4_course\DataBase\CourseWorkDB\DB\NoNoralizedLogShopDB.ldf', 
	SIZE = 100MB,                        
	MAXSIZE = 500MB,                    
	FILEGROWTH = 50MB                   
)   
COLLATE Cyrillic_General_CI_AS -- Задаем кодировку для базы данных по умолчанию
-------------------------------------------------------------------------


-- Используем базу данных интернет-магазина
USE NoNoralizedShopDB 
GO

-------------------------------------------------------------------------
						/* Создание таблиц */
-------------------------------------------------------------------------
CREATE SCHEMA Shop
GO

--------------------не нормализованная часть
CREATE TABLE Shop.PostAddress
(
	PostAddressId int IDENTITY(1,1) NOT NULL 
		PRIMARY KEY,
	City nvarchar(30) NOT NULL,
	Street nvarchar(30) NOT NULL,
	NumberOfStreet nvarchar(10) NOT NULL
)
GO


ALTER TABLE Shop.Orders 
ADD					 
PostAddressId int NOT NULL 
	FOREIGN KEY REFERENCES Shop.PostAddress(PostAddressId)
GO
--------------------

--таблица клиентов
CREATE TABLE Shop.Client
(
	ClientId int IDENTITY(1,1) NOT NULL,		
	FirstName nvarchar(20) NOT NULL,
	LastName nvarchar(20) NOT NULL,
	Phone char(10) UNIQUE 	
)

-- Создание  ограничения на телефон 
ALTER TABLE Shop.Client
ADD CONSTRAINT CN_ClientsPhone
-- Ограничение CHECK
CHECK (Phone LIKE '0[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') 
GO

-- Изменяем таблицу	Client задав ограничение первичного ключа на столбце ClientId
ALTER TABLE Shop.Client
ADD CONSTRAINT PK_Client
PRIMARY KEY (ClientId)
GO

--таблица заказов
CREATE TABLE Shop.Orders
(
	OrderId int IDENTITY(1,1) NOT NULL
		PRIMARY KEY,
	ClientId int NOT NULL FOREIGN KEY REFERENCES Shop.Client(ClientId),				                           	 
	City nvarchar(30) NOT NULL,
	Street nvarchar(30) NOT NULL,
	NumberOfStreet nvarchar(10) NOT NULL
)



--таблица карточки для виплат клиента
CREATE TABLE Shop.CreditCard
(
	CCNumb bigint NOT NULL PRIMARY KEY,
	FirstName nvarchar(20) NOT NULL,
	LastName nvarchar(20) NOT NULL,
	ExpiryDate Date NOT NULL,
	ClientId int NOT NULL FOREIGN KEY REFERENCES Shop.Client(ClientId),
	OrderId int NULL FOREIGN KEY REFERENCES Shop.Orders(OrderId)
)

--производитель товаров
CREATE TABLE Shop.Producer
(
	ProducerId int IDENTITY(1,1) NOT NULL
		PRIMARY KEY,
	CompanyName nvarchar(20),
	Country nvarchar(20)
)
GO

--тип аксесуара
CREATE TABLE Shop.AccessoryType
(
	AccessoryTypeId int IDENTITY(1,1) NOT NULL
		PRIMARY KEY,
	TypeName nvarchar(20) NOT NULL
)

--склады для аксесуара
CREATE TABLE Shop.StockStore
(
	StockId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	City nvarchar(30) NOT NULL,
	AddressOfStock nvarchar(30) NOT NULL
)

--аксесуар
CREATE TABLE Shop.Accessory
(
	AccessoryId int IDENTITY(1,1) NOT NULL
		PRIMARY KEY,
	AccessoryName nvarchar(20) NOT NULL,
	Color nvarchar(20) NOT NULL,
	TypeOfLink nvarchar(20) NOT NULL,
	ProducerId int NOT NULL
		FOREIGN KEY REFERENCES Shop.Producer(ProducerId),
	AccessoryTypeId int NOT NULL
		FOREIGN KEY REFERENCES Shop.AccessoryType(AccessoryTypeId)
)
GO

--многие ко многим StockStore - Accessory
CREATE TABLE Shop.StockStoreAccessory
(
	StockId int NOT NULL
		FOREIGN KEY REFERENCES Shop.StockStore(StockId),
	AccessoryId int NOT NULL
		FOREIGN KEY REFERENCES Shop.Accessory(AccessoryId),
	PRIMARY KEY(StockId, AccessoryId)
)
GO

--менеджер склада
CREATE TABLE Shop.ManagerStockStore
(
	ManagerId int IDENTITY(1,1) NOT NULL PRIMARY KEY,	
	FirstName nvarchar(20) NOT NULL,
	LastName nvarchar(20) NOT NULL,
	Phone char(10) UNIQUE,
	StockId int NOT NULL
)

-- Создание  ограничения на телефон 
ALTER TABLE Shop.ManagerStockStore
ADD CONSTRAINT CN_ManagerPhone
-- Ограничение CHECK
CHECK (Phone LIKE '0[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') 
GO

ALTER TABLE Shop.ManagerStockStore
ADD CONSTRAINT FK_StockStoreId
FOREIGN KEY (StockId) REFERENCES Shop.StockStore(StockId)
GO

--товар
CREATE TABLE Shop.Goods
(
	OrderId int NOT NULL,
	AccessoryId int NOT NULL
		FOREIGN KEY REFERENCES Shop.Accessory(AccessoryId),
	CountAccessory smallint NOT NULL
)
GO

ALTER TABLE Shop.Goods
ADD CONSTRAINT FK_OrderId
FOREIGN KEY (OrderId) REFERENCES Shop.Orders(OrderId)
GO
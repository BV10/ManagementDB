-- ������� ����������
USE master;
GO
DROP DATABASE  NoNoralizedShopDB;
GO

 /* �������� ���� ������*/
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
COLLATE Cyrillic_General_CI_AS -- ������ ��������� ��� ���� ������ �� ���������
-------------------------------------------------------------------------


-- ���������� ���� ������ ��������-��������
USE NoNoralizedShopDB 
GO

-------------------------------------------------------------------------
						/* �������� ������ */
-------------------------------------------------------------------------
CREATE SCHEMA Shop
GO

--------------------�� ��������������� �����
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

--������� ��������
CREATE TABLE Shop.Client
(
	ClientId int IDENTITY(1,1) NOT NULL,		
	FirstName nvarchar(20) NOT NULL,
	LastName nvarchar(20) NOT NULL,
	Phone char(10) UNIQUE 	
)

-- ��������  ����������� �� ������� 
ALTER TABLE Shop.Client
ADD CONSTRAINT CN_ClientsPhone
-- ����������� CHECK
CHECK (Phone LIKE '0[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') 
GO

-- �������� �������	Client ����� ����������� ���������� ����� �� ������� ClientId
ALTER TABLE Shop.Client
ADD CONSTRAINT PK_Client
PRIMARY KEY (ClientId)
GO

--������� �������
CREATE TABLE Shop.Orders
(
	OrderId int IDENTITY(1,1) NOT NULL
		PRIMARY KEY,
	ClientId int NOT NULL FOREIGN KEY REFERENCES Shop.Client(ClientId),				                           	 
	City nvarchar(30) NOT NULL,
	Street nvarchar(30) NOT NULL,
	NumberOfStreet nvarchar(10) NOT NULL
)



--������� �������� ��� ������ �������
CREATE TABLE Shop.CreditCard
(
	CCNumb bigint NOT NULL PRIMARY KEY,
	FirstName nvarchar(20) NOT NULL,
	LastName nvarchar(20) NOT NULL,
	ExpiryDate Date NOT NULL,
	ClientId int NOT NULL FOREIGN KEY REFERENCES Shop.Client(ClientId),
	OrderId int NULL FOREIGN KEY REFERENCES Shop.Orders(OrderId)
)

--������������� �������
CREATE TABLE Shop.Producer
(
	ProducerId int IDENTITY(1,1) NOT NULL
		PRIMARY KEY,
	CompanyName nvarchar(20),
	Country nvarchar(20)
)
GO

--��� ���������
CREATE TABLE Shop.AccessoryType
(
	AccessoryTypeId int IDENTITY(1,1) NOT NULL
		PRIMARY KEY,
	TypeName nvarchar(20) NOT NULL
)

--������ ��� ���������
CREATE TABLE Shop.StockStore
(
	StockId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	City nvarchar(30) NOT NULL,
	AddressOfStock nvarchar(30) NOT NULL
)

--��������
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

--������ �� ������ StockStore - Accessory
CREATE TABLE Shop.StockStoreAccessory
(
	StockId int NOT NULL
		FOREIGN KEY REFERENCES Shop.StockStore(StockId),
	AccessoryId int NOT NULL
		FOREIGN KEY REFERENCES Shop.Accessory(AccessoryId),
	PRIMARY KEY(StockId, AccessoryId)
)
GO

--�������� ������
CREATE TABLE Shop.ManagerStockStore
(
	ManagerId int IDENTITY(1,1) NOT NULL PRIMARY KEY,	
	FirstName nvarchar(20) NOT NULL,
	LastName nvarchar(20) NOT NULL,
	Phone char(10) UNIQUE,
	StockId int NOT NULL
)

-- ��������  ����������� �� ������� 
ALTER TABLE Shop.ManagerStockStore
ADD CONSTRAINT CN_ManagerPhone
-- ����������� CHECK
CHECK (Phone LIKE '0[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') 
GO

ALTER TABLE Shop.ManagerStockStore
ADD CONSTRAINT FK_StockStoreId
FOREIGN KEY (StockId) REFERENCES Shop.StockStore(StockId)
GO

--�����
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
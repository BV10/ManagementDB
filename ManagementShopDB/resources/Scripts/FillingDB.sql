						/* ���������� ������*/
-------------------------------------------------------------------------

Use ShopDB;
GO

-- ��������� ��������������
INSERT INTO Shop.Producer
VALUES
('Sony', '������'),
('Panasonic', '������'),
('Dell', '���'),
('Xiaomi', '�����'),
('Samsung', '����� �����'),
('LG', '����� �����'),
('Meizu', '�����'),
('Lenovo', '�����');
GO

-- ��������� ���� �����������
INSERT INTO Shop.AccessoryType
VALUES
('��������'),
('�����'),
('�������� ������'),
('����������'),
('������ ���������');
GO

-- ��������� ���������
INSERT INTO Shop.Accessory
VALUES
('Bluetooth-��������� K213', '����������', 'Bluetooth', 8, 5),
('Bluetooth-��������� LE12', '�����', 'Bluetooth', 2, 5),
('Bluetooth-��������� ST1243', '������', 'Bluetooth', 1, 5),
('�������� ������ Meizu M2', '����������', '����', 7, 3),
('�������� ������ Xiaomi Redmi Note5', '�����������', '����', 4, 3),
('�������� ������ Lenove R3', '����������', '����', 8, 3),
('����� KL1312', '�������', 'AUX', 3, 2),
('����� LS1452', '������', 'AUX', 4, 2),
('����� LT1422', '����������', 'AUX', 6, 2),
('�������� LN142', '�������', 'AUX', 8, 1),
('�������� M17', '�����', 'Bluetooth', 7, 1),
('�������� D27A', '�������', 'AUX', 1, 1),
('���������� ��������� Samsung DS4100', '�������', 'USB', 5, 4),
('���������� ��������� Logitech K120', '������', 'USB', 3, 4),
('���������� ��������� Real-El 8700 Backlit', '�����-�����', 'USB', 1, 4);
GO

--��������� ������ ��� ����������
INSERT INTO Shop.StockStore
VALUES
('�����', '���� 10'),
('�����', '������� 19'),
('�����', '�������� 35'),
('����', '����� ������ 13'),
('�������', '��������� 33')
GO

--��������� ��������� �� ��������
INSERT INTO Shop.StockStoreAccessory
VALUES
	(1, 1),	(1, 2),	(1, 3),	(1, 4),	(1, 5),	(1, 6),	(1, 7),	(1, 8),	(1, 9),	(1, 10),	(1, 11),	(1, 12),	(1, 13),	(1, 14),	(1, 15),	
	(2, 1),	(2, 2),	(2, 3),	(2, 4),	(2, 5),	(2, 6),	(2, 7),	(2, 8),	(2, 9),	(2, 10),	(2, 11),	(2, 12),	(2, 13),	(2, 14),	(2, 15),	
	(3, 1),	(3, 2),	(3, 3),	(3, 4),	(3, 5),	(3, 6),	(3, 7),	(3, 8),	(3, 9),	(3, 10),	(3, 11),	(3, 12),	(3, 13),	(3, 14),	(3, 15),
	(4, 1),	(4, 2),	(4, 3),	(4, 4),	(4, 5),	(4, 6),	(4, 7),	(4, 8),	(4, 9),	(4, 10),	(4, 11),	(4, 12),	(4, 13),	(4, 14),	(4, 15),	
	(5, 1),	(5, 2),	(5, 3),	(5, 4),	(5, 5),	(5, 6),	(5, 7),	(5, 8),	(5, 9),	(5, 10),	(5, 11),	(5, 12),	(5, 13),	(5, 14),	(5, 15);
	GO

--��������� ���������� �� ������
INSERT INTO Shop.ManagerStockStore
VALUES
('������', '�������', '0990775851', 1),
('����', '���',  '0990775852', 2),
('����', '�������', '0990775853', 3),
('����', '�������', '0990775858', 4);
GO


-- ��������� ��������
INSERT INTO Shop.Client
VALUES
('������', '������', '0932345512'),
('����', '��������', '0953352112'),
('����', '��������', '0951235311'),
('������', '�����������', '0973218592'),
('����', '�����', '0973218532'),
('����', '�������', '0983158992'),
('���������', '�������', '0663118592'),
('�������', '�������', '0993214592'),
('�����', '������', '0991218592'),
('����', '�������', '0931518592'),
('����', '�����', '0983258592'),
('������', '�������', '0961628592'),
('�������', '������������', '0983518592'),
('������', '��������', '0973858592');
GO

--��������� ������
INSERT INTO Shop.Orders
VALUES
(1, '�����','����','17'),
(2, '�����','�����','18'),
(3, '�����','�����','19'),
(4, '�����','����','23'),
(5, '����','����� ��������','64'),
(6, '����','������','13'),
(7, '����','������ �������','76'),
(8, '����','������','43'),
(9, '����','������','21'),
(10, '����','��������','85'),
(11, '����','�����','12'),
(12, '�������','���������','62'),
(13, '�������','�������','27'),
(14, '�������','��������','93');
GO

--��������� ��������� ��������
INSERT INTO Shop.CreditCard
VALUES
(1234634123551234,'������', '�����������', '2019-01-01', 5, NULL),
(1234684121551237, '����', '�����', '2020-01-01', 6, NULL);

-- ��������� ������
INSERT INTO Shop.Goods
VALUES
(1, 3, 3),
(2, 4, 1),
(3, 7, 2),
(4, 12, 4),
(5, 11, 2),
(6, 2, 6),
(7, 6, 3),
(8, 3, 2),
(9, 9, 1),
(10, 7, 7),
(11, 12, 4),
(12, 14, 3),
(13, 4, 6),
(14, 6, 1);
GO
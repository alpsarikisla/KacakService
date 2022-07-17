CREATE TABLE Customers
(
	ID int IDENTITY(1,1),
	Name nvarchar(75),
	Surname nvarchar(75),
	PhoneNumber nvarchar(11),
	Status bit,
	CONSTRAINT pk_Customer PRIMARY KEY(ID)
)
GO
INSERT INTO Customers(Name, Surname, PhoneNumber, Status) VALUES('Mehpare','Keteci','5301234578',1)
INSERT INTO Customers(Name, Surname, PhoneNumber, Status) VALUES('Abbas','Þahabettinoðlu','5358968745',1)
INSERT INTO Customers(Name, Surname, PhoneNumber, Status) VALUES('Volkan','Üstünel','5495068958',1)
GO
CREATE TABLE CustomerCards
(
	ID int IDENTITY(1,1),
	Customer_ID int,
	CardNumber nvarchar(16),
	expMonth nvarchar(2),
	expYear	nvarchar(4),
	CVVNumber nvarchar(3),
	Balance decimal,
	Status bit,
	CONSTRAINT pk_CustomerCard PRIMARY KEY(ID),
	CONSTRAINT fk_customerCardCustomer FOREIGN KEY(Customer_ID) REFERENCES Customers(ID)
)
GO
INSERT INTO CustomerCards(Customer_ID,CardNumber,expMonth,expYear,CVVNumber,Balance,Status) VALUES(1, '1597856412547854', '12', '25', '097', 12000, 1)
INSERT INTO CustomerCards(Customer_ID,CardNumber,expMonth,expYear,CVVNumber,Balance,Status) VALUES(1, '9856325874587456', '05', '24', '187', 100, 1)
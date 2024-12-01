-- Create the WorkCafeDB database
CREATE DATABASE CafePortalDB;
GO

USE CafePortalDB;
GO


-- Create table for cafés
CREATE TABLE Cafes (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL CHECK (LEN(Name) BETWEEN 6 AND 100),
    Description NVARCHAR(256) NOT NULL,
    Logo VARBINARY(MAX) NULL,
    Location NVARCHAR(100) NOT NULL
);

-- Create table for employees
CREATE TABLE Employees (
    Id CHAR(10) PRIMARY KEY CHECK (Id LIKE 'UI%'),
	CafeId UNIQUEIDENTIFIER,
    Name NVARCHAR(100) NOT NULL CHECK (LEN(Name) BETWEEN 2 AND 100),
    EmailAddress NVARCHAR(256) NOT NULL CHECK (EmailAddress LIKE '%@%.%'),
    PhoneNumber CHAR(8) NOT NULL CHECK (PhoneNumber LIKE '8%' OR PhoneNumber LIKE '9%'),
    Gender NVARCHAR(10) NOT NULL CHECK (Gender IN ('Male', 'Female')),
	StartDate DATE NOT NULL,
	CONSTRAINT FK_Employee_Cafe FOREIGN KEY (CafeId) REFERENCES Cafes(Id),
	CONSTRAINT UQ_Employee_Cafe UNIQUE (CafeId)
);


-- cafes data
INSERT INTO Cafes (Name, Description, Location)
VALUES 
('The Coffee Spot', 'Cozy place for great coffee', 'TiongBahru'),
('Cafe Haven', 'Relaxing atmosphere with gourmet treats', 'Clementi'),
('Starbucks', 'Perfect spot for caffeine lovers', 'LakeSide');

--  employees data
INSERT INTO Employees (Id, CafeId, Name, EmailAddress, PhoneNumber, Gender, StartDate)
VALUES 
('UI1234567', (SELECT Id FROM Cafes WHERE Name = 'The Coffee Spot'), 'Snow White', 'snow@gmail.com', '91234567', 'Female', '2023-01-15'),
('UI2345678', (SELECT Id FROM Cafes WHERE Name = 'Cafe Haven'),'Hnin Phyu', 'hnin@gmail.com', '92345678', 'Female', '2023-06-01'),
('UI3456789', (SELECT Id FROM Cafes WHERE Name = 'Starbucks'), 'Maw Kon', 'win@gmai.com', '93456789', 'Male', '2024-06-20');

GO

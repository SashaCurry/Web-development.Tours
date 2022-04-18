USE Master;
IF DB_ID(N'ToursDB') IS NOT NULL DROP DATABASE [ToursDB];
GO
CREATE DATABASE [ToursDB] ON PRIMARY
( NAME = N'ToursDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ToursDB.mdf' , SIZE = 51200KB , FILEGROWTH = 10240KB )
LOG ON
( NAME = N'ToursDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ToursDB_log.ldf' , SIZE = 10240KB , FILEGROWTH = 10%)
COLLATE Cyrillic_General_100_CI_AI 
GO
ALTER DATABASE ToursDB SET RECOVERY SIMPLE WITH NO_WAIT; 
GO
ALTER DATABASE ToursDB SET AUTO_SHRINK OFF 
GO

CREATE TABLE [Countries] (
  [Id] int PRIMARY KEY NOT NULL,
  [Name] nvarchar(50) NOT NULL
)
GO

CREATE TABLE [Tours] (
  [Id] int PRIMARY KEY NOT NULL,
  [Price] money NOT NULL,
  [Days] int NOT NULL,
  [City] nvarchar(50) NOT NULL,
  [CountriesID] int NOT NULL
)
GO

ALTER TABLE [Tours]
	ADD FOREIGN KEY ([CountriesID]) REFERENCES [Countries] ([Id])
GO

INSERT INTO Tours VALUES
	(1, 62000, 7, 'Гёйнюк', 1),
	(2, 72000, 4, 'Сиде', 1),
	(3, 96000, 9, 'Махмутлар', 1),
	(4, 63000, 7, 'Пхукет', 2),
	(5, 86000, 14, 'Краби', 2),
	(6, 89000, 15, 'Краби', 2),
	(7, 77000, 9, 'Джерба', 3),
	(8, 91000, 21, 'Хаммамет', 3),
	(9, 56000, 7, 'Джерба', 3);
GO

INSERT INTO Countries VALUES
	(1, 'Турция'),
	(2, 'Таиланд'),
	(3, 'Тунис');
GO
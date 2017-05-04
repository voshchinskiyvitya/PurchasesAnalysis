--========================================================
-- Author: Victor Voshchinskiy
-- Date: 04/05/2017
-- Description: script for db tables initialization.
--========================================================

DECLARE @dbname nvarchar(128)
SET @dbname = N'Purchases'

IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = @dbname OR name = @dbname)))
BEGIN
	EXEC('
	USE Purchases

	BEGIN TRY
		BEGIN TRANSACTION
	
		-- Product table
		CREATE TABLE [Product](
			[ID] int IDENTITY(1,1) NOT NULL,
			[Name] VARCHAR(100) NOT NULL,
			CONSTRAINT Product_PK PRIMARY KEY ([ID]),
			CONSTRAINT ProductName_UniqueConstraint UNIQUE([Name])
		);
		
		-- Dates table
		CREATE TABLE [Dates](
			[ID] int IDENTITY(1,1) NOT NULL,
			[Date] DATETIME NOT NULL,
			[Year] int NOT NULL,
			[Month] int NOT NULL,
			[DayOfMonth] int NOT NULL,
			[DayOfWeek] int NOT NULL,
			CONSTRAINT Dates_PK PRIMARY KEY ([ID]),
			CONSTRAINT DatesDate_UniqueConstraint UNIQUE([Date]),
			INDEX DatesYear_NCIndex NONCLUSTERED([Year]),
			INDEX DatesMonth_NCIndex NONCLUSTERED([Month]),
			INDEX DatesDayOfMonth_NCIndex NONCLUSTERED([DayOfMonth]),
			INDEX DatesDayOfWeek_NCIndex NONCLUSTERED([DayOfWeek])
		);
		
		-- Types table
		CREATE TABLE [Type](
			[ID] int IDENTITY(1,1) NOT NULL,
			[Name] VARCHAR(100) NOT NULL,
			CONSTRAINT Type_PK PRIMARY KEY ([ID]),
			CONSTRAINT TypeName_UniqueConstraint UNIQUE([Name])
		);
		
		-- Purchases table
		CREATE TABLE [Purchases](
			[ID] int IDENTITY(1,1) NOT NULL,
			[Type] int NOT NULL,
			[Product] int NOT NULL,
			[Date] int NOT NULL,
			[Price] decimal(7,2) NOT NULL,
			[Quantity] int NOT NULL,
			CONSTRAINT Purchases_PK PRIMARY KEY ([ID]),
			CONSTRAINT Purchases_FK_Type FOREIGN KEY ([Type]) REFERENCES [Type]([ID]),
			CONSTRAINT Purchases_FK_Product FOREIGN KEY ([Product]) REFERENCES [Product]([ID]),
			CONSTRAINT Purchases_FK_Date FOREIGN KEY ([Date]) REFERENCES [Dates]([ID])
		);
	
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		DECLARE @ErrorMessage NVARCHAR(max), @ErrorSeverity INT, @ErrorState INT;
		SELECT @ErrorMessage = ERROR_MESSAGE() + '' Line '' + cast(ERROR_LINE() as nvarchar(5)) + '' '', @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
		ROLLBACK TRANSACTION;
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState )
	END CATCH
	');
END
ELSE
BEGIN
	RAISERROR ('Purchases DB doesn"t exist. Please run CreateDB.sql script.', 16, 1)
END
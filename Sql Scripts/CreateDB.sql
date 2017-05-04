--========================================================
-- Author: Victor Voshchinskiy
-- Date: 04/05/2017
-- Description: script for db initialization.
--========================================================

USE master;  
GO 
 
DECLARE @dbname nvarchar(128)
SET @dbname = N'Purchases'

IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = @dbname OR name = @dbname)))
BEGIN
	DROP DATABASE Purchases
END

CREATE DATABASE Purchases  
ON   
( 
	NAME = Purchases_dat,  
    FILENAME = 'C:\Projects\PurchasesAnalysis\PurchasesDat.mdf',  
    SIZE = 10,  
    FILEGROWTH = 5 
)  
LOG ON  
( 
	NAME = Purchases_log,  
    FILENAME = 'C:\Projects\PurchasesAnalysis\Purchaseslog.ldf',  
    SIZE = 5MB,
    FILEGROWTH = 5MB 
);  
GO
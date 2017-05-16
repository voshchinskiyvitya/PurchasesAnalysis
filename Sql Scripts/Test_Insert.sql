USE [Purchases]

EXECUTE dbo.Create_Purchase
	@date = '2017-05-05 00:00:00', 
	@name = '«убна паста', 
	@type = 'Watsons', 
	@price = 30, 
	@quantity = 2 

Select * from [dbo].[Dates]
Select * from [dbo].[Product]
Select * from [dbo].[Type]
Select * from [dbo].[Purchases]
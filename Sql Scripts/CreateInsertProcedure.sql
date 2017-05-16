--========================================================
-- Author: Victor Voshchinskiy
-- Date: 16/05/2017
-- Description: script for insert procedure creation
--========================================================

CREATE PROCEDURE Create_Purchase 
	@date DATETIME, 
	@name VARCHAR(100), 
	@type VARCHAR(100), 
	@price INT, 
	@quantity INT 
AS
BEGIN

	DECLARE @DateID INT = (SELECT TOP 1 ID FROM [dbo].[Dates] WHERE [Date] = @date);
	DECLARE @ProductID INT = (SELECT TOP 1 ID FROM [dbo].[Product] WHERE [Name] = @name);
	DECLARE @TypeID INT = (SELECT TOP 1 ID FROM [dbo].[Type] WHERE [Name] = @type);

	BEGIN TRANSACTION

	BEGIN TRY

		IF(@DateID IS NULL)
		BEGIN

			DECLARE @DateYear INT = YEAR(@date);
			DECLARE @DateMonth INT = MONTH(@date);
			DECLARE @DateDayOfMonth INT = DAY(@date);
			DECLARE @DateDayOfWeek INT = DATEPART(dw, @date);

			INSERT INTO [dbo].[Dates]
			([Date] ,[Year] ,[Month] ,[DayOfMonth] ,[DayOfWeek])
			VALUES
			(@date, @DateYear, @DateMonth, @DateDayOfMonth, @DateDayOfWeek)

			SELECT @DateID = SCOPE_IDENTITY();

		END

		IF(@ProductID IS NULL)
		BEGIN

			INSERT INTO [dbo].[Product]
			([Name])
			VALUES
			(@name)

			SELECT @ProductID = SCOPE_IDENTITY();

		END

		IF(@TypeID IS NULL)
		BEGIN

			INSERT INTO [dbo].[Type]
			([Name])
			VALUES
			(@type)

			SELECT @TypeID = SCOPE_IDENTITY();

		END

		INSERT INTO [dbo].[Purchases]
		([Type], [Product], [Date], [Price], [Quantity])
		VALUES
		(@TypeID, @ProductID, @DateID, @price, @quantity)

		COMMIT TRANSACTION
		RETURN(0)

	END TRY
	BEGIN CATCH
		DECLARE @ErrorMessage NVARCHAR(max), @ErrorSeverity INT, @ErrorState INT;
		SELECT @ErrorMessage = ERROR_MESSAGE() + ' Line ' + cast(ERROR_LINE() as nvarchar(5)) + ' ', @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
		ROLLBACK TRANSACTION;
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState )
		RETURN(3)
	END CATCH

END
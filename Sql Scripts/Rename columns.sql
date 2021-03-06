--========================================================
-- Author: Victor Voshchinskiy
-- Date: 18/10/2017
-- Description: change column names.
--========================================================

BEGIN
	BEGIN TRANSACTION

	BEGIN TRY

		-- Rename Type column
		EXECUTE SP_RENAME 'dbo.Purchases.Type', 'TypeId', 'COLUMN'
		
		-- Rename Date column
		EXECUTE SP_RENAME 'dbo.Purchases.Date', 'DateId', 'COLUMN'
		
		-- Rename Product column
		EXECUTE SP_RENAME 'dbo.Purchases.Product', 'ProductId', 'COLUMN'

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		DECLARE @ErrorMessage NVARCHAR(max), @ErrorSeverity INT, @ErrorState INT;
		SELECT @ErrorMessage = ERROR_MESSAGE() + ' Line ' + cast(ERROR_LINE() as nvarchar(5)) + ' ', @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
		ROLLBACK TRANSACTION;
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState )
	END CATCH
END
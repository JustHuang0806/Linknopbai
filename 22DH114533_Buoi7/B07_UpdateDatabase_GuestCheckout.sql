USE [DBSportStore];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;

IF OBJECT_ID(N'dbo.Customer', N'U') IS NULL OR OBJECT_ID(N'dbo.OrderPro', N'U') IS NULL
    THROW 51000, 'Expected dbo.Customer and dbo.OrderPro tables were not found. No changes were made.', 1;
IF COL_LENGTH(N'dbo.Customer', N'UserID') IS NULL OR COL_LENGTH(N'dbo.Customer', N'Email') IS NULL
    THROW 51001, 'Expected Customer.UserID and Customer.Email columns were not found. No changes were made.', 1;
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.Customer') AND name = N'UserID' AND is_nullable = 0)
    THROW 51002, 'Customer.UserID must allow NULL before guest checkout. No changes were made.', 1;

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @sql nvarchar(max) = N'';

    -- Remove single-column uniqueness on Email so repeat guest checkouts can reuse an address.
    SELECT @sql = @sql + CASE WHEN i.is_unique_constraint = 1
        THEN N'ALTER TABLE dbo.Customer DROP CONSTRAINT ' + QUOTENAME(i.name) + N';'
        ELSE N'DROP INDEX ' + QUOTENAME(i.name) + N' ON dbo.Customer;'
        END
    FROM sys.indexes i
    WHERE i.object_id = OBJECT_ID(N'dbo.Customer')
      AND i.is_unique = 1 AND i.is_primary_key = 0
      AND (SELECT COUNT(*) FROM sys.index_columns ic WHERE ic.object_id=i.object_id AND ic.index_id=i.index_id AND ic.key_ordinal > 0) = 1
      AND EXISTS (SELECT 1 FROM sys.index_columns ic JOIN sys.columns c ON c.object_id=ic.object_id AND c.column_id=ic.column_id
                  WHERE ic.object_id=i.object_id AND ic.index_id=i.index_id AND ic.key_ordinal > 0 AND c.name=N'Email');
    IF @sql <> N'' EXEC sys.sp_executesql @sql;

    -- Replace any single-column UserID uniqueness with a filtered index that allows many NULL guests.
    SET @sql = N'';
    SELECT @sql = @sql + CASE WHEN i.is_unique_constraint = 1
        THEN N'ALTER TABLE dbo.Customer DROP CONSTRAINT ' + QUOTENAME(i.name) + N';'
        ELSE N'DROP INDEX ' + QUOTENAME(i.name) + N' ON dbo.Customer;'
        END
    FROM sys.indexes i
    WHERE i.object_id = OBJECT_ID(N'dbo.Customer')
      AND i.is_unique = 1 AND i.is_primary_key = 0
      AND (SELECT COUNT(*) FROM sys.index_columns ic WHERE ic.object_id=i.object_id AND ic.index_id=i.index_id AND ic.key_ordinal > 0) = 1
      AND EXISTS (SELECT 1 FROM sys.index_columns ic JOIN sys.columns c ON c.object_id=ic.object_id AND c.column_id=ic.column_id
                  WHERE ic.object_id=i.object_id AND ic.index_id=i.index_id AND ic.key_ordinal > 0 AND c.name=N'UserID');
    IF @sql <> N'' EXEC sys.sp_executesql @sql;

    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.Customer') AND name=N'UX_Customer_UserID')
        CREATE UNIQUE INDEX UX_Customer_UserID ON dbo.Customer(UserID) WHERE UserID IS NOT NULL;

    IF COL_LENGTH(N'dbo.OrderPro', N'RecipientName') IS NULL
        ALTER TABLE dbo.OrderPro ADD RecipientName nvarchar(100) NULL;
    IF COL_LENGTH(N'dbo.OrderPro', N'ShippingPhone') IS NULL
        ALTER TABLE dbo.OrderPro ADD ShippingPhone nvarchar(20) NULL;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

-- Post-change checks: run the script again safely; these should stay true.
SELECT c.name, c.is_nullable, TYPE_NAME(c.user_type_id) AS data_type, c.max_length
FROM sys.columns c
WHERE c.object_id = OBJECT_ID(N'dbo.Customer') AND c.name IN (N'Email', N'UserID')
   OR c.object_id = OBJECT_ID(N'dbo.OrderPro') AND c.name IN (N'RecipientName', N'ShippingPhone');
SELECT name, is_unique, has_filter, filter_definition
FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.Customer') AND name=N'UX_Customer_UserID';

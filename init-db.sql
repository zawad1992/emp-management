IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'HRMWeb')
BEGIN
    CREATE DATABASE HRMWeb;
END
GO

USE HRMWeb;
GO
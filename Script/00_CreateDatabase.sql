IF NOT EXISTS(SELECT name FROM master.dbo.sysdatabases WHERE name = N'{{DBNAME}}')
BEGIN
CREATE DATABASE [{{DBNAME}}]
END
GO

-- Create the KanbanDB database if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'KanbanDB')
BEGIN
    CREATE DATABASE KanbanDB;
END
GO

USE KanbanDB;
GO

-- Create Tasks table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tasks]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Tasks] (
        [Id] UNIQUEIDENTIFIER PRIMARY KEY,
        [Title] NVARCHAR(200) NOT NULL,
        [Description] NVARCHAR(1000) NULL,
        [Status] INT NOT NULL,
        [IsArchived] BIT NOT NULL DEFAULT 0,
        [CreatedAt] DATETIME2 NOT NULL,
        [UpdatedAt] DATETIME2 NOT NULL
    );
END
GO

-- Create Events table for Event Sourcing if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Events]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Events] (
        [Id] UNIQUEIDENTIFIER PRIMARY KEY,
        [AggregateId] UNIQUEIDENTIFIER NOT NULL,
        [Type] NVARCHAR(100) NOT NULL,
        [Data] NVARCHAR(MAX) NOT NULL,
        [Timestamp] DATETIME2 NOT NULL,
        INDEX [IX_Events_AggregateId] NONCLUSTERED ([AggregateId])
    );
END
GO

USE PNSMS;


GO
/*
The column Url on table [dbo].[Institutes] must be changed from NULL to NOT NULL. If the table contains data, the ALTER script may not work. To avoid this issue, you must add values to this column for all rows or mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.

The type for column Url in table [dbo].[Institutes] is currently  NVARCHAR (512) NULL but is being changed to  NVARCHAR (128) NOT NULL. Data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Institutes])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[IX_Institutes]...';


GO
ALTER TABLE [dbo].[Institutes] DROP CONSTRAINT [IX_Institutes];


GO
PRINT N'Altering [dbo].[Institutes]...';


GO
ALTER TABLE [dbo].[Institutes] ALTER COLUMN [Url] NVARCHAR (128) NOT NULL;


GO
PRINT N'Creating [dbo].[IX_Institutes]...';


GO
ALTER TABLE [dbo].[Institutes]
    ADD CONSTRAINT [IX_Institutes] UNIQUE NONCLUSTERED ([Url] ASC);


GO
update dbo._Migration set LastUpdate='0012'
PRINT N'Update complete.';


GO

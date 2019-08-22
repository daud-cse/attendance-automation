
USE PNSMS;


GO
PRINT N'Altering [dbo].[Designations]...';


GO
ALTER TABLE [dbo].[Designations]
    ADD [Ordering] INT NULL;


GO
PRINT N'Altering [dbo].[Institutes]...';


GO
ALTER TABLE [dbo].[Institutes]
    ADD [HistoryText]        VARCHAR (MAX) NULL,
        [InfractructureText] VARCHAR (MAX) NULL,
        [MasterPlanText]     VARCHAR (MAX) NULL,
		[UsefulLinkText]     VARCHAR (MAX) NULL;


GO
update dbo._Migration set LastUpdate='0018'
PRINT N'Update complete.';


GO

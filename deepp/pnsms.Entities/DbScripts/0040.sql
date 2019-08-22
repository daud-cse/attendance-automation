
USE PNSMS;


GO
/*
The column [dbo].[ContentDetails].[Caption] is being dropped, data loss could occur.

The column [dbo].[ContentDetails].[Description] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[ContentDetails])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Altering [dbo].[ContentDetails]...';


GO
ALTER TABLE [dbo].[ContentDetails] DROP COLUMN [Caption], COLUMN [Description];


GO
PRINT N'Altering [dbo].[Contents]...';


GO
ALTER TABLE [dbo].[Contents]
    ADD [Description] NVARCHAR (MAX) NULL;


GO
PRINT N'Creating [dbo].[ResultPublications]...';


GO
CREATE TABLE [dbo].[ResultPublications] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]       INT            NOT NULL,
    [AcademicSessionId] INT            NOT NULL,
    [Title]             NVARCHAR (512) NULL,
    [Body]              NVARCHAR (MAX) NULL,
    [IsActive]          BIT            NOT NULL,
    [LastUpdateTime]    DATETIME       NULL,
    CONSTRAINT [PK_ResultPublications] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_ResultPublications_Institutes]...';


GO
ALTER TABLE [dbo].[ResultPublications] WITH NOCHECK
    ADD CONSTRAINT [FK_ResultPublications_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ResultPublications_AcademicSessions]...';


GO
ALTER TABLE [dbo].[ResultPublications] WITH NOCHECK
    ADD CONSTRAINT [FK_ResultPublications_AcademicSessions] FOREIGN KEY ([AcademicSessionId]) REFERENCES [dbo].[AcademicSessions] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[ResultPublications] WITH CHECK CHECK CONSTRAINT [FK_ResultPublications_Institutes];

ALTER TABLE [dbo].[ResultPublications] WITH CHECK CHECK CONSTRAINT [FK_ResultPublications_AcademicSessions];


GO
update dbo._Migration set LastUpdate='0040'
PRINT N'Update complete.';


GO

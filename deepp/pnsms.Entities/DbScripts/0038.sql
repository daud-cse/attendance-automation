
USE PNSMS;
GO

IF EXISTS (select top 1 1 from [dbo].[FeesGenerates])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[FeesGenerateStudents].[IsPublished] on table [dbo].[FeesGenerateStudents] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[FeesGenerateStudents])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_FeesGenerateAcademics_FeesGenerates]...';


GO
ALTER TABLE [dbo].[FeesGenerateAcademics] DROP CONSTRAINT [FK_FeesGenerateAcademics_FeesGenerates];


GO
PRINT N'Dropping [dbo].[FK_FeesGenerateHeads_FeesGenerates]...';


GO
ALTER TABLE [dbo].[FeesGenerateHeads] DROP CONSTRAINT [FK_FeesGenerateHeads_FeesGenerates];


GO
PRINT N'Dropping [dbo].[FK_FeesGenerates_Institutes]...';


GO
ALTER TABLE [dbo].[FeesGenerates] DROP CONSTRAINT [FK_FeesGenerates_Institutes];


GO
PRINT N'Dropping [dbo].[FK_FeesGenerateStudents_FeesGenerates]...';


GO
ALTER TABLE [dbo].[FeesGenerateStudents] DROP CONSTRAINT [FK_FeesGenerateStudents_FeesGenerates];


GO
PRINT N'Starting rebuilding table [dbo].[FeesGenerates]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_FeesGenerates] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT             NOT NULL,
    [ForTheMonth]    INT             NOT NULL,
    [ForTheYear]     INT             NOT NULL,
    [ForTheDate]     DATE            NOT NULL,
    [GenerationDate] DATE            NULL,
    [DueDate]        DATE            NULL,
    [Remarks]        NVARCHAR (1024) NULL,
    [LastUpdateTime] DATETIME        NULL,
    [IsPublished]    BIT             NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_FeesGenerates] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[FeesGenerates])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_FeesGenerates] ON;
        INSERT INTO [dbo].[tmp_ms_xx_FeesGenerates] ([Id], [InstituteId], [ForTheMonth], [ForTheYear], [GenerationDate], [DueDate], [Remarks], [LastUpdateTime])
        SELECT   [Id],
                 [InstituteId],
                 [ForTheMonth],
                 [ForTheYear],
                 [GenerationDate],
                 [DueDate],
                 [Remarks],
                 [LastUpdateTime]
        FROM     [dbo].[FeesGenerates]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_FeesGenerates] OFF;
    END

DROP TABLE [dbo].[FeesGenerates];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_FeesGenerates]', N'FeesGenerates';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_FeesGenerates]', N'PK_FeesGenerates', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Altering [dbo].[FeesGenerateStudents]...';


GO
ALTER TABLE [dbo].[FeesGenerateStudents]
    ADD [IsPublished] BIT NOT NULL;


GO
PRINT N'Creating [dbo].[FK_FeesGenerateAcademics_FeesGenerates]...';


GO
ALTER TABLE [dbo].[FeesGenerateAcademics] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateAcademics_FeesGenerates] FOREIGN KEY ([FeesGenerateId]) REFERENCES [dbo].[FeesGenerates] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateHeads_FeesGenerates]...';


GO
ALTER TABLE [dbo].[FeesGenerateHeads] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateHeads_FeesGenerates] FOREIGN KEY ([FeesGenerateId]) REFERENCES [dbo].[FeesGenerates] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerates_Institutes]...';


GO
ALTER TABLE [dbo].[FeesGenerates] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerates_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateStudents_FeesGenerates]...';


GO
ALTER TABLE [dbo].[FeesGenerateStudents] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateStudents_FeesGenerates] FOREIGN KEY ([FeesGenerateId]) REFERENCES [dbo].[FeesGenerates] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO

USE PNSMS;


GO
ALTER TABLE [dbo].[FeesGenerateAcademics] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateAcademics_FeesGenerates];

ALTER TABLE [dbo].[FeesGenerateHeads] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateHeads_FeesGenerates];

ALTER TABLE [dbo].[FeesGenerates] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerates_Institutes];

ALTER TABLE [dbo].[FeesGenerateStudents] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateStudents_FeesGenerates];

GO
update dbo._Migration set LastUpdate='0038'
PRINT N'Update complete.';


GO

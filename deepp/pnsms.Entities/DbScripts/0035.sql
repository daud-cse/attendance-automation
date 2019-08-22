
USE PNSMS;


GO
/*
The column [dbo].[ContentDetails].[AcademicClassId] is being dropped, data loss could occur.

The column [dbo].[ContentDetails].[SubjectId] is being dropped, data loss could occur.

The column [dbo].[ContentDetails].[TagId] on table [dbo].[ContentDetails] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[ContentDetails])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[Contents].[TeacherId] is being dropped, data loss could occur.

The column [dbo].[Contents].[UserInfoId] on table [dbo].[Contents] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[Contents])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping FK_ContentDetails_Contents...';


GO
ALTER TABLE [dbo].[ContentDetails] DROP CONSTRAINT [FK_ContentDetails_Contents];


GO
PRINT N'Dropping FK_Contents_Institutes...';


GO
ALTER TABLE [dbo].[Contents] DROP CONSTRAINT [FK_Contents_Institutes];


GO
PRINT N'Dropping FK_Contents_Teachers...';


GO
ALTER TABLE [dbo].[Contents] DROP CONSTRAINT [FK_Contents_Teachers];


GO
PRINT N'Dropping FK_ContentDetails_AcademicClasses...';


GO
ALTER TABLE [dbo].[ContentDetails] DROP CONSTRAINT [FK_ContentDetails_AcademicClasses];


GO
PRINT N'Dropping FK_ContentDetails_Subject...';


GO
ALTER TABLE [dbo].[ContentDetails] DROP CONSTRAINT [FK_ContentDetails_Subject];


GO
PRINT N'Altering [dbo].[ContentDetails]...';


GO
ALTER TABLE [dbo].[ContentDetails] DROP COLUMN [AcademicClassId], COLUMN [SubjectId];


GO
ALTER TABLE [dbo].[ContentDetails]
    ADD [TagId] INT NOT NULL;


GO
PRINT N'Starting rebuilding table [dbo].[Contents]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Contents] (
    [Id]             INT      IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT      NOT NULL,
    [UserInfoId]     INT      NOT NULL,
    [IsActive]       BIT      NOT NULL,
    [LastUpdateTime] DATETIME NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Contents] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Contents])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Contents] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Contents] ([Id], [InstituteId], [IsActive], [LastUpdateTime])
        SELECT   [Id],
                 [InstituteId],
                 [IsActive],
                 [LastUpdateTime]
        FROM     [dbo].[Contents]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Contents] OFF;
    END

DROP TABLE [dbo].[Contents];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Contents]', N'Contents';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Contents]', N'PK_Contents', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Tags]...';


GO
CREATE TABLE [dbo].[Tags] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT            NOT NULL,
    [TagText]        NVARCHAR (512) NOT NULL,
    [IsActive]       BIT            NOT NULL,
    [LastUpdateTime] DATETIME       NULL,
    CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating FK_ContentDetails_Contents...';


GO
ALTER TABLE [dbo].[ContentDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_ContentDetails_Contents] FOREIGN KEY ([ContentId]) REFERENCES [dbo].[Contents] ([Id]);


GO
PRINT N'Creating FK_Contents_Institutes...';


GO
ALTER TABLE [dbo].[Contents] WITH NOCHECK
    ADD CONSTRAINT [FK_Contents_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating FK_Contents_UserInfoes...';


GO
ALTER TABLE [dbo].[Contents] WITH NOCHECK
    ADD CONSTRAINT [FK_Contents_UserInfoes] FOREIGN KEY ([UserInfoId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating FK_Tags_Institutes...';


GO
ALTER TABLE [dbo].[Tags] WITH NOCHECK
    ADD CONSTRAINT [FK_Tags_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating FK_ContentDetails_Tags...';


GO
ALTER TABLE [dbo].[ContentDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_ContentDetails_Tags] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[ContentDetails] WITH CHECK CHECK CONSTRAINT [FK_ContentDetails_Contents];

ALTER TABLE [dbo].[Contents] WITH CHECK CHECK CONSTRAINT [FK_Contents_Institutes];

ALTER TABLE [dbo].[Contents] WITH CHECK CHECK CONSTRAINT [FK_Contents_UserInfoes];

ALTER TABLE [dbo].[Tags] WITH CHECK CHECK CONSTRAINT [FK_Tags_Institutes];

ALTER TABLE [dbo].[ContentDetails] WITH CHECK CHECK CONSTRAINT [FK_ContentDetails_Tags];


GO
update dbo._Migration set LastUpdate='0035'
PRINT N'Update complete.';


GO

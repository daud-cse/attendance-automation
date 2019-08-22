
USE PNSMS;


GO
PRINT N'Dropping [dbo].[FK_ShortMessageTemplates_Institutes]...';


GO
ALTER TABLE [dbo].[ShortMessageTemplates] DROP CONSTRAINT [FK_ShortMessageTemplates_Institutes];


GO
PRINT N'Altering [dbo].[ShortMessages]...';


GO
ALTER TABLE [dbo].[ShortMessages]
    ADD [IsChecked] BIT NULL;


GO
PRINT N'Starting rebuilding table [dbo].[ShortMessageTemplates]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_ShortMessageTemplates] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]        INT            NOT NULL,
    [SmsTemplate]        NVARCHAR (512) NOT NULL,
    [IsStaticSms]        BIT            NOT NULL,
    [IsActive]           BIT            NOT NULL,
    [IsForGeneral]       BIT            NULL,
    [IsForStudent]       BIT            NULL,
    [IsForGuardian]      BIT            NULL,
    [IsForTeacher]       BIT            NULL,
    [IsForEmployee]      BIT            NULL,
    [IsForGoverningbody] BIT            NULL,
    [LastUpdateTime]     DATETIME       NULL,
    [SmsCount]           INT            NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_ShortMessageTemplates] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[ShortMessageTemplates])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ShortMessageTemplates] ON;
        INSERT INTO [dbo].[tmp_ms_xx_ShortMessageTemplates] ([Id], [InstituteId], [SmsTemplate], [IsStaticSms], [IsActive], [IsForGeneral], [IsForStudent], [IsForTeacher], [IsForEmployee], [LastUpdateTime], [SmsCount])
        SELECT   [Id],
                 [InstituteId],
                 [SmsTemplate],
                 [IsStaticSms],
                 [IsActive],
                 [IsForGeneral],
                 [IsForStudent],
                 [IsForTeacher],
                 [IsForEmployee],
                 [LastUpdateTime],
                 [SmsCount]
        FROM     [dbo].[ShortMessageTemplates]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ShortMessageTemplates] OFF;
    END

DROP TABLE [dbo].[ShortMessageTemplates];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_ShortMessageTemplates]', N'ShortMessageTemplates';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_ShortMessageTemplates]', N'PK_ShortMessageTemplates', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[FK_ShortMessageTemplates_Institutes]...';


GO
ALTER TABLE [dbo].[ShortMessageTemplates] WITH NOCHECK
    ADD CONSTRAINT [FK_ShortMessageTemplates_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[ShortMessageTemplates] WITH CHECK CHECK CONSTRAINT [FK_ShortMessageTemplates_Institutes];


GO

update dbo._Migration set LastUpdate='0025'
PRINT N'Update complete.';


GO

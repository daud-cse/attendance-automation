
USE PNSMS;


GO
/*
The column [dbo].[ShortMessageTemplates].[Name] on table [dbo].[ShortMessageTemplates] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

delete from [dbo].[ShortMessageTemplates]
GO

IF EXISTS (select top 1 1 from [dbo].[ShortMessageTemplates])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Altering [dbo].[ShortMessageTemplates]...';


GO
ALTER TABLE [dbo].[ShortMessageTemplates]
    ADD [Name] NVARCHAR (128) NOT NULL;


GO

update dbo._Migration set LastUpdate='0029'

PRINT N'Update complete.';


GO


USE PNSMS;


GO
PRINT N'Dropping [dbo].[FK_UserAttendanceDetails_AddressTypes]...';


GO
ALTER TABLE [dbo].[UserAttendanceDetails] DROP CONSTRAINT [FK_UserAttendanceDetails_AddressTypes];


GO
PRINT N'Altering [dbo].[Designations]...';


GO
ALTER TABLE [dbo].[Designations]
    ADD [StaffRequired] INT NULL;


GO
PRINT N'Creating [dbo].[CertificatePrints]...';


GO
CREATE TABLE [dbo].[CertificatePrints] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [CertificatePrintTypeId] INT            NOT NULL,
    [InstituteId]            INT            NOT NULL,
    [Body]                   NVARCHAR (128) NOT NULL,
    [IsActive]               BIT            NOT NULL,
    [LastUpdateTime]         DATETIME       NULL,
    CONSTRAINT [PK_CertificatePrints] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[CertificatePrintTypes]...';


GO
CREATE TABLE [dbo].[CertificatePrintTypes] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT            NOT NULL,
    [Name]           NVARCHAR (128) NOT NULL,
    [IsActive]       BIT            NOT NULL,
    [LastUpdateTime] DATETIME       NULL,
    CONSTRAINT [PK_CertificatePrintTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_UserAttendanceDetails_AddressTypes]...';


GO
ALTER TABLE [dbo].[UserAttendanceDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_UserAttendanceDetails_AddressTypes] FOREIGN KEY ([AttendanceTypeId]) REFERENCES [dbo].[AttendanceTypes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_CertificatePrints_CertificatePrintTypes]...';


GO
ALTER TABLE [dbo].[CertificatePrints] WITH NOCHECK
    ADD CONSTRAINT [FK_CertificatePrints_CertificatePrintTypes] FOREIGN KEY ([CertificatePrintTypeId]) REFERENCES [dbo].[CertificatePrintTypes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_CertificatePrints_Institutes]...';


GO
ALTER TABLE [dbo].[CertificatePrints] WITH NOCHECK
    ADD CONSTRAINT [FK_CertificatePrints_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_CertificatePrintTypes_Institutes]...';


GO
ALTER TABLE [dbo].[CertificatePrintTypes] WITH NOCHECK
    ADD CONSTRAINT [FK_CertificatePrintTypes_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[UserAttendanceDetails] WITH CHECK CHECK CONSTRAINT [FK_UserAttendanceDetails_AddressTypes];

ALTER TABLE [dbo].[CertificatePrints] WITH CHECK CHECK CONSTRAINT [FK_CertificatePrints_CertificatePrintTypes];

ALTER TABLE [dbo].[CertificatePrints] WITH CHECK CHECK CONSTRAINT [FK_CertificatePrints_Institutes];

ALTER TABLE [dbo].[CertificatePrintTypes] WITH CHECK CHECK CONSTRAINT [FK_CertificatePrintTypes_Institutes];


GO

update dbo._Migration set LastUpdate='0030'
PRINT N'Update complete.';


GO

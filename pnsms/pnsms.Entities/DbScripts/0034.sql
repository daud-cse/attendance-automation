
USE PNSMS;


GO
PRINT N'Altering [dbo].[AdmissionFormGuardians]...';


GO
ALTER TABLE [dbo].[AdmissionFormGuardians]
    ADD [FirstName]      NVARCHAR (128) NULL,
        [MiddleName]     NVARCHAR (128) NULL,
        [LastName]       NVARCHAR (128) NULL,
        [Name]           NVARCHAR (128) NULL,
        [ContactNumber1] NVARCHAR (32)  NULL,
        [ContactNumber2] NVARCHAR (32)  NULL,
        [EmailAddress]   NVARCHAR (128) NULL,
        [SSN]            NVARCHAR (128) NULL,
        [PassportNo]     NVARCHAR (128) NULL,
        [DOB]            DATE           NULL,
        [GenderId]       INT            NULL,
        [NationalityId]  INT            NULL,
        [ReligionId]     INT            NULL,
        [BloodGroupId]   INT            NULL;


GO
PRINT N'Altering [dbo].[AdmissionForms]...';


GO
ALTER TABLE [dbo].[AdmissionForms] ALTER COLUMN [Code] NVARCHAR (128) NULL;



update dbo._Migration set LastUpdate='0034'

PRINT N'Update complete.';


GO

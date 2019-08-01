
USE PNSMS;


GO
/*
The column [dbo].[GlobalUsers].[GlobalUserTypeId] on table [dbo].[GlobalUsers] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[GlobalUsers])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_GlobalUsers_UserInfoes]...';


GO
ALTER TABLE [dbo].[GlobalUsers] DROP CONSTRAINT [FK_GlobalUsers_UserInfoes];


GO
PRINT N'Altering [dbo].[BloodGroups]...';


GO
ALTER TABLE [dbo].[BloodGroups]
    ADD [GlobalBloodGroupId] INT NULL;


GO
PRINT N'Altering [dbo].[Genders]...';


GO
ALTER TABLE [dbo].[Genders]
    ADD [GlobalGenderId] INT NULL;


GO
PRINT N'Starting rebuilding table [dbo].[GlobalUsers]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_GlobalUsers] (
    [GlobalUserId]     INT            NOT NULL,
    [GlobalUserTypeId] INT            NOT NULL,
    [FatherName]       NVARCHAR (128) NULL,
    [MotherName]       NVARCHAR (128) NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_GlobalUsers] PRIMARY KEY CLUSTERED ([GlobalUserId] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[GlobalUsers])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_GlobalUsers] ([GlobalUserId], [FatherName], [MotherName])
        SELECT   [GlobalUserId],
                 [FatherName],
                 [MotherName]
        FROM     [dbo].[GlobalUsers]
        ORDER BY [GlobalUserId] ASC;
    END

DROP TABLE [dbo].[GlobalUsers];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_GlobalUsers]', N'GlobalUsers';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_GlobalUsers]', N'PK_GlobalUsers', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Altering [dbo].[Rights]...';


GO
ALTER TABLE [dbo].[Rights]
    ADD [EnableGlobalUser] BIT NULL;


GO
PRINT N'Altering [dbo].[Roles]...';


GO
ALTER TABLE [dbo].[Roles]
    ADD [EnableGlobalUser] BIT NULL,
        [GlobalUserTypeId] INT NULL;


GO
PRINT N'Creating [dbo].[AdmissionFormAddresses]...';


GO
CREATE TABLE [dbo].[AdmissionFormAddresses] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [AdmissionFormId]   INT            NOT NULL,
    [DistrictOrStateId] INT            NULL,
    [ZipCode]           NVARCHAR (32)  NULL,
    [AddressBody]       NVARCHAR (512) NULL,
    CONSTRAINT [PK_AdmissionFormAddresses] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[AdmissionFormGuardians]...';


GO
CREATE TABLE [dbo].[AdmissionFormGuardians] (
    [Id]                         INT IDENTITY (1, 1) NOT NULL,
    [AdmissionFormId]            INT NOT NULL,
    [GuardianTypeId]             INT NOT NULL,
    [EducationalQualificationId] INT NULL,
    [ProfessionId]               INT NULL,
    [MonthlyIncome]              INT NULL,
    CONSTRAINT [PK_AdmissionFormGuardians] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[AdmissionForms]...';


GO
CREATE TABLE [dbo].[AdmissionForms] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [InstituteId]       INT             NOT NULL,
    [ImageBinaryData]   VARBINARY (MAX) NULL,
    [Code]              NCHAR (128)     NULL,
    [FirstName]         NVARCHAR (128)  NULL,
    [MiddleName]        NVARCHAR (128)  NULL,
    [LastName]          NVARCHAR (128)  NULL,
    [Name]              NVARCHAR (128)  NOT NULL,
    [ContactNumber]     NVARCHAR (32)   NULL,
    [EmailAddress]      NVARCHAR (128)  NULL,
    [SSN]               NVARCHAR (128)  NULL,
    [PassportNo]        NVARCHAR (128)  NULL,
    [DOB]               DATE            NULL,
    [GenderId]          INT             NULL,
    [NationalityId]     INT             NULL,
    [ReligionId]        INT             NULL,
    [BloodGroupId]      INT             NULL,
    [LastUpdateTime]    DATETIME        NULL,
    [AcademicSessionId] INT             NULL,
    [AcademicClassId]   INT             NULL,
    [AcademicBranchId]  INT             NULL,
    [IsActive]          BIT             NOT NULL,
    [IsSelected]        BIT             NOT NULL,
    CONSTRAINT [PK_AdmissionForms] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[GlobalBloodGroups]...';


GO
CREATE TABLE [dbo].[GlobalBloodGroups] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (256) NOT NULL,
    [IsActive] BIT            NOT NULL,
    CONSTRAINT [PK_GlobalBloodGroups] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[GlobalGenders]...';


GO
CREATE TABLE [dbo].[GlobalGenders] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (256) NOT NULL,
    [IsActive] BIT            NOT NULL,
    CONSTRAINT [PK_GlobalGenders] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[GlobalUserTypes]...';


GO
CREATE TABLE [dbo].[GlobalUserTypes] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (256) NOT NULL,
    [IsActive] BIT            NOT NULL,
    CONSTRAINT [PK_GlobalUserTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_GlobalUsers_UserInfoes]...';


GO
ALTER TABLE [dbo].[GlobalUsers] WITH NOCHECK
    ADD CONSTRAINT [FK_GlobalUsers_UserInfoes] FOREIGN KEY ([GlobalUserId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_GlobalUsers_GlobalUserTypes]...';


GO
ALTER TABLE [dbo].[GlobalUsers] WITH NOCHECK
    ADD CONSTRAINT [FK_GlobalUsers_GlobalUserTypes] FOREIGN KEY ([GlobalUserTypeId]) REFERENCES [dbo].[GlobalUserTypes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AdmissionFormAddresses_DistrictOrStates]...';


GO
ALTER TABLE [dbo].[AdmissionFormAddresses] WITH NOCHECK
    ADD CONSTRAINT [FK_AdmissionFormAddresses_DistrictOrStates] FOREIGN KEY ([DistrictOrStateId]) REFERENCES [dbo].[DistrictOrStates] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AdmissionFormAddresses_AdmissionForms]...';


GO
ALTER TABLE [dbo].[AdmissionFormAddresses] WITH NOCHECK
    ADD CONSTRAINT [FK_AdmissionFormAddresses_AdmissionForms] FOREIGN KEY ([AdmissionFormId]) REFERENCES [dbo].[AdmissionForms] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AdmissionFormGuardians_GuardianTypes]...';


GO
ALTER TABLE [dbo].[AdmissionFormGuardians] WITH NOCHECK
    ADD CONSTRAINT [FK_AdmissionFormGuardians_GuardianTypes] FOREIGN KEY ([GuardianTypeId]) REFERENCES [dbo].[GuardianTypes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AdmissionFormGuardians_EducationalQualifications]...';


GO
ALTER TABLE [dbo].[AdmissionFormGuardians] WITH NOCHECK
    ADD CONSTRAINT [FK_AdmissionFormGuardians_EducationalQualifications] FOREIGN KEY ([EducationalQualificationId]) REFERENCES [dbo].[EducationalQualifications] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AdmissionFormGuardians_Professions]...';


GO
ALTER TABLE [dbo].[AdmissionFormGuardians] WITH NOCHECK
    ADD CONSTRAINT [FK_AdmissionFormGuardians_Professions] FOREIGN KEY ([ProfessionId]) REFERENCES [dbo].[Professions] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AdmissionFormGuardians_AdmissionForms]...';


GO
ALTER TABLE [dbo].[AdmissionFormGuardians] WITH NOCHECK
    ADD CONSTRAINT [FK_AdmissionFormGuardians_AdmissionForms] FOREIGN KEY ([AdmissionFormId]) REFERENCES [dbo].[AdmissionForms] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AdmissionForms_Institutes]...';


GO
ALTER TABLE [dbo].[AdmissionForms] WITH NOCHECK
    ADD CONSTRAINT [FK_AdmissionForms_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AdmissionForms_Genders]...';


GO
ALTER TABLE [dbo].[AdmissionForms] WITH NOCHECK
    ADD CONSTRAINT [FK_AdmissionForms_Genders] FOREIGN KEY ([GenderId]) REFERENCES [dbo].[Genders] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AdmissionForms_Nationalities]...';


GO
ALTER TABLE [dbo].[AdmissionForms] WITH NOCHECK
    ADD CONSTRAINT [FK_AdmissionForms_Nationalities] FOREIGN KEY ([NationalityId]) REFERENCES [dbo].[Nationalities] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AdmissionForms_Religions]...';


GO
ALTER TABLE [dbo].[AdmissionForms] WITH NOCHECK
    ADD CONSTRAINT [FK_AdmissionForms_Religions] FOREIGN KEY ([ReligionId]) REFERENCES [dbo].[Religions] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AdmissionForms_BloodGroups]...';


GO
ALTER TABLE [dbo].[AdmissionForms] WITH NOCHECK
    ADD CONSTRAINT [FK_AdmissionForms_BloodGroups] FOREIGN KEY ([BloodGroupId]) REFERENCES [dbo].[BloodGroups] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AdmissionForms_AcademicSessions]...';


GO
ALTER TABLE [dbo].[AdmissionForms] WITH NOCHECK
    ADD CONSTRAINT [FK_AdmissionForms_AcademicSessions] FOREIGN KEY ([AcademicSessionId]) REFERENCES [dbo].[AcademicSessions] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AdmissionForms_AcademicClasses]...';


GO
ALTER TABLE [dbo].[AdmissionForms] WITH NOCHECK
    ADD CONSTRAINT [FK_AdmissionForms_AcademicClasses] FOREIGN KEY ([AcademicClassId]) REFERENCES [dbo].[AcademicClasses] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AdmissionForms_AcademicBranches]...';


GO
ALTER TABLE [dbo].[AdmissionForms] WITH NOCHECK
    ADD CONSTRAINT [FK_AdmissionForms_AcademicBranches] FOREIGN KEY ([AcademicBranchId]) REFERENCES [dbo].[AcademicBranches] ([Id]);


GO
PRINT N'Creating [dbo].[FK_BloodGroups_GlobalBloodGroups]...';


GO
ALTER TABLE [dbo].[BloodGroups] WITH NOCHECK
    ADD CONSTRAINT [FK_BloodGroups_GlobalBloodGroups] FOREIGN KEY ([GlobalBloodGroupId]) REFERENCES [dbo].[GlobalBloodGroups] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Genders_GlobalGenders]...';


GO
ALTER TABLE [dbo].[Genders] WITH NOCHECK
    ADD CONSTRAINT [FK_Genders_GlobalGenders] FOREIGN KEY ([GlobalGenderId]) REFERENCES [dbo].[GlobalGenders] ([Id]);


GO
PRINT N'Refreshing [dbo].[QryRightsOfRoles]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[QryRightsOfRoles]';


GO
PRINT N'Refreshing [dbo].[QryRightsOfUserInfoes]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[QryRightsOfUserInfoes]';


GO
PRINT N'Refreshing [dbo].[SprGetRights]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[SprGetRights]';


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[GlobalUsers] WITH CHECK CHECK CONSTRAINT [FK_GlobalUsers_UserInfoes];

ALTER TABLE [dbo].[GlobalUsers] WITH CHECK CHECK CONSTRAINT [FK_GlobalUsers_GlobalUserTypes];

ALTER TABLE [dbo].[AdmissionFormAddresses] WITH CHECK CHECK CONSTRAINT [FK_AdmissionFormAddresses_DistrictOrStates];

ALTER TABLE [dbo].[AdmissionFormAddresses] WITH CHECK CHECK CONSTRAINT [FK_AdmissionFormAddresses_AdmissionForms];

ALTER TABLE [dbo].[AdmissionFormGuardians] WITH CHECK CHECK CONSTRAINT [FK_AdmissionFormGuardians_GuardianTypes];

ALTER TABLE [dbo].[AdmissionFormGuardians] WITH CHECK CHECK CONSTRAINT [FK_AdmissionFormGuardians_EducationalQualifications];

ALTER TABLE [dbo].[AdmissionFormGuardians] WITH CHECK CHECK CONSTRAINT [FK_AdmissionFormGuardians_Professions];

ALTER TABLE [dbo].[AdmissionFormGuardians] WITH CHECK CHECK CONSTRAINT [FK_AdmissionFormGuardians_AdmissionForms];

ALTER TABLE [dbo].[AdmissionForms] WITH CHECK CHECK CONSTRAINT [FK_AdmissionForms_Institutes];

ALTER TABLE [dbo].[AdmissionForms] WITH CHECK CHECK CONSTRAINT [FK_AdmissionForms_Genders];

ALTER TABLE [dbo].[AdmissionForms] WITH CHECK CHECK CONSTRAINT [FK_AdmissionForms_Nationalities];

ALTER TABLE [dbo].[AdmissionForms] WITH CHECK CHECK CONSTRAINT [FK_AdmissionForms_Religions];

ALTER TABLE [dbo].[AdmissionForms] WITH CHECK CHECK CONSTRAINT [FK_AdmissionForms_BloodGroups];

ALTER TABLE [dbo].[AdmissionForms] WITH CHECK CHECK CONSTRAINT [FK_AdmissionForms_AcademicSessions];

ALTER TABLE [dbo].[AdmissionForms] WITH CHECK CHECK CONSTRAINT [FK_AdmissionForms_AcademicClasses];

ALTER TABLE [dbo].[AdmissionForms] WITH CHECK CHECK CONSTRAINT [FK_AdmissionForms_AcademicBranches];

ALTER TABLE [dbo].[BloodGroups] WITH CHECK CHECK CONSTRAINT [FK_BloodGroups_GlobalBloodGroups];

ALTER TABLE [dbo].[Genders] WITH CHECK CHECK CONSTRAINT [FK_Genders_GlobalGenders];


GO
update dbo._Migration set LastUpdate='0021'
PRINT N'Update complete.';


GO

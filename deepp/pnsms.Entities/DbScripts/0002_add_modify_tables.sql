
USE PNSMS;


GO
/*
The column [dbo].[AcademicBranches].[BannerUrl] is being dropped, data loss could occur.

The column [dbo].[AcademicBranches].[LogoUrl] is being dropped, data loss could occur.

The column [dbo].[AcademicBranches].[VideoURL] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[AcademicBranches])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[Addresses].[RefCode] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Addresses])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[Employees].[Id] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Employees])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[Guardians].[Id] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Guardians])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[Institutes].[BannerUrl] is being dropped, data loss could occur.

The column [dbo].[Institutes].[LogoUrl] is being dropped, data loss could occur.

The column [dbo].[Institutes].[VideoURL] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Institutes])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[Students].[Id] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Students])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[Teachers].[Id] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Teachers])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[UserInfoes].[PhotoUrl] is being dropped, data loss could occur.

The column [dbo].[UserInfoes].[SmallPhotoUrl] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[UserInfoes])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_UserInfoes_BloodGroups]...';


GO
ALTER TABLE [dbo].[UserInfoes] DROP CONSTRAINT [FK_UserInfoes_BloodGroups];


GO
PRINT N'Dropping [dbo].[FK_UserInfoes_Genders]...';


GO
ALTER TABLE [dbo].[UserInfoes] DROP CONSTRAINT [FK_UserInfoes_Genders];


GO
PRINT N'Dropping [dbo].[FK_UserInfoes_Institutes]...';


GO
ALTER TABLE [dbo].[UserInfoes] DROP CONSTRAINT [FK_UserInfoes_Institutes];


GO
PRINT N'Dropping [dbo].[FK_UserInfoes_Nationalities]...';


GO
ALTER TABLE [dbo].[UserInfoes] DROP CONSTRAINT [FK_UserInfoes_Nationalities];


GO
PRINT N'Dropping [dbo].[FK_UserInfoes_Religions]...';


GO
ALTER TABLE [dbo].[UserInfoes] DROP CONSTRAINT [FK_UserInfoes_Religions];


GO
PRINT N'Dropping [dbo].[FK_Guardians_UserInfoes]...';


GO
ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_Guardians_UserInfoes];


GO
PRINT N'Dropping [dbo].[FK_Employees_UserInfoes]...';


GO
ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_UserInfoes];


GO
PRINT N'Dropping [dbo].[FK_Teachers_UserInfoes]...';


GO
ALTER TABLE [dbo].[Teachers] DROP CONSTRAINT [FK_Teachers_UserInfoes];


GO
PRINT N'Dropping [dbo].[FK_Students_UserInfoes]...';


GO
ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_Students_UserInfoes];


GO
PRINT N'Dropping [dbo].[FK_Employees_Departments]...';


GO
ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_Departments];


GO
PRINT N'Dropping [dbo].[FK_Employees_Designations]...';


GO
ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_Designations];


GO
PRINT N'Dropping [dbo].[FK_Employees_MaritalStatuses]...';


GO
ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_MaritalStatuses];


GO
PRINT N'Dropping [dbo].[FK_Guardians_EducationalQualifications]...';


GO
ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_Guardians_EducationalQualifications];


GO
PRINT N'Dropping [dbo].[FK_Guardians_GuardianTypes]...';


GO
ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_Guardians_GuardianTypes];


GO
PRINT N'Dropping [dbo].[FK_Guardians_Professions]...';


GO
ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_Guardians_Professions];


GO
PRINT N'Dropping [dbo].[FK_GuardiansOfStudents_Guardians]...';


GO
ALTER TABLE [dbo].[GuardiansOfStudents] DROP CONSTRAINT [FK_GuardiansOfStudents_Guardians];


GO
PRINT N'Dropping [dbo].[FK_StudentAttendanceDetails_Students]...';


GO
ALTER TABLE [dbo].[StudentAttendanceDetails] DROP CONSTRAINT [FK_StudentAttendanceDetails_Students];


GO
PRINT N'Dropping [dbo].[FK_Students_AcademicShifts]...';


GO
ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_Students_AcademicShifts];


GO
PRINT N'Dropping [dbo].[FK_Students_AcademicVersions]...';


GO
ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_Students_AcademicVersions];


GO
PRINT N'Dropping [dbo].[FK_Students_AcademicBranches]...';


GO
ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_Students_AcademicBranches];


GO
PRINT N'Dropping [dbo].[FK_Students_AcademicClasses]...';


GO
ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_Students_AcademicClasses];


GO
PRINT N'Dropping [dbo].[FK_Students_AcademicGroups]...';


GO
ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_Students_AcademicGroups];


GO
PRINT N'Dropping [dbo].[FK_Students_AcademicSections]...';


GO
ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_Students_AcademicSections];


GO
PRINT N'Dropping [dbo].[FK_Students_AcademicSessions]...';


GO
ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_Students_AcademicSessions];


GO
PRINT N'Dropping [dbo].[FK_Siblings_SiblingStudents]...';


GO
ALTER TABLE [dbo].[Siblings] DROP CONSTRAINT [FK_Siblings_SiblingStudents];


GO
PRINT N'Dropping [dbo].[FK_Siblings_Students]...';


GO
ALTER TABLE [dbo].[Siblings] DROP CONSTRAINT [FK_Siblings_Students];


GO
PRINT N'Dropping [dbo].[FK_GuardiansOfStudents_Students]...';


GO
ALTER TABLE [dbo].[GuardiansOfStudents] DROP CONSTRAINT [FK_GuardiansOfStudents_Students];


GO
PRINT N'Dropping [dbo].[FK_StudentAttendances_Teachers]...';


GO
ALTER TABLE [dbo].[StudentAttendances] DROP CONSTRAINT [FK_StudentAttendances_Teachers];


GO
PRINT N'Dropping [dbo].[FK_Teachers_AcademicBranches]...';


GO
ALTER TABLE [dbo].[Teachers] DROP CONSTRAINT [FK_Teachers_AcademicBranches];


GO
PRINT N'Dropping [dbo].[FK_Teachers_Designations]...';


GO
ALTER TABLE [dbo].[Teachers] DROP CONSTRAINT [FK_Teachers_Designations];


GO
PRINT N'Dropping [dbo].[FK_Teachers_MaritalStatuses]...';


GO
ALTER TABLE [dbo].[Teachers] DROP CONSTRAINT [FK_Teachers_MaritalStatuses];


GO
PRINT N'Dropping [dbo].[IX_Guardians]...';


GO
ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [IX_Guardians];


GO
PRINT N'Dropping [dbo].[IX_Students]...';


GO
ALTER TABLE [dbo].[Students] DROP CONSTRAINT [IX_Students];


GO
PRINT N'Dropping [dbo].[IX_Teachers]...';


GO
ALTER TABLE [dbo].[Teachers] DROP CONSTRAINT [IX_Teachers];


GO
PRINT N'Altering [dbo].[AcademicBranches]...';


GO
ALTER TABLE [dbo].[AcademicBranches] DROP COLUMN [BannerUrl], COLUMN [LogoUrl], COLUMN [VideoURL];


GO
PRINT N'Altering [dbo].[Addresses]...';


GO
ALTER TABLE [dbo].[Addresses] DROP COLUMN [RefCode];


GO
PRINT N'Altering [dbo].[AttendanceTypes]...';


GO
ALTER TABLE [dbo].[AttendanceTypes]
    ADD [IsDefault] BIT NULL;


GO
PRINT N'Altering [dbo].[Colours]...';


GO
ALTER TABLE [dbo].[Colours] ALTER COLUMN [ColorCode] NVARCHAR (128) NOT NULL;


GO
PRINT N'Starting rebuilding table [dbo].[Employees]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Employees] (
    [EmployeeId]      INT            NOT NULL,
    [FatherName]      NVARCHAR (128) NULL,
    [MotherName]      NVARCHAR (128) NULL,
    [MaritalStatusId] INT            NULL,
    [DesignationId]   INT            NULL,
    [DepartmentId]    INT            NULL,
    [LastUpdateTime]  DATETIME       NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Employees] PRIMARY KEY CLUSTERED ([EmployeeId] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Employees])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Employees] ([EmployeeId], [FatherName], [MotherName], [MaritalStatusId], [DesignationId], [DepartmentId], [LastUpdateTime])
        SELECT   [EmployeeId],
                 [FatherName],
                 [MotherName],
                 [MaritalStatusId],
                 [DesignationId],
                 [DepartmentId],
                 [LastUpdateTime]
        FROM     [dbo].[Employees]
        ORDER BY [EmployeeId] ASC;
    END

DROP TABLE [dbo].[Employees];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Employees]', N'Employees';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Employees]', N'PK_Employees', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[Guardians]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Guardians] (
    [GuardianId]                 INT      NOT NULL,
    [GuardianTypeId]             INT      NOT NULL,
    [EducationalQualificationId] INT      NULL,
    [ProfessionId]               INT      NULL,
    [MonthlyIncome]              INT      NULL,
    [LastUpdateTime]             DATETIME NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Guardians] PRIMARY KEY CLUSTERED ([GuardianId] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Guardians])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Guardians] ([GuardianId], [GuardianTypeId], [EducationalQualificationId], [ProfessionId], [MonthlyIncome], [LastUpdateTime])
        SELECT   [GuardianId],
                 [GuardianTypeId],
                 [EducationalQualificationId],
                 [ProfessionId],
                 [MonthlyIncome],
                 [LastUpdateTime]
        FROM     [dbo].[Guardians]
        ORDER BY [GuardianId] ASC;
    END

DROP TABLE [dbo].[Guardians];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Guardians]', N'Guardians';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Guardians]', N'PK_Guardians', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Altering [dbo].[Institutes]...';


GO
ALTER TABLE [dbo].[Institutes] DROP COLUMN [BannerUrl], COLUMN [LogoUrl], COLUMN [VideoURL];


GO
PRINT N'Starting rebuilding table [dbo].[Students]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Students] (
    [StudentId]                 INT            NOT NULL,
    [CurrentAcademicSessionId]  INT            NULL,
    [CurrentAcademicBranchId]   INT            NULL,
    [CurrentAcademicClassId]    INT            NULL,
    [CurrentAcademicShiftId]    INT            NULL,
    [CurrentAcademicSectionId]  INT            NULL,
    [CurrentAcademicVerssionId] INT            NULL,
    [CurrentAcademicGroupId]    INT            NULL,
    [CurrentRollNo]             NVARCHAR (128) NULL,
    [LastUpdateTime]            DATETIME       NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Students] PRIMARY KEY CLUSTERED ([StudentId] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Students])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Students] ([StudentId], [CurrentAcademicSessionId], [CurrentAcademicBranchId], [CurrentAcademicClassId], [CurrentAcademicShiftId], [CurrentAcademicSectionId], [CurrentAcademicVerssionId], [CurrentAcademicGroupId], [CurrentRollNo], [LastUpdateTime])
        SELECT   [StudentId],
                 [CurrentAcademicSessionId],
                 [CurrentAcademicBranchId],
                 [CurrentAcademicClassId],
                 [CurrentAcademicShiftId],
                 [CurrentAcademicSectionId],
                 [CurrentAcademicVerssionId],
                 [CurrentAcademicGroupId],
                 [CurrentRollNo],
                 [LastUpdateTime]
        FROM     [dbo].[Students]
        ORDER BY [StudentId] ASC;
    END

DROP TABLE [dbo].[Students];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Students]', N'Students';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Students]', N'PK_Students', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[Teachers]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Teachers] (
    [TeacherId]               INT            NOT NULL,
    [FatherName]              NVARCHAR (128) NULL,
    [MotherName]              NVARCHAR (128) NULL,
    [MaritalStatusId]         INT            NULL,
    [DesignationId]           INT            NULL,
    [CurrentAcademicBranchId] INT            NULL,
    [LastUpdateTime]          DATETIME       NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Teachers] PRIMARY KEY CLUSTERED ([TeacherId] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Teachers])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Teachers] ([TeacherId], [FatherName], [MotherName], [MaritalStatusId], [DesignationId], [CurrentAcademicBranchId], [LastUpdateTime])
        SELECT   [TeacherId],
                 [FatherName],
                 [MotherName],
                 [MaritalStatusId],
                 [DesignationId],
                 [CurrentAcademicBranchId],
                 [LastUpdateTime]
        FROM     [dbo].[Teachers]
        ORDER BY [TeacherId] ASC;
    END

DROP TABLE [dbo].[Teachers];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Teachers]', N'Teachers';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Teachers]', N'PK_Teachers', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[UserInfoes]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_UserInfoes] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT            NOT NULL,
    [PIN]            NCHAR (128)    NULL,
    [FirstName]      NVARCHAR (128) NULL,
    [MiddleName]     NVARCHAR (128) NULL,
    [LastName]       NVARCHAR (128) NULL,
    [Name]           NVARCHAR (128) NOT NULL,
    [ContactNumber1] NVARCHAR (32)  NULL,
    [ContactNumber2] NVARCHAR (32)  NULL,
    [EmailAddress]   NVARCHAR (128) NULL,
    [SSN]            NVARCHAR (128) NULL,
    [PassportNo]     NVARCHAR (128) NULL,
    [DOB]            DATE           NULL,
    [GenderId]       INT            NULL,
    [NationalityId]  INT            NULL,
    [ReligionId]     INT            NULL,
    [BloodGroupId]   INT            NULL,
    [GoogleId]       NVARCHAR (128) NULL,
    [FacebookId]     NVARCHAR (128) NULL,
    [MicrosoftId]    NVARCHAR (128) NULL,
    [TwitterId]      NVARCHAR (128) NULL,
    [IsActive]       BIT            NOT NULL,
    [LastUpdateTime] DATETIME       NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_UserInfoes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[UserInfoes])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_UserInfoes] ON;
        INSERT INTO [dbo].[tmp_ms_xx_UserInfoes] ([Id], [InstituteId], [PIN], [FirstName], [LastName], [Name], [ContactNumber1], [ContactNumber2], [EmailAddress], [SSN], [PassportNo], [DOB], [GenderId], [NationalityId], [ReligionId], [BloodGroupId], [GoogleId], [FacebookId], [MicrosoftId], [TwitterId], [IsActive], [LastUpdateTime])
        SELECT   [Id],
                 [InstituteId],
                 [PIN],
                 [FirstName],
                 [LastName],
                 [Name],
                 [ContactNumber1],
                 [ContactNumber2],
                 [EmailAddress],
                 [SSN],
                 [PassportNo],
                 [DOB],
                 [GenderId],
                 [NationalityId],
                 [ReligionId],
                 [BloodGroupId],
                 [GoogleId],
                 [FacebookId],
                 [MicrosoftId],
                 [TwitterId],
                 [IsActive],
                 [LastUpdateTime]
        FROM     [dbo].[UserInfoes]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_UserInfoes] OFF;
    END

DROP TABLE [dbo].[UserInfoes];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_UserInfoes]', N'UserInfoes';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_UserInfoes]', N'PK_UserInfoes', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Images]...';


GO
CREATE TABLE [dbo].[Images] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [RefCode]         NCHAR (16)      NOT NULL,
    [RefPrimaryKey]   INT             NOT NULL,
    [ImageBinaryData] VARBINARY (MAX) NULL,
    [IsActive]        BIT             NOT NULL,
    [LastUpdatedTime] DATETIME        NULL,
    CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_Images] UNIQUE NONCLUSTERED ([RefCode] ASC, [RefPrimaryKey] ASC)
);


GO
PRINT N'Creating [dbo].[Notices]...';


GO
CREATE TABLE [dbo].[Notices] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]       INT            NOT NULL,
    [AcademicBranchId]  INT            NULL,
    [AcademicClassId]   INT            NULL,
    [AcademicSectionId] INT            NULL,
    [StudentId]         INT            NULL,
    [StartDate]         DATE           NULL,
    [EndDate]           DATE           NULL,
    [NoticeTitle]       NVARCHAR (512) NULL,
    [NoticeBody]        NVARCHAR (MAX) NULL,
    [IsActive]          BIT            NOT NULL,
    [LastUpdateTime]    DATETIME       NULL,
    CONSTRAINT [PK_Notices] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_UserInfoes_BloodGroups]...';


GO
ALTER TABLE [dbo].[UserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_UserInfoes_BloodGroups] FOREIGN KEY ([BloodGroupId]) REFERENCES [dbo].[BloodGroups] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserInfoes_Genders]...';


GO
ALTER TABLE [dbo].[UserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_UserInfoes_Genders] FOREIGN KEY ([GenderId]) REFERENCES [dbo].[Genders] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserInfoes_Institutes]...';


GO
ALTER TABLE [dbo].[UserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_UserInfoes_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserInfoes_Nationalities]...';


GO
ALTER TABLE [dbo].[UserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_UserInfoes_Nationalities] FOREIGN KEY ([NationalityId]) REFERENCES [dbo].[Nationalities] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserInfoes_Religions]...';


GO
ALTER TABLE [dbo].[UserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_UserInfoes_Religions] FOREIGN KEY ([ReligionId]) REFERENCES [dbo].[Religions] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Guardians_UserInfoes]...';


GO
ALTER TABLE [dbo].[Guardians] WITH NOCHECK
    ADD CONSTRAINT [FK_Guardians_UserInfoes] FOREIGN KEY ([GuardianId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Employees_UserInfoes]...';


GO
ALTER TABLE [dbo].[Employees] WITH NOCHECK
    ADD CONSTRAINT [FK_Employees_UserInfoes] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Teachers_UserInfoes]...';


GO
ALTER TABLE [dbo].[Teachers] WITH NOCHECK
    ADD CONSTRAINT [FK_Teachers_UserInfoes] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Students_UserInfoes]...';


GO
ALTER TABLE [dbo].[Students] WITH NOCHECK
    ADD CONSTRAINT [FK_Students_UserInfoes] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Employees_Departments]...';


GO
ALTER TABLE [dbo].[Employees] WITH NOCHECK
    ADD CONSTRAINT [FK_Employees_Departments] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Departments] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Employees_Designations]...';


GO
ALTER TABLE [dbo].[Employees] WITH NOCHECK
    ADD CONSTRAINT [FK_Employees_Designations] FOREIGN KEY ([DesignationId]) REFERENCES [dbo].[Designations] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Employees_MaritalStatuses]...';


GO
ALTER TABLE [dbo].[Employees] WITH NOCHECK
    ADD CONSTRAINT [FK_Employees_MaritalStatuses] FOREIGN KEY ([MaritalStatusId]) REFERENCES [dbo].[MaritalStatuses] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Guardians_EducationalQualifications]...';


GO
ALTER TABLE [dbo].[Guardians] WITH NOCHECK
    ADD CONSTRAINT [FK_Guardians_EducationalQualifications] FOREIGN KEY ([EducationalQualificationId]) REFERENCES [dbo].[EducationalQualifications] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Guardians_GuardianTypes]...';


GO
ALTER TABLE [dbo].[Guardians] WITH NOCHECK
    ADD CONSTRAINT [FK_Guardians_GuardianTypes] FOREIGN KEY ([GuardianTypeId]) REFERENCES [dbo].[GuardianTypes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Guardians_Professions]...';


GO
ALTER TABLE [dbo].[Guardians] WITH NOCHECK
    ADD CONSTRAINT [FK_Guardians_Professions] FOREIGN KEY ([ProfessionId]) REFERENCES [dbo].[Professions] ([Id]);


GO
PRINT N'Creating [dbo].[FK_GuardiansOfStudents_Guardians]...';


GO
ALTER TABLE [dbo].[GuardiansOfStudents] WITH NOCHECK
    ADD CONSTRAINT [FK_GuardiansOfStudents_Guardians] FOREIGN KEY ([GuardianId]) REFERENCES [dbo].[Guardians] ([GuardianId]);


GO
PRINT N'Creating [dbo].[FK_Students_AcademicShifts]...';


GO
ALTER TABLE [dbo].[Students] WITH NOCHECK
    ADD CONSTRAINT [FK_Students_AcademicShifts] FOREIGN KEY ([CurrentAcademicShiftId]) REFERENCES [dbo].[AcademicShifts] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Students_AcademicVersions]...';


GO
ALTER TABLE [dbo].[Students] WITH NOCHECK
    ADD CONSTRAINT [FK_Students_AcademicVersions] FOREIGN KEY ([CurrentAcademicVerssionId]) REFERENCES [dbo].[AcademicVersions] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Students_AcademicBranches]...';


GO
ALTER TABLE [dbo].[Students] WITH NOCHECK
    ADD CONSTRAINT [FK_Students_AcademicBranches] FOREIGN KEY ([CurrentAcademicBranchId]) REFERENCES [dbo].[AcademicBranches] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Students_AcademicClasses]...';


GO
ALTER TABLE [dbo].[Students] WITH NOCHECK
    ADD CONSTRAINT [FK_Students_AcademicClasses] FOREIGN KEY ([CurrentAcademicClassId]) REFERENCES [dbo].[AcademicClasses] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Students_AcademicGroups]...';


GO
ALTER TABLE [dbo].[Students] WITH NOCHECK
    ADD CONSTRAINT [FK_Students_AcademicGroups] FOREIGN KEY ([CurrentAcademicGroupId]) REFERENCES [dbo].[AcademicGroups] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Students_AcademicSections]...';


GO
ALTER TABLE [dbo].[Students] WITH NOCHECK
    ADD CONSTRAINT [FK_Students_AcademicSections] FOREIGN KEY ([CurrentAcademicSectionId]) REFERENCES [dbo].[AcademicSections] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Students_AcademicSessions]...';


GO
ALTER TABLE [dbo].[Students] WITH NOCHECK
    ADD CONSTRAINT [FK_Students_AcademicSessions] FOREIGN KEY ([CurrentAcademicSessionId]) REFERENCES [dbo].[AcademicSessions] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Siblings_SiblingStudents]...';


GO
ALTER TABLE [dbo].[Siblings] WITH NOCHECK
    ADD CONSTRAINT [FK_Siblings_SiblingStudents] FOREIGN KEY ([SiblingId]) REFERENCES [dbo].[Students] ([StudentId]);


GO
PRINT N'Creating [dbo].[FK_Siblings_Students]...';


GO
ALTER TABLE [dbo].[Siblings] WITH NOCHECK
    ADD CONSTRAINT [FK_Siblings_Students] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([StudentId]);


GO
PRINT N'Creating [dbo].[FK_GuardiansOfStudents_Students]...';


GO
ALTER TABLE [dbo].[GuardiansOfStudents] WITH NOCHECK
    ADD CONSTRAINT [FK_GuardiansOfStudents_Students] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([StudentId]);


GO
PRINT N'Creating [dbo].[FK_StudentAttendances_Teachers]...';


GO
ALTER TABLE [dbo].[StudentAttendances] WITH NOCHECK
    ADD CONSTRAINT [FK_StudentAttendances_Teachers] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[Teachers] ([TeacherId]);


GO
PRINT N'Creating [dbo].[FK_Teachers_AcademicBranches]...';


GO
ALTER TABLE [dbo].[Teachers] WITH NOCHECK
    ADD CONSTRAINT [FK_Teachers_AcademicBranches] FOREIGN KEY ([CurrentAcademicBranchId]) REFERENCES [dbo].[AcademicBranches] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Teachers_Designations]...';


GO
ALTER TABLE [dbo].[Teachers] WITH NOCHECK
    ADD CONSTRAINT [FK_Teachers_Designations] FOREIGN KEY ([DesignationId]) REFERENCES [dbo].[Designations] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Teachers_MaritalStatuses]...';


GO
ALTER TABLE [dbo].[Teachers] WITH NOCHECK
    ADD CONSTRAINT [FK_Teachers_MaritalStatuses] FOREIGN KEY ([MaritalStatusId]) REFERENCES [dbo].[MaritalStatuses] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Notices_Institutes]...';


GO
ALTER TABLE [dbo].[Notices] WITH NOCHECK
    ADD CONSTRAINT [FK_Notices_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Notices_AcademicBranches]...';


GO
ALTER TABLE [dbo].[Notices] WITH NOCHECK
    ADD CONSTRAINT [FK_Notices_AcademicBranches] FOREIGN KEY ([AcademicBranchId]) REFERENCES [dbo].[AcademicBranches] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Notices_AcademicClasses]...';


GO
ALTER TABLE [dbo].[Notices] WITH NOCHECK
    ADD CONSTRAINT [FK_Notices_AcademicClasses] FOREIGN KEY ([AcademicClassId]) REFERENCES [dbo].[AcademicClasses] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Notices_AcademicSections]...';


GO
ALTER TABLE [dbo].[Notices] WITH NOCHECK
    ADD CONSTRAINT [FK_Notices_AcademicSections] FOREIGN KEY ([AcademicSectionId]) REFERENCES [dbo].[AcademicSections] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Notices_Students]...';


GO
ALTER TABLE [dbo].[Notices] WITH NOCHECK
    ADD CONSTRAINT [FK_Notices_Students] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([StudentId]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[UserInfoes] WITH CHECK CHECK CONSTRAINT [FK_UserInfoes_BloodGroups];

ALTER TABLE [dbo].[UserInfoes] WITH CHECK CHECK CONSTRAINT [FK_UserInfoes_Genders];

ALTER TABLE [dbo].[UserInfoes] WITH CHECK CHECK CONSTRAINT [FK_UserInfoes_Institutes];

ALTER TABLE [dbo].[UserInfoes] WITH CHECK CHECK CONSTRAINT [FK_UserInfoes_Nationalities];

ALTER TABLE [dbo].[UserInfoes] WITH CHECK CHECK CONSTRAINT [FK_UserInfoes_Religions];

ALTER TABLE [dbo].[Guardians] WITH CHECK CHECK CONSTRAINT [FK_Guardians_UserInfoes];

ALTER TABLE [dbo].[Employees] WITH CHECK CHECK CONSTRAINT [FK_Employees_UserInfoes];

ALTER TABLE [dbo].[Teachers] WITH CHECK CHECK CONSTRAINT [FK_Teachers_UserInfoes];

ALTER TABLE [dbo].[Students] WITH CHECK CHECK CONSTRAINT [FK_Students_UserInfoes];

ALTER TABLE [dbo].[Employees] WITH CHECK CHECK CONSTRAINT [FK_Employees_Departments];

ALTER TABLE [dbo].[Employees] WITH CHECK CHECK CONSTRAINT [FK_Employees_Designations];

ALTER TABLE [dbo].[Employees] WITH CHECK CHECK CONSTRAINT [FK_Employees_MaritalStatuses];

ALTER TABLE [dbo].[Guardians] WITH CHECK CHECK CONSTRAINT [FK_Guardians_EducationalQualifications];

ALTER TABLE [dbo].[Guardians] WITH CHECK CHECK CONSTRAINT [FK_Guardians_GuardianTypes];

ALTER TABLE [dbo].[Guardians] WITH CHECK CHECK CONSTRAINT [FK_Guardians_Professions];

ALTER TABLE [dbo].[GuardiansOfStudents] WITH CHECK CHECK CONSTRAINT [FK_GuardiansOfStudents_Guardians];

ALTER TABLE [dbo].[Students] WITH CHECK CHECK CONSTRAINT [FK_Students_AcademicShifts];

ALTER TABLE [dbo].[Students] WITH CHECK CHECK CONSTRAINT [FK_Students_AcademicVersions];

ALTER TABLE [dbo].[Students] WITH CHECK CHECK CONSTRAINT [FK_Students_AcademicBranches];

ALTER TABLE [dbo].[Students] WITH CHECK CHECK CONSTRAINT [FK_Students_AcademicClasses];

ALTER TABLE [dbo].[Students] WITH CHECK CHECK CONSTRAINT [FK_Students_AcademicGroups];

ALTER TABLE [dbo].[Students] WITH CHECK CHECK CONSTRAINT [FK_Students_AcademicSections];

ALTER TABLE [dbo].[Students] WITH CHECK CHECK CONSTRAINT [FK_Students_AcademicSessions];

ALTER TABLE [dbo].[Siblings] WITH CHECK CHECK CONSTRAINT [FK_Siblings_SiblingStudents];

ALTER TABLE [dbo].[Siblings] WITH CHECK CHECK CONSTRAINT [FK_Siblings_Students];

ALTER TABLE [dbo].[GuardiansOfStudents] WITH CHECK CHECK CONSTRAINT [FK_GuardiansOfStudents_Students];

ALTER TABLE [dbo].[StudentAttendances] WITH CHECK CHECK CONSTRAINT [FK_StudentAttendances_Teachers];

ALTER TABLE [dbo].[Teachers] WITH CHECK CHECK CONSTRAINT [FK_Teachers_AcademicBranches];

ALTER TABLE [dbo].[Teachers] WITH CHECK CHECK CONSTRAINT [FK_Teachers_Designations];

ALTER TABLE [dbo].[Teachers] WITH CHECK CHECK CONSTRAINT [FK_Teachers_MaritalStatuses];

ALTER TABLE [dbo].[Notices] WITH CHECK CHECK CONSTRAINT [FK_Notices_Institutes];

ALTER TABLE [dbo].[Notices] WITH CHECK CHECK CONSTRAINT [FK_Notices_AcademicBranches];

ALTER TABLE [dbo].[Notices] WITH CHECK CHECK CONSTRAINT [FK_Notices_AcademicClasses];

ALTER TABLE [dbo].[Notices] WITH CHECK CHECK CONSTRAINT [FK_Notices_AcademicSections];

ALTER TABLE [dbo].[Notices] WITH CHECK CHECK CONSTRAINT [FK_Notices_Students];


GO
PRINT N'Update complete.';


GO

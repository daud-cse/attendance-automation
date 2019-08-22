
USE PNSMS;

GO
/*
The column [dbo].[Rights].[EnableTeacherOrEmployee] is being dropped, data loss could occur.

The column [dbo].[Rights].[EnableEmployee] on table [dbo].[Rights] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.

The column [dbo].[Rights].[EnableTeacher] on table [dbo].[Rights] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[Rights])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[Roles].[IsForTeacherOrEmployee] is being dropped, data loss could occur.

The column [dbo].[Roles].[IsForEmployee] on table [dbo].[Roles] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.

The column [dbo].[Roles].[IsForTeacher] on table [dbo].[Roles] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[Roles])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_RightsOfPackages_Rights]...';


GO
ALTER TABLE [dbo].[RightsOfPackages] DROP CONSTRAINT [FK_RightsOfPackages_Rights];


GO
PRINT N'Dropping [dbo].[FK_RightsOfRoles_Rights]...';


GO
ALTER TABLE [dbo].[RightsOfRoles] DROP CONSTRAINT [FK_RightsOfRoles_Rights];


GO
PRINT N'Dropping [dbo].[FK_RightsOfRoles_Roles]...';


GO
ALTER TABLE [dbo].[RightsOfRoles] DROP CONSTRAINT [FK_RightsOfRoles_Roles];


GO
PRINT N'Dropping [dbo].[FK_Roles_GuardianTypes]...';


GO
ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [FK_Roles_GuardianTypes];


GO
PRINT N'Dropping [dbo].[FK_Roles_Institutes]...';


GO
ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [FK_Roles_Institutes];


GO
PRINT N'Dropping [dbo].[FK_RolesOfUserInfoes_Roles]...';


GO
ALTER TABLE [dbo].[RolesOfUserInfoes] DROP CONSTRAINT [FK_RolesOfUserInfoes_Roles];


GO
PRINT N'Dropping [dbo].[FK_AcademicBranchesOfUserInfoes_UserInfoes]...';


GO
ALTER TABLE [dbo].[AcademicBranchesOfUserInfoes] DROP CONSTRAINT [FK_AcademicBranchesOfUserInfoes_UserInfoes];


GO
PRINT N'Dropping [dbo].[FK_Employees_UserInfoes]...';


GO
ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_UserInfoes];


GO
PRINT N'Dropping [dbo].[FK_Guardians_UserInfoes]...';


GO
ALTER TABLE [dbo].[Guardians] DROP CONSTRAINT [FK_Guardians_UserInfoes];


GO
PRINT N'Dropping [dbo].[FK_Students_UserInfoes]...';


GO
ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_Students_UserInfoes];


GO
PRINT N'Dropping [dbo].[FK_UserInfoSecurities_UserInfoes]...';


GO
ALTER TABLE [dbo].[UserInfoSecurities] DROP CONSTRAINT [FK_UserInfoSecurities_UserInfoes];


GO
PRINT N'Dropping [dbo].[FK_UserInfoes_BloodGroups]...';


GO
ALTER TABLE [dbo].[UserInfoes] DROP CONSTRAINT [FK_UserInfoes_BloodGroups];


GO
PRINT N'Dropping [dbo].[FK_UserInfoes_Genders]...';


GO
ALTER TABLE [dbo].[UserInfoes] DROP CONSTRAINT [FK_UserInfoes_Genders];


GO
PRINT N'Dropping [dbo].[FK_UserInfoes_Nationalities]...';


GO
ALTER TABLE [dbo].[UserInfoes] DROP CONSTRAINT [FK_UserInfoes_Nationalities];


GO
PRINT N'Dropping [dbo].[FK_UserInfoes_Religions]...';


GO
ALTER TABLE [dbo].[UserInfoes] DROP CONSTRAINT [FK_UserInfoes_Religions];


GO
PRINT N'Dropping [dbo].[FK_UserInfoes_Institutes]...';


GO
ALTER TABLE [dbo].[UserInfoes] DROP CONSTRAINT [FK_UserInfoes_Institutes];


GO
PRINT N'Dropping [dbo].[FK_Teachers_UserInfoes]...';


GO
ALTER TABLE [dbo].[Teachers] DROP CONSTRAINT [FK_Teachers_UserInfoes];


GO
PRINT N'Dropping [dbo].[FK_RolesOfUserInfoes_UserInfoes]...';


GO
ALTER TABLE [dbo].[RolesOfUserInfoes] DROP CONSTRAINT [FK_RolesOfUserInfoes_UserInfoes];


GO
PRINT N'Starting rebuilding table [dbo].[Rights]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Rights] (
    [Id]             INT             NOT NULL,
    [Name]           NVARCHAR (128)  NOT NULL,
    [Code]           NVARCHAR (32)   NOT NULL,
    [Description]    NVARCHAR (1024) NULL,
    [EnableEmployee] BIT             NOT NULL,
    [EnableTeacher]  BIT             NOT NULL,
    [EnableStudent]  BIT             NOT NULL,
    [EnableGuardian] BIT             NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Rights] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_Rights_Code] UNIQUE NONCLUSTERED ([Code] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_Rights_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Rights])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Rights] ([Id], [Name], [Code], [Description], [EnableStudent], [EnableGuardian])
        SELECT   [Id],
                 [Name],
                 [Code],
                 [Description],
                 [EnableStudent],
                 [EnableGuardian]
        FROM     [dbo].[Rights]
        ORDER BY [Id] ASC;
    END

DROP TABLE [dbo].[Rights];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Rights]', N'Rights';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Rights]', N'PK_Rights', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_Rights_Code]', N'IX_Rights_Code', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_Rights_Name]', N'IX_Rights_Name', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[Roles]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Roles] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT            NOT NULL,
    [Name]           NVARCHAR (128) NOT NULL,
    [Description]    NVARCHAR (512) NULL,
    [IsForEmployee]  BIT            NOT NULL,
    [IsForTeacher]   BIT            NOT NULL,
    [IsForStudent]   BIT            NOT NULL,
    [IsForGuardian]  BIT            NOT NULL,
    [GuardianTypeId] INT            NULL,
    [IsActive]       BIT            NOT NULL,
    [LastUpdateTime] DATETIME       NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_Roles] UNIQUE NONCLUSTERED ([Name] ASC, [InstituteId] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Roles])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Roles] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Roles] ([Id], [InstituteId], [Name], [Description], [IsForStudent], [IsForGuardian], [GuardianTypeId], [IsActive], [LastUpdateTime])
        SELECT   [Id],
                 [InstituteId],
                 [Name],
                 [Description],
                 [IsForStudent],
                 [IsForGuardian],
                 [GuardianTypeId],
                 [IsActive],
                 [LastUpdateTime]
        FROM     [dbo].[Roles]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Roles] OFF;
    END

DROP TABLE [dbo].[Roles];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Roles]', N'Roles';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Roles]', N'PK_Roles', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_Roles]', N'IX_Roles', N'OBJECT';

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
    [UserInfoTypeId] INT            NULL,
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
        INSERT INTO [dbo].[tmp_ms_xx_UserInfoes] ([Id], [InstituteId], [PIN], [FirstName], [MiddleName], [LastName], [Name], [ContactNumber1], [ContactNumber2], [EmailAddress], [SSN], [PassportNo], [DOB], [GenderId], [NationalityId], [ReligionId], [BloodGroupId], [GoogleId], [FacebookId], [MicrosoftId], [TwitterId], [IsActive], [LastUpdateTime])
        SELECT   [Id],
                 [InstituteId],
                 [PIN],
                 [FirstName],
                 [MiddleName],
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
PRINT N'Creating [dbo].[_Migration]...';


GO
CREATE TABLE [dbo].[_Migration] (
    [LastUpdate] NCHAR (10) NOT NULL
);


GO
PRINT N'Creating [dbo].[UserInfoTypes]...';


GO
CREATE TABLE [dbo].[UserInfoTypes] (
    [Id]   INT           NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_UserInfoTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_RightsOfPackages_Rights]...';


GO
ALTER TABLE [dbo].[RightsOfPackages] WITH NOCHECK
    ADD CONSTRAINT [FK_RightsOfPackages_Rights] FOREIGN KEY ([RightId]) REFERENCES [dbo].[Rights] ([Id]);


GO
PRINT N'Creating [dbo].[FK_RightsOfRoles_Rights]...';


GO
ALTER TABLE [dbo].[RightsOfRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_RightsOfRoles_Rights] FOREIGN KEY ([RightId]) REFERENCES [dbo].[Rights] ([Id]);


GO
PRINT N'Creating [dbo].[FK_RightsOfRoles_Roles]...';


GO
ALTER TABLE [dbo].[RightsOfRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_RightsOfRoles_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Roles_GuardianTypes]...';


GO
ALTER TABLE [dbo].[Roles] WITH NOCHECK
    ADD CONSTRAINT [FK_Roles_GuardianTypes] FOREIGN KEY ([GuardianTypeId]) REFERENCES [dbo].[GuardianTypes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Roles_Institutes]...';


GO
ALTER TABLE [dbo].[Roles] WITH NOCHECK
    ADD CONSTRAINT [FK_Roles_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_RolesOfUserInfoes_Roles]...';


GO
ALTER TABLE [dbo].[RolesOfUserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_RolesOfUserInfoes_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AcademicBranchesOfUserInfoes_UserInfoes]...';


GO
ALTER TABLE [dbo].[AcademicBranchesOfUserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_AcademicBranchesOfUserInfoes_UserInfoes] FOREIGN KEY ([UserInfoId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Employees_UserInfoes]...';


GO
ALTER TABLE [dbo].[Employees] WITH NOCHECK
    ADD CONSTRAINT [FK_Employees_UserInfoes] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Guardians_UserInfoes]...';


GO
ALTER TABLE [dbo].[Guardians] WITH NOCHECK
    ADD CONSTRAINT [FK_Guardians_UserInfoes] FOREIGN KEY ([GuardianId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Students_UserInfoes]...';


GO
ALTER TABLE [dbo].[Students] WITH NOCHECK
    ADD CONSTRAINT [FK_Students_UserInfoes] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserInfoSecurities_UserInfoes]...';


GO
ALTER TABLE [dbo].[UserInfoSecurities] WITH NOCHECK
    ADD CONSTRAINT [FK_UserInfoSecurities_UserInfoes] FOREIGN KEY ([UserInfoId]) REFERENCES [dbo].[UserInfoes] ([Id]);


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
PRINT N'Creating [dbo].[FK_UserInfoes_Institutes]...';


GO
ALTER TABLE [dbo].[UserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_UserInfoes_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Teachers_UserInfoes]...';


GO
ALTER TABLE [dbo].[Teachers] WITH NOCHECK
    ADD CONSTRAINT [FK_Teachers_UserInfoes] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_RolesOfUserInfoes_UserInfoes]...';


GO
ALTER TABLE [dbo].[RolesOfUserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_RolesOfUserInfoes_UserInfoes] FOREIGN KEY ([UserInfoId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserInfoes_UserInfoTypes]...';


GO
ALTER TABLE [dbo].[UserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_UserInfoes_UserInfoTypes] FOREIGN KEY ([UserInfoTypeId]) REFERENCES [dbo].[UserInfoTypes] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[RightsOfPackages] WITH CHECK CHECK CONSTRAINT [FK_RightsOfPackages_Rights];

ALTER TABLE [dbo].[RightsOfRoles] WITH CHECK CHECK CONSTRAINT [FK_RightsOfRoles_Rights];

ALTER TABLE [dbo].[RightsOfRoles] WITH CHECK CHECK CONSTRAINT [FK_RightsOfRoles_Roles];

ALTER TABLE [dbo].[Roles] WITH CHECK CHECK CONSTRAINT [FK_Roles_GuardianTypes];

ALTER TABLE [dbo].[Roles] WITH CHECK CHECK CONSTRAINT [FK_Roles_Institutes];

ALTER TABLE [dbo].[RolesOfUserInfoes] WITH CHECK CHECK CONSTRAINT [FK_RolesOfUserInfoes_Roles];

ALTER TABLE [dbo].[AcademicBranchesOfUserInfoes] WITH CHECK CHECK CONSTRAINT [FK_AcademicBranchesOfUserInfoes_UserInfoes];

ALTER TABLE [dbo].[Employees] WITH CHECK CHECK CONSTRAINT [FK_Employees_UserInfoes];

ALTER TABLE [dbo].[Guardians] WITH CHECK CHECK CONSTRAINT [FK_Guardians_UserInfoes];

ALTER TABLE [dbo].[Students] WITH CHECK CHECK CONSTRAINT [FK_Students_UserInfoes];

ALTER TABLE [dbo].[UserInfoSecurities] WITH CHECK CHECK CONSTRAINT [FK_UserInfoSecurities_UserInfoes];

ALTER TABLE [dbo].[UserInfoes] WITH CHECK CHECK CONSTRAINT [FK_UserInfoes_BloodGroups];

ALTER TABLE [dbo].[UserInfoes] WITH CHECK CHECK CONSTRAINT [FK_UserInfoes_Genders];

ALTER TABLE [dbo].[UserInfoes] WITH CHECK CHECK CONSTRAINT [FK_UserInfoes_Nationalities];

ALTER TABLE [dbo].[UserInfoes] WITH CHECK CHECK CONSTRAINT [FK_UserInfoes_Religions];

ALTER TABLE [dbo].[UserInfoes] WITH CHECK CHECK CONSTRAINT [FK_UserInfoes_Institutes];

ALTER TABLE [dbo].[Teachers] WITH CHECK CHECK CONSTRAINT [FK_Teachers_UserInfoes];

ALTER TABLE [dbo].[RolesOfUserInfoes] WITH CHECK CHECK CONSTRAINT [FK_RolesOfUserInfoes_UserInfoes];

ALTER TABLE [dbo].[UserInfoes] WITH CHECK CHECK CONSTRAINT [FK_UserInfoes_UserInfoTypes];

insert into dbo._Migration values('08')

/****** Object:  Table [dbo].[Rights]    Script Date: 04/28/2015 02:37:29 ******/
INSERT [dbo].[Rights] ([Id], [Name], [Code], [Description], [EnableEmployee], [EnableTeacher], [EnableStudent], [EnableGuardian]) VALUES (1, N'Attendanse entry by teacher', N'0101', N'Attendanse entry by teacher', 0, 1, 0, 0)
INSERT [dbo].[Rights] ([Id], [Name], [Code], [Description], [EnableEmployee], [EnableTeacher], [EnableStudent], [EnableGuardian]) VALUES (2, N'Attendanse entry by admin', N'0102', N'Attendanse entry by officer or senior teacher', 1, 1, 0, 0)
INSERT [dbo].[Rights] ([Id], [Name], [Code], [Description], [EnableEmployee], [EnableTeacher], [EnableStudent], [EnableGuardian]) VALUES (3, N'Student Information view', N'0202', N'Student Information view', 1, 1, 0, 1)
INSERT [dbo].[Rights] ([Id], [Name], [Code], [Description], [EnableEmployee], [EnableTeacher], [EnableStudent], [EnableGuardian]) VALUES (6, N'Student enrollmant', N'0201', N'Student enrollmant', 1, 1, 0, 0)
INSERT [dbo].[Rights] ([Id], [Name], [Code], [Description], [EnableEmployee], [EnableTeacher], [EnableStudent], [EnableGuardian]) VALUES (7, N'Employee add edit', N'0301', N'Employee add edit', 1, 1, 0, 0)
INSERT [dbo].[Rights] ([Id], [Name], [Code], [Description], [EnableEmployee], [EnableTeacher], [EnableStudent], [EnableGuardian]) VALUES (8, N'Employee view', N'0302', N'Employee view', 1, 1, 0, 0)
INSERT [dbo].[Rights] ([Id], [Name], [Code], [Description], [EnableEmployee], [EnableTeacher], [EnableStudent], [EnableGuardian]) VALUES (9, N'Teacher add, edit', N'0401', N'Teacher add, edit', 1, 1, 0, 0)
INSERT [dbo].[Rights] ([Id], [Name], [Code], [Description], [EnableEmployee], [EnableTeacher], [EnableStudent], [EnableGuardian]) VALUES (10, N'Master setup', N'0501', N'Master setup', 1, 1, 0, 0)
INSERT [dbo].[Rights] ([Id], [Name], [Code], [Description], [EnableEmployee], [EnableTeacher], [EnableStudent], [EnableGuardian]) VALUES (11, N'Notice add, edit', N'0601', N'Notice add, edit', 1, 1, 0, 0)
INSERT [dbo].[Rights] ([Id], [Name], [Code], [Description], [EnableEmployee], [EnableTeacher], [EnableStudent], [EnableGuardian]) VALUES (13, N'Events add, edit', N'0602', N'Events add, edit', 1, 1, 0, 0)
INSERT [dbo].[Rights] ([Id], [Name], [Code], [Description], [EnableEmployee], [EnableTeacher], [EnableStudent], [EnableGuardian]) VALUES (14, N'Gallery add, edit', N'0603', N'Gallery add, edit', 1, 1, 0, 0)
INSERT [dbo].[Rights] ([Id], [Name], [Code], [Description], [EnableEmployee], [EnableTeacher], [EnableStudent], [EnableGuardian]) VALUES (15, N'Notice view', N'0604', N'Notice view', 1, 1, 1, 1)
INSERT [dbo].[Rights] ([Id], [Name], [Code], [Description], [EnableEmployee], [EnableTeacher], [EnableStudent], [EnableGuardian]) VALUES (16, N'Event view', N'0605', N'Event view', 1, 1, 1, 1)
INSERT [dbo].[Rights] ([Id], [Name], [Code], [Description], [EnableEmployee], [EnableTeacher], [EnableStudent], [EnableGuardian]) VALUES (17, N'Gallery view', N'0606', N'Gallery view', 1, 1, 1, 1)
INSERT [dbo].[Rights] ([Id], [Name], [Code], [Description], [EnableEmployee], [EnableTeacher], [EnableStudent], [EnableGuardian]) VALUES (18, N'Teacher view', N'0402', N'Teacher view', 1, 1, 0, 0)
/****** Object:  Table [dbo].[Roles]    Script Date: 04/28/2015 02:37:29 ******/
SET IDENTITY_INSERT [dbo].[Roles] ON
INSERT [dbo].[Roles] ([Id], [InstituteId], [Name], [Description], [IsForEmployee], [IsForTeacher], [IsForStudent], [IsForGuardian], [GuardianTypeId], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'General Teacher', NULL, 0, 1, 0, 0, NULL, 1, CAST(0x0000A41300000000 AS DateTime))
INSERT [dbo].[Roles] ([Id], [InstituteId], [Name], [Description], [IsForEmployee], [IsForTeacher], [IsForStudent], [IsForGuardian], [GuardianTypeId], [IsActive], [LastUpdateTime]) VALUES (2, 1, N'Principle', NULL, 0, 1, 0, 0, NULL, 1, CAST(0x0000A41300000000 AS DateTime))
INSERT [dbo].[Roles] ([Id], [InstituteId], [Name], [Description], [IsForEmployee], [IsForTeacher], [IsForStudent], [IsForGuardian], [GuardianTypeId], [IsActive], [LastUpdateTime]) VALUES (3, 1, N'Employee', NULL, 1, 0, 0, 0, NULL, 1, CAST(0x0000A41300000000 AS DateTime))
INSERT [dbo].[Roles] ([Id], [InstituteId], [Name], [Description], [IsForEmployee], [IsForTeacher], [IsForStudent], [IsForGuardian], [GuardianTypeId], [IsActive], [LastUpdateTime]) VALUES (4, 1, N'School Admin', NULL, 1, 0, 0, 0, NULL, 1, CAST(0x0000A41300000000 AS DateTime))
INSERT [dbo].[Roles] ([Id], [InstituteId], [Name], [Description], [IsForEmployee], [IsForTeacher], [IsForStudent], [IsForGuardian], [GuardianTypeId], [IsActive], [LastUpdateTime]) VALUES (5, 1, N'Student', NULL, 0, 0, 1, 0, NULL, 1, CAST(0x0000A41300000000 AS DateTime))
INSERT [dbo].[Roles] ([Id], [InstituteId], [Name], [Description], [IsForEmployee], [IsForTeacher], [IsForStudent], [IsForGuardian], [GuardianTypeId], [IsActive], [LastUpdateTime]) VALUES (6, 1, N'Guardian', NULL, 0, 0, 0, 1, NULL, 1, CAST(0x0000A41300000000 AS DateTime))
INSERT [dbo].[Roles] ([Id], [InstituteId], [Name], [Description], [IsForEmployee], [IsForTeacher], [IsForStudent], [IsForGuardian], [GuardianTypeId], [IsActive], [LastUpdateTime]) VALUES (7, 1, N'House Tutor', NULL, 0, 0, 0, 1, 3, 1, CAST(0x0000A41300000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[Roles] OFF
/****** Object:  Table [dbo].[RightsOfRoles]    Script Date: 04/28/2015 02:37:29 ******/
SET IDENTITY_INSERT [dbo].[RightsOfRoles] ON
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (1, 1, 1)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (6, 2, 2)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (2, 1, 3)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (8, 2, 3)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (11, 3, 3)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (27, 6, 3)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (7, 2, 6)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (12, 3, 6)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (13, 3, 7)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (9, 2, 8)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (14, 3, 8)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (15, 3, 9)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (23, 4, 10)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (17, 3, 11)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (18, 3, 13)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (19, 3, 14)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (3, 1, 15)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (20, 3, 15)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (24, 5, 15)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (29, 6, 15)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (32, 7, 15)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (4, 1, 16)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (21, 3, 16)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (25, 5, 16)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (30, 6, 16)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (33, 7, 16)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (5, 1, 17)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (22, 3, 17)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (26, 5, 17)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (31, 6, 17)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (34, 7, 17)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (10, 2, 18)
INSERT [dbo].[RightsOfRoles] ([Id], [RoleId], [RightId]) VALUES (16, 3, 18)
SET IDENTITY_INSERT [dbo].[RightsOfRoles] OFF


INSERT [dbo].[UserInfoTypes] ([Id], [Name]) VALUES (11, N'Student')
INSERT [dbo].[UserInfoTypes] ([Id], [Name]) VALUES (12, N'Guardian')
INSERT [dbo].[UserInfoTypes] ([Id], [Name]) VALUES (13, N'Teacher')
INSERT [dbo].[UserInfoTypes] ([Id], [Name]) VALUES (14, N'Employee')

GO
PRINT N'Update complete.';


GO

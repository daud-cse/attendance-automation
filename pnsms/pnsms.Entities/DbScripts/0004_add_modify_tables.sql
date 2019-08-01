
USE PNSMS;


GO
/*
The column [dbo].[Institutes].[About] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Institutes])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_Institutes_Packages]...';


GO
ALTER TABLE [dbo].[Institutes] DROP CONSTRAINT [FK_Institutes_Packages];


GO
PRINT N'Dropping [dbo].[FK_Religions_Institutes]...';


GO
ALTER TABLE [dbo].[Religions] DROP CONSTRAINT [FK_Religions_Institutes];


GO
PRINT N'Dropping [dbo].[FK_Professions_Institutes]...';


GO
ALTER TABLE [dbo].[Professions] DROP CONSTRAINT [FK_Professions_Institutes];


GO
PRINT N'Dropping [dbo].[FK_AcademicVersions_Institutes]...';


GO
ALTER TABLE [dbo].[AcademicVersions] DROP CONSTRAINT [FK_AcademicVersions_Institutes];


GO
PRINT N'Dropping [dbo].[FK_AcademicShifts_Institutes]...';


GO
ALTER TABLE [dbo].[AcademicShifts] DROP CONSTRAINT [FK_AcademicShifts_Institutes];


GO
PRINT N'Dropping [dbo].[FK_AcademicSessions_Institutes]...';


GO
ALTER TABLE [dbo].[AcademicSessions] DROP CONSTRAINT [FK_AcademicSessions_Institutes];


GO
PRINT N'Dropping [dbo].[FK_AcademicSections_Institutes]...';


GO
ALTER TABLE [dbo].[AcademicSections] DROP CONSTRAINT [FK_AcademicSections_Institutes];


GO
PRINT N'Dropping [dbo].[FK_AcademicGroups_Institutes]...';


GO
ALTER TABLE [dbo].[AcademicGroups] DROP CONSTRAINT [FK_AcademicGroups_Institutes];


GO
PRINT N'Dropping [dbo].[FK_AcademicClasses_Institutes]...';


GO
ALTER TABLE [dbo].[AcademicClasses] DROP CONSTRAINT [FK_AcademicClasses_Institutes];


GO
PRINT N'Dropping [dbo].[FK_UserInfoes_Institutes]...';


GO
ALTER TABLE [dbo].[UserInfoes] DROP CONSTRAINT [FK_UserInfoes_Institutes];


GO
PRINT N'Dropping [dbo].[FK_AcademicBranches_Institutes]...';


GO
ALTER TABLE [dbo].[AcademicBranches] DROP CONSTRAINT [FK_AcademicBranches_Institutes];


GO
PRINT N'Dropping [dbo].[FK_Notices_Institutes]...';


GO
ALTER TABLE [dbo].[Notices] DROP CONSTRAINT [FK_Notices_Institutes];


GO
PRINT N'Dropping [dbo].[FK_BloodGroups_Institutes]...';


GO
ALTER TABLE [dbo].[BloodGroups] DROP CONSTRAINT [FK_BloodGroups_Institutes];


GO
PRINT N'Dropping [dbo].[FK_AttendanceTypes_Institutes]...';


GO
ALTER TABLE [dbo].[AttendanceTypes] DROP CONSTRAINT [FK_AttendanceTypes_Institutes];


GO
PRINT N'Dropping [dbo].[FK_AddressTypes_Institutes]...';


GO
ALTER TABLE [dbo].[AddressTypes] DROP CONSTRAINT [FK_AddressTypes_Institutes];


GO
PRINT N'Dropping [dbo].[FK_Designations_Institutes]...';


GO
ALTER TABLE [dbo].[Designations] DROP CONSTRAINT [FK_Designations_Institutes];


GO
PRINT N'Dropping [dbo].[FK_Departments_Institutes]...';


GO
ALTER TABLE [dbo].[Departments] DROP CONSTRAINT [FK_Departments_Institutes];


GO
PRINT N'Dropping [dbo].[FK_Countries_Institutes]...';


GO
ALTER TABLE [dbo].[Countries] DROP CONSTRAINT [FK_Countries_Institutes];


GO
PRINT N'Dropping [dbo].[FK_EducationalQualifications_Institutes]...';


GO
ALTER TABLE [dbo].[EducationalQualifications] DROP CONSTRAINT [FK_EducationalQualifications_Institutes];


GO
PRINT N'Dropping [dbo].[FK_GuardianTypes_Institutes]...';


GO
ALTER TABLE [dbo].[GuardianTypes] DROP CONSTRAINT [FK_GuardianTypes_Institutes];


GO
PRINT N'Dropping [dbo].[FK_Nationalities_Institutes]...';


GO
ALTER TABLE [dbo].[Nationalities] DROP CONSTRAINT [FK_Nationalities_Institutes];


GO
PRINT N'Dropping [dbo].[FK_MaritalStatuses_Institutes]...';


GO
ALTER TABLE [dbo].[MaritalStatuses] DROP CONSTRAINT [FK_MaritalStatuses_Institutes];


GO
PRINT N'Dropping [dbo].[FK_Genders_Institutes]...';


GO
ALTER TABLE [dbo].[Genders] DROP CONSTRAINT [FK_Genders_Institutes];


GO
PRINT N'Dropping [dbo].[FK_Testimonials_Institutes]...';


GO
ALTER TABLE [dbo].[Testimonials] DROP CONSTRAINT [FK_Testimonials_Institutes];


GO
PRINT N'Dropping [dbo].[FK_Teachers_UserInfoes]...';


GO
ALTER TABLE [dbo].[Teachers] DROP CONSTRAINT [FK_Teachers_UserInfoes];


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
PRINT N'Dropping [dbo].[FK_StudentAttendances_Teachers]...';


GO
ALTER TABLE [dbo].[StudentAttendances] DROP CONSTRAINT [FK_StudentAttendances_Teachers];


GO
PRINT N'Dropping [dbo].[IX_Images]...';


GO
ALTER TABLE [dbo].[Images] DROP CONSTRAINT [IX_Images];


GO
PRINT N'Altering [dbo].[AttendanceTypes]...';


GO
ALTER TABLE [dbo].[AttendanceTypes]
    ADD [IsPresent] BIT NULL;


GO
PRINT N'Starting rebuilding table [dbo].[Images]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Images] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [RefCode]         NCHAR (16)      NOT NULL,
    [RefPrimaryKey]   INT             NOT NULL,
    [ImageBinaryData] VARBINARY (MAX) NULL,
    [ImageCaption]    NVARCHAR (MAX)  NULL,
    [IsActive]        BIT             NOT NULL,
    [LastUpdatedTime] DATETIME        NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Images] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Images])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Images] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Images] ([Id], [RefCode], [RefPrimaryKey], [ImageBinaryData], [IsActive], [LastUpdatedTime])
        SELECT   [Id],
                 [RefCode],
                 [RefPrimaryKey],
                 [ImageBinaryData],
                 [IsActive],
                 [LastUpdatedTime]
        FROM     [dbo].[Images]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Images] OFF;
    END

DROP TABLE [dbo].[Images];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Images]', N'Images';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Images]', N'PK_Images', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Images].[IX_Images]...';


GO
CREATE NONCLUSTERED INDEX [IX_Images]
    ON [dbo].[Images]([RefCode] ASC, [RefPrimaryKey] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[Institutes]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Institutes] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (256) NOT NULL,
    [PackageId]      INT            NOT NULL,
    [Code]           NVARCHAR (128) NULL,
    [Url]            NVARCHAR (512) NULL,
    [latitude]       DECIMAL (9, 6) NULL,
    [longitude]      DECIMAL (9, 6) NULL,
    [SeoText]        VARCHAR (MAX)  NULL,
    [WelComeText]    VARCHAR (MAX)  NULL,
    [ContactText]    VARCHAR (MAX)  NULL,
    [FacebookUrl]    NVARCHAR (256) NULL,
    [TwitterUrl]     NVARCHAR (256) NULL,
    [GoogleUrl]      NVARCHAR (256) NULL,
    [LinkedinUrl]    NVARCHAR (256) NULL,
    [BehanceUrl]     NVARCHAR (256) NULL,
    [IsActive]       BIT            NOT NULL,
    [LastUpdateTime] DATETIME       NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Institutes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Institutes])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Institutes] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Institutes] ([Id], [Name], [PackageId], [Code], [SeoText], [latitude], [longitude], [WelComeText], [FacebookUrl], [TwitterUrl], [GoogleUrl], [LinkedinUrl], [BehanceUrl], [IsActive], [LastUpdateTime])
        SELECT   [Id],
                 [Name],
                 [PackageId],
                 [Code],
                 [SeoText],
                 [latitude],
                 [longitude],
                 [WelComeText],
                 [FacebookUrl],
                 [TwitterUrl],
                 [GoogleUrl],
                 [LinkedinUrl],
                 [BehanceUrl],
                 [IsActive],
                 [LastUpdateTime]
        FROM     [dbo].[Institutes]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Institutes] OFF;
    END

DROP TABLE [dbo].[Institutes];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Institutes]', N'Institutes';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Institutes]', N'PK_Institutes', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Altering [dbo].[StudentAttendances]...';


GO
ALTER TABLE [dbo].[StudentAttendances]
    ADD [PresentCount] INT NULL,
        [AbsentCount]  INT NULL;


GO
PRINT N'Starting rebuilding table [dbo].[Teachers]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Teachers] (
    [TeacherId]                INT            NOT NULL,
    [FatherName]               NVARCHAR (128) NULL,
    [MotherName]               NVARCHAR (128) NULL,
    [MaritalStatusId]          INT            NULL,
    [DesignationId]            INT            NULL,
    [CurrentAcademicBranchId]  INT            NULL,
    [DefaultAcademicClassId]   INT            NULL,
    [DefaultAcademicSectionId] INT            NULL,
    [LastUpdateTime]           DATETIME       NULL,
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
PRINT N'Creating [dbo].[Events]...';


GO
CREATE TABLE [dbo].[Events] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT            NOT NULL,
    [StartDate]      DATE           NULL,
    [EndDate]        DATE           NULL,
    [EventStartAt]   DATETIME       NULL,
    [EventEndAt]     DATETIME       NULL,
    [EventTitle]     NVARCHAR (512) NULL,
    [EventBody]      NVARCHAR (MAX) NULL,
    [EventBriefInfo] NVARCHAR (MAX) NULL,
    [IsActive]       BIT            NOT NULL,
    [LastUpdateTime] DATETIME       NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Galleries]...';


GO
CREATE TABLE [dbo].[Galleries] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]     INT            NOT NULL,
    [EventId]         INT            NULL,
    [StartDate]       DATE           NULL,
    [EndDate]         DATE           NULL,
    [GalleryTitle]    NVARCHAR (512) NULL,
    [GallerySubtitle] NVARCHAR (512) NULL,
    [IsActive]        BIT            NOT NULL,
    [LastUpdateTime]  DATETIME       NULL,
    CONSTRAINT [PK_Galleries] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_Institutes_Packages]...';


GO
ALTER TABLE [dbo].[Institutes] WITH NOCHECK
    ADD CONSTRAINT [FK_Institutes_Packages] FOREIGN KEY ([PackageId]) REFERENCES [dbo].[Packages] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Religions_Institutes]...';


GO
ALTER TABLE [dbo].[Religions] WITH NOCHECK
    ADD CONSTRAINT [FK_Religions_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Professions_Institutes]...';


GO
ALTER TABLE [dbo].[Professions] WITH NOCHECK
    ADD CONSTRAINT [FK_Professions_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AcademicVersions_Institutes]...';


GO
ALTER TABLE [dbo].[AcademicVersions] WITH NOCHECK
    ADD CONSTRAINT [FK_AcademicVersions_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AcademicShifts_Institutes]...';


GO
ALTER TABLE [dbo].[AcademicShifts] WITH NOCHECK
    ADD CONSTRAINT [FK_AcademicShifts_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AcademicSessions_Institutes]...';


GO
ALTER TABLE [dbo].[AcademicSessions] WITH NOCHECK
    ADD CONSTRAINT [FK_AcademicSessions_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AcademicSections_Institutes]...';


GO
ALTER TABLE [dbo].[AcademicSections] WITH NOCHECK
    ADD CONSTRAINT [FK_AcademicSections_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AcademicGroups_Institutes]...';


GO
ALTER TABLE [dbo].[AcademicGroups] WITH NOCHECK
    ADD CONSTRAINT [FK_AcademicGroups_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AcademicClasses_Institutes]...';


GO
ALTER TABLE [dbo].[AcademicClasses] WITH NOCHECK
    ADD CONSTRAINT [FK_AcademicClasses_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserInfoes_Institutes]...';


GO
ALTER TABLE [dbo].[UserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_UserInfoes_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AcademicBranches_Institutes]...';


GO
ALTER TABLE [dbo].[AcademicBranches] WITH NOCHECK
    ADD CONSTRAINT [FK_AcademicBranches_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Notices_Institutes]...';


GO
ALTER TABLE [dbo].[Notices] WITH NOCHECK
    ADD CONSTRAINT [FK_Notices_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_BloodGroups_Institutes]...';


GO
ALTER TABLE [dbo].[BloodGroups] WITH NOCHECK
    ADD CONSTRAINT [FK_BloodGroups_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AttendanceTypes_Institutes]...';


GO
ALTER TABLE [dbo].[AttendanceTypes] WITH NOCHECK
    ADD CONSTRAINT [FK_AttendanceTypes_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AddressTypes_Institutes]...';


GO
ALTER TABLE [dbo].[AddressTypes] WITH NOCHECK
    ADD CONSTRAINT [FK_AddressTypes_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Designations_Institutes]...';


GO
ALTER TABLE [dbo].[Designations] WITH NOCHECK
    ADD CONSTRAINT [FK_Designations_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Departments_Institutes]...';


GO
ALTER TABLE [dbo].[Departments] WITH NOCHECK
    ADD CONSTRAINT [FK_Departments_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Countries_Institutes]...';


GO
ALTER TABLE [dbo].[Countries] WITH NOCHECK
    ADD CONSTRAINT [FK_Countries_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_EducationalQualifications_Institutes]...';


GO
ALTER TABLE [dbo].[EducationalQualifications] WITH NOCHECK
    ADD CONSTRAINT [FK_EducationalQualifications_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_GuardianTypes_Institutes]...';


GO
ALTER TABLE [dbo].[GuardianTypes] WITH NOCHECK
    ADD CONSTRAINT [FK_GuardianTypes_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Nationalities_Institutes]...';


GO
ALTER TABLE [dbo].[Nationalities] WITH NOCHECK
    ADD CONSTRAINT [FK_Nationalities_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_MaritalStatuses_Institutes]...';


GO
ALTER TABLE [dbo].[MaritalStatuses] WITH NOCHECK
    ADD CONSTRAINT [FK_MaritalStatuses_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Genders_Institutes]...';


GO
ALTER TABLE [dbo].[Genders] WITH NOCHECK
    ADD CONSTRAINT [FK_Genders_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Testimonials_Institutes]...';


GO
ALTER TABLE [dbo].[Testimonials] WITH NOCHECK
    ADD CONSTRAINT [FK_Testimonials_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Teachers_UserInfoes]...';


GO
ALTER TABLE [dbo].[Teachers] WITH NOCHECK
    ADD CONSTRAINT [FK_Teachers_UserInfoes] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[UserInfoes] ([Id]);


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
PRINT N'Creating [dbo].[FK_StudentAttendances_Teachers]...';


GO
ALTER TABLE [dbo].[StudentAttendances] WITH NOCHECK
    ADD CONSTRAINT [FK_StudentAttendances_Teachers] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[Teachers] ([TeacherId]);


GO
PRINT N'Creating [dbo].[FK_Events_Institutes]...';


GO
ALTER TABLE [dbo].[Events] WITH NOCHECK
    ADD CONSTRAINT [FK_Events_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Galleries_Institutes]...';


GO
ALTER TABLE [dbo].[Galleries] WITH NOCHECK
    ADD CONSTRAINT [FK_Galleries_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Galleries_Events]...';


GO
ALTER TABLE [dbo].[Galleries] WITH NOCHECK
    ADD CONSTRAINT [FK_Galleries_Events] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[Institutes] WITH CHECK CHECK CONSTRAINT [FK_Institutes_Packages];

ALTER TABLE [dbo].[Religions] WITH CHECK CHECK CONSTRAINT [FK_Religions_Institutes];

ALTER TABLE [dbo].[Professions] WITH CHECK CHECK CONSTRAINT [FK_Professions_Institutes];

ALTER TABLE [dbo].[AcademicVersions] WITH CHECK CHECK CONSTRAINT [FK_AcademicVersions_Institutes];

ALTER TABLE [dbo].[AcademicShifts] WITH CHECK CHECK CONSTRAINT [FK_AcademicShifts_Institutes];

ALTER TABLE [dbo].[AcademicSessions] WITH CHECK CHECK CONSTRAINT [FK_AcademicSessions_Institutes];

ALTER TABLE [dbo].[AcademicSections] WITH CHECK CHECK CONSTRAINT [FK_AcademicSections_Institutes];

ALTER TABLE [dbo].[AcademicGroups] WITH CHECK CHECK CONSTRAINT [FK_AcademicGroups_Institutes];

ALTER TABLE [dbo].[AcademicClasses] WITH CHECK CHECK CONSTRAINT [FK_AcademicClasses_Institutes];

ALTER TABLE [dbo].[UserInfoes] WITH CHECK CHECK CONSTRAINT [FK_UserInfoes_Institutes];

ALTER TABLE [dbo].[AcademicBranches] WITH CHECK CHECK CONSTRAINT [FK_AcademicBranches_Institutes];

ALTER TABLE [dbo].[Notices] WITH CHECK CHECK CONSTRAINT [FK_Notices_Institutes];

ALTER TABLE [dbo].[BloodGroups] WITH CHECK CHECK CONSTRAINT [FK_BloodGroups_Institutes];

ALTER TABLE [dbo].[AttendanceTypes] WITH CHECK CHECK CONSTRAINT [FK_AttendanceTypes_Institutes];

ALTER TABLE [dbo].[AddressTypes] WITH CHECK CHECK CONSTRAINT [FK_AddressTypes_Institutes];

ALTER TABLE [dbo].[Designations] WITH CHECK CHECK CONSTRAINT [FK_Designations_Institutes];

ALTER TABLE [dbo].[Departments] WITH CHECK CHECK CONSTRAINT [FK_Departments_Institutes];

ALTER TABLE [dbo].[Countries] WITH CHECK CHECK CONSTRAINT [FK_Countries_Institutes];

ALTER TABLE [dbo].[EducationalQualifications] WITH CHECK CHECK CONSTRAINT [FK_EducationalQualifications_Institutes];

ALTER TABLE [dbo].[GuardianTypes] WITH CHECK CHECK CONSTRAINT [FK_GuardianTypes_Institutes];

ALTER TABLE [dbo].[Nationalities] WITH CHECK CHECK CONSTRAINT [FK_Nationalities_Institutes];

ALTER TABLE [dbo].[MaritalStatuses] WITH CHECK CHECK CONSTRAINT [FK_MaritalStatuses_Institutes];

ALTER TABLE [dbo].[Genders] WITH CHECK CHECK CONSTRAINT [FK_Genders_Institutes];

ALTER TABLE [dbo].[Testimonials] WITH CHECK CHECK CONSTRAINT [FK_Testimonials_Institutes];

ALTER TABLE [dbo].[Teachers] WITH CHECK CHECK CONSTRAINT [FK_Teachers_UserInfoes];

ALTER TABLE [dbo].[Teachers] WITH CHECK CHECK CONSTRAINT [FK_Teachers_AcademicBranches];

ALTER TABLE [dbo].[Teachers] WITH CHECK CHECK CONSTRAINT [FK_Teachers_Designations];

ALTER TABLE [dbo].[Teachers] WITH CHECK CHECK CONSTRAINT [FK_Teachers_MaritalStatuses];

ALTER TABLE [dbo].[StudentAttendances] WITH CHECK CHECK CONSTRAINT [FK_StudentAttendances_Teachers];

ALTER TABLE [dbo].[Events] WITH CHECK CHECK CONSTRAINT [FK_Events_Institutes];

ALTER TABLE [dbo].[Galleries] WITH CHECK CHECK CONSTRAINT [FK_Galleries_Institutes];

ALTER TABLE [dbo].[Galleries] WITH CHECK CHECK CONSTRAINT [FK_Galleries_Events];


GO
PRINT N'Update complete.';


GO

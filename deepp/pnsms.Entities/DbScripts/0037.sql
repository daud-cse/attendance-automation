
USE PNSMS;


GO
/*
Table [dbo].[MessageTagGroups] is being dropped.  Deployment will halt if the table contains data.
*/

IF EXISTS (select top 1 1 from [dbo].[MessageTagGroups])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
Table [dbo].[MessageTags] is being dropped.  Deployment will halt if the table contains data.
*/

IF EXISTS (select top 1 1 from [dbo].[MessageTags])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_MessageTags_MessageTagGroups]...';


GO
ALTER TABLE [dbo].[MessageTags] DROP CONSTRAINT [FK_MessageTags_MessageTagGroups];


GO
PRINT N'Dropping [dbo].[MessageTagGroups]...';


GO
DROP TABLE [dbo].[MessageTagGroups];


GO
PRINT N'Dropping [dbo].[MessageTags]...';


GO
DROP TABLE [dbo].[MessageTags];


GO
PRINT N'Creating [dbo].[FeesGenerateAcademics]...';


GO
CREATE TABLE [dbo].[FeesGenerateAcademics] (
    [Id]                 INT IDENTITY (1, 1) NOT NULL,
    [FeesGenerateId]     INT NOT NULL,
    [AcademicSessionId]  INT NULL,
    [AcademicBranchId]   INT NULL,
    [AcademicClassId]    INT NULL,
    [AcademicShiftId]    INT NULL,
    [AcademicSectionId]  INT NULL,
    [AcademicVerssionId] INT NULL,
    [AcademicGroupId]    INT NULL,
    CONSTRAINT [PK_FeesGenerateAcademics] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FeesGenerateHeads]...';


GO
CREATE TABLE [dbo].[FeesGenerateHeads] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [FeesGenerateId] INT             NOT NULL,
    [FeesHeadId]     INT             NOT NULL,
    [Amount]         DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FeesGenerateHeads] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FeesGenerates]...';


GO
CREATE TABLE [dbo].[FeesGenerates] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT             NOT NULL,
    [ForTheMonth]    INT             NOT NULL,
    [ForTheYear]     INT             NOT NULL,
    [GenerationDate] DATE            NULL,
    [DueDate]        DATE            NULL,
    [Remarks]        NVARCHAR (1024) NULL,
    [LastUpdateTime] DATETIME        NULL,
    CONSTRAINT [PK_FeesGenerates] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FeesGenerateStudentDetails]...';


GO
CREATE TABLE [dbo].[FeesGenerateStudentDetails] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [FeesGenerateStudentId] INT             NOT NULL,
    [FeesHeadId]            INT             NOT NULL,
    [Amount]                DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FeesGenerateStudentDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FeesGenerateStudents]...';


GO
CREATE TABLE [dbo].[FeesGenerateStudents] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [FeesGenerateId] INT             NOT NULL,
    [StudentId]      INT             NOT NULL,
    [AmountDue]      DECIMAL (18, 2) NOT NULL,
    [AmountPaid]     DECIMAL (18, 2) NOT NULL,
    [IsCompleted]    BIT             NOT NULL,
    CONSTRAINT [PK_FeesGenerateStudents] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FeesHeads]...';


GO
CREATE TABLE [dbo].[FeesHeads] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [InstituteId]      INT             NOT NULL,
    [Name]             NVARCHAR (256)  NOT NULL,
    [Description]      NVARCHAR (1024) NULL,
    [IsActive]         BIT             NOT NULL,
    [LastUpdateTime]   DATETIME        NULL,
    [ChartOfAccountId] INT             NULL,
    CONSTRAINT [PK_FeesHeads] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[IX_Institutes]...';


GO
ALTER TABLE [dbo].[Institutes]
    ADD CONSTRAINT [IX_Institutes] UNIQUE NONCLUSTERED ([Url] ASC);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateAcademics_FeesGenerates]...';


GO
ALTER TABLE [dbo].[FeesGenerateAcademics] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateAcademics_FeesGenerates] FOREIGN KEY ([FeesGenerateId]) REFERENCES [dbo].[FeesGenerates] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateAcademics_AcademicSessions]...';


GO
ALTER TABLE [dbo].[FeesGenerateAcademics] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateAcademics_AcademicSessions] FOREIGN KEY ([AcademicSessionId]) REFERENCES [dbo].[AcademicSessions] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateAcademics_AcademicBranches]...';


GO
ALTER TABLE [dbo].[FeesGenerateAcademics] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateAcademics_AcademicBranches] FOREIGN KEY ([AcademicBranchId]) REFERENCES [dbo].[AcademicBranches] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateAcademics_AcademicClasses]...';


GO
ALTER TABLE [dbo].[FeesGenerateAcademics] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateAcademics_AcademicClasses] FOREIGN KEY ([AcademicClassId]) REFERENCES [dbo].[AcademicClasses] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateAcademics_AcademicShifts]...';


GO
ALTER TABLE [dbo].[FeesGenerateAcademics] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateAcademics_AcademicShifts] FOREIGN KEY ([AcademicShiftId]) REFERENCES [dbo].[AcademicShifts] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateAcademics_AcademicSections]...';


GO
ALTER TABLE [dbo].[FeesGenerateAcademics] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateAcademics_AcademicSections] FOREIGN KEY ([AcademicSectionId]) REFERENCES [dbo].[AcademicSections] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateAcademics_AcademicVersions]...';


GO
ALTER TABLE [dbo].[FeesGenerateAcademics] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateAcademics_AcademicVersions] FOREIGN KEY ([AcademicVerssionId]) REFERENCES [dbo].[AcademicVersions] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateAcademics_AcademicGroups]...';


GO
ALTER TABLE [dbo].[FeesGenerateAcademics] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateAcademics_AcademicGroups] FOREIGN KEY ([AcademicGroupId]) REFERENCES [dbo].[AcademicGroups] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateHeads_FeesHeads]...';


GO
ALTER TABLE [dbo].[FeesGenerateHeads] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateHeads_FeesHeads] FOREIGN KEY ([FeesHeadId]) REFERENCES [dbo].[FeesHeads] ([Id]);


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
PRINT N'Creating [dbo].[FK_FeesGenerateStudentDetails_FeesGenerateStudents]...';


GO
ALTER TABLE [dbo].[FeesGenerateStudentDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateStudentDetails_FeesGenerateStudents] FOREIGN KEY ([FeesGenerateStudentId]) REFERENCES [dbo].[FeesGenerateStudents] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateStudentDetails_FeesHeads]...';


GO
ALTER TABLE [dbo].[FeesGenerateStudentDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateStudentDetails_FeesHeads] FOREIGN KEY ([FeesHeadId]) REFERENCES [dbo].[FeesHeads] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateStudents_FeesGenerates]...';


GO
ALTER TABLE [dbo].[FeesGenerateStudents] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateStudents_FeesGenerates] FOREIGN KEY ([FeesGenerateId]) REFERENCES [dbo].[FeesGenerates] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateStudents_Students]...';


GO
ALTER TABLE [dbo].[FeesGenerateStudents] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateStudents_Students] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([StudentId]);


GO
PRINT N'Creating [dbo].[FK_FeesHeads_Institutes]...';


GO
ALTER TABLE [dbo].[FeesHeads] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesHeads_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[FeesGenerateAcademics] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateAcademics_FeesGenerates];

ALTER TABLE [dbo].[FeesGenerateAcademics] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateAcademics_AcademicSessions];

ALTER TABLE [dbo].[FeesGenerateAcademics] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateAcademics_AcademicBranches];

ALTER TABLE [dbo].[FeesGenerateAcademics] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateAcademics_AcademicClasses];

ALTER TABLE [dbo].[FeesGenerateAcademics] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateAcademics_AcademicShifts];

ALTER TABLE [dbo].[FeesGenerateAcademics] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateAcademics_AcademicSections];

ALTER TABLE [dbo].[FeesGenerateAcademics] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateAcademics_AcademicVersions];

ALTER TABLE [dbo].[FeesGenerateAcademics] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateAcademics_AcademicGroups];

ALTER TABLE [dbo].[FeesGenerateHeads] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateHeads_FeesHeads];

ALTER TABLE [dbo].[FeesGenerateHeads] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateHeads_FeesGenerates];

ALTER TABLE [dbo].[FeesGenerates] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerates_Institutes];

ALTER TABLE [dbo].[FeesGenerateStudentDetails] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateStudentDetails_FeesGenerateStudents];

ALTER TABLE [dbo].[FeesGenerateStudentDetails] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateStudentDetails_FeesHeads];

ALTER TABLE [dbo].[FeesGenerateStudents] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateStudents_FeesGenerates];

ALTER TABLE [dbo].[FeesGenerateStudents] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateStudents_Students];

ALTER TABLE [dbo].[FeesHeads] WITH CHECK CHECK CONSTRAINT [FK_FeesHeads_Institutes];


GO
update dbo._Migration set LastUpdate='0037'
PRINT N'Update complete.';


GO

USE PNSMS;


GO
PRINT N'Dropping [dbo].[FK_Events_Institutes]...';


GO
ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Events_Institutes];


GO
PRINT N'Dropping [dbo].[FK_Galleries_Events]...';


GO
ALTER TABLE [dbo].[Galleries] DROP CONSTRAINT [FK_Galleries_Events];


GO
PRINT N'Starting rebuilding table [dbo].[Events]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Events] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT            NOT NULL,
    [StartDate]      DATE           NULL,
    [EndDate]        DATE           NULL,
    [EventStartAt]   DATETIME       NULL,
    [EventEndAt]     DATETIME       NULL,
    [EventTitle]     NVARCHAR (512) NULL,
    [EventBody]      NVARCHAR (MAX) NULL,
    [EventBriefInfo] NVARCHAR (MAX) NULL,
    [EventLocation]  NVARCHAR (128) NULL,
    [IsActive]       BIT            NOT NULL,
    [LastUpdateTime] DATETIME       NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Events] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Events])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Events] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Events] ([Id], [InstituteId], [StartDate], [EndDate], [EventStartAt], [EventEndAt], [EventTitle], [EventBody], [EventBriefInfo], [IsActive], [LastUpdateTime])
        SELECT   [Id],
                 [InstituteId],
                 [StartDate],
                 [EndDate],
                 [EventStartAt],
                 [EventEndAt],
                 [EventTitle],
                 [EventBody],
                 [EventBriefInfo],
                 [IsActive],
                 [LastUpdateTime]
        FROM     [dbo].[Events]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Events] OFF;
    END

DROP TABLE [dbo].[Events];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Events]', N'Events';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Events]', N'PK_Events', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[AcademicBranchesOfUserInfoes]...';


GO
CREATE TABLE [dbo].[AcademicBranchesOfUserInfoes] (
    [Id]               INT IDENTITY (1, 1) NOT NULL,
    [UserInfoId]       INT NOT NULL,
    [AcademicBranchId] INT NOT NULL,
    CONSTRAINT [PK_AcademicBranchesOfUserInfoes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[ContactUs]...';


GO
CREATE TABLE [dbo].[ContactUs] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT            NOT NULL,
    [FirstName]      NVARCHAR (128) NULL,
    [LastName]       NVARCHAR (128) NULL,
    [Address]        NVARCHAR (512) NULL,
    [Country]        NVARCHAR (128) NULL,
    [ContactNumber]  NVARCHAR (128) NULL,
    [Email]          NVARCHAR (128) NULL,
    [Subject]        NVARCHAR (512) NULL,
    [Message]        NVARCHAR (MAX) NULL,
    [IsActive]       BIT            NOT NULL,
    [LastUpdateTime] DATETIME       NULL,
    CONSTRAINT [PK_ContactUs] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_Events_Institutes]...';


GO
ALTER TABLE [dbo].[Events] WITH NOCHECK
    ADD CONSTRAINT [FK_Events_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Galleries_Events]...';


GO
ALTER TABLE [dbo].[Galleries] WITH NOCHECK
    ADD CONSTRAINT [FK_Galleries_Events] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AcademicBranchesOfUserInfoes_AcademicBranches]...';


GO
ALTER TABLE [dbo].[AcademicBranchesOfUserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_AcademicBranchesOfUserInfoes_AcademicBranches] FOREIGN KEY ([AcademicBranchId]) REFERENCES [dbo].[AcademicBranches] ([Id]);


GO
PRINT N'Creating [dbo].[FK_AcademicBranchesOfUserInfoes_UserInfoes]...';


GO
ALTER TABLE [dbo].[AcademicBranchesOfUserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_AcademicBranchesOfUserInfoes_UserInfoes] FOREIGN KEY ([UserInfoId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ContactUs_Institutes]...';


GO
ALTER TABLE [dbo].[ContactUs] WITH NOCHECK
    ADD CONSTRAINT [FK_ContactUs_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Teachers_AcademicClasses]...';


GO
ALTER TABLE [dbo].[Teachers] WITH NOCHECK
    ADD CONSTRAINT [FK_Teachers_AcademicClasses] FOREIGN KEY ([DefaultAcademicClassId]) REFERENCES [dbo].[AcademicClasses] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Teachers_AcademicSections]...';


GO
ALTER TABLE [dbo].[Teachers] WITH NOCHECK
    ADD CONSTRAINT [FK_Teachers_AcademicSections] FOREIGN KEY ([DefaultAcademicSectionId]) REFERENCES [dbo].[AcademicSections] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[Events] WITH CHECK CHECK CONSTRAINT [FK_Events_Institutes];

ALTER TABLE [dbo].[Galleries] WITH CHECK CHECK CONSTRAINT [FK_Galleries_Events];

ALTER TABLE [dbo].[AcademicBranchesOfUserInfoes] WITH CHECK CHECK CONSTRAINT [FK_AcademicBranchesOfUserInfoes_AcademicBranches];

ALTER TABLE [dbo].[AcademicBranchesOfUserInfoes] WITH CHECK CHECK CONSTRAINT [FK_AcademicBranchesOfUserInfoes_UserInfoes];

ALTER TABLE [dbo].[ContactUs] WITH CHECK CHECK CONSTRAINT [FK_ContactUs_Institutes];

ALTER TABLE [dbo].[Teachers] WITH CHECK CHECK CONSTRAINT [FK_Teachers_AcademicClasses];

ALTER TABLE [dbo].[Teachers] WITH CHECK CHECK CONSTRAINT [FK_Teachers_AcademicSections];


GO
PRINT N'Update complete.';


GO

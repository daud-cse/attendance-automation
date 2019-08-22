
USE PNSMS;


GO
PRINT N'Altering [dbo].[StudentAttendanceDetails]...';


GO
ALTER TABLE [dbo].[StudentAttendanceDetails]
    ADD [IsAbsconding] BIT NULL;


GO
PRINT N'Altering [dbo].[StudentAttendances]...';


GO
ALTER TABLE [dbo].[StudentAttendances]
    ADD [TotalCount]           INT             NULL,
        [AbscondingCount]      INT             NULL,
        [PresentPercentage]    DECIMAL (18, 2) NULL,
        [AbsentPercentage]     DECIMAL (18, 2) NULL,
        [AbscondingPercentage] DECIMAL (18, 2) NULL;


GO
PRINT N'Creating [dbo].[GlobalUsers]...';


GO
CREATE TABLE [dbo].[GlobalUsers] (
    [GlobalUserId] INT            NOT NULL,
    [FatherName]   NVARCHAR (128) NULL,
    [MotherName]   NVARCHAR (128) NULL,
    CONSTRAINT [PK_GlobalUsers] PRIMARY KEY CLUSTERED ([GlobalUserId] ASC)
);


GO
PRINT N'Creating [dbo].[FK_GlobalUsers_UserInfoes]...';


GO
ALTER TABLE [dbo].[GlobalUsers] WITH NOCHECK
    ADD CONSTRAINT [FK_GlobalUsers_UserInfoes] FOREIGN KEY ([GlobalUserId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserInfoSecurities_Institutes]...';


GO
ALTER TABLE [dbo].[UserInfoSecurities] WITH NOCHECK
    ADD CONSTRAINT [FK_UserInfoSecurities_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[GlobalUsers] WITH CHECK CHECK CONSTRAINT [FK_GlobalUsers_UserInfoes];

ALTER TABLE [dbo].[UserInfoSecurities] WITH CHECK CHECK CONSTRAINT [FK_UserInfoSecurities_Institutes];


GO
update dbo._Migration set LastUpdate='0015'
INSERT [dbo].[UserInfoTypes] ([Id], [Name]) VALUES (15, N'Global ')
PRINT N'Update complete.';


GO

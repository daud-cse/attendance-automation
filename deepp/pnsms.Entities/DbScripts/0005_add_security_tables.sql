
USE PNSMS;


GO
PRINT N'Creating [dbo].[Rights]...';


GO
CREATE TABLE [dbo].[Rights] (
    [Id]                      INT             IDENTITY (1, 1) NOT NULL,
    [Name]                    NVARCHAR (128)  NOT NULL,
    [Code]                    NVARCHAR (32)   NOT NULL,
    [Description]             NVARCHAR (1024) NULL,
    [EnableTeacherOrEmployee] BIT             NOT NULL,
    [EnableStudent]           BIT             NOT NULL,
    [EnableGuardian]          BIT             NOT NULL,
    CONSTRAINT [PK_Rights] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_Rights_Code] UNIQUE NONCLUSTERED ([Code] ASC),
    CONSTRAINT [IX_Rights_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
PRINT N'Creating [dbo].[RightsOfPackages]...';


GO
CREATE TABLE [dbo].[RightsOfPackages] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [PackageId] INT NOT NULL,
    [RightId]   INT NOT NULL,
    CONSTRAINT [PK_RightsOfPackages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_RightsOfPackages] UNIQUE NONCLUSTERED ([PackageId] ASC, [RightId] ASC)
);


GO
PRINT N'Creating [dbo].[RightsOfRoles]...';


GO
CREATE TABLE [dbo].[RightsOfRoles] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [RoleId]  INT NOT NULL,
    [RightId] INT NOT NULL,
    CONSTRAINT [PK_RightsOfRoles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_RightsOfRoles] UNIQUE NONCLUSTERED ([RightId] ASC, [RoleId] ASC)
);


GO
PRINT N'Creating [dbo].[Roles]...';


GO
CREATE TABLE [dbo].[Roles] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]            INT            NOT NULL,
    [Name]                   NVARCHAR (128) NOT NULL,
    [Description]            NVARCHAR (512) NULL,
    [IsForTeacherOrEmployee] BIT            NOT NULL,
    [IsForStudent]           BIT            NOT NULL,
    [IsForGuardian]          BIT            NOT NULL,
    [GuardianTypeId]         INT            NULL,
    [IsActive]               BIT            NOT NULL,
    [LastUpdateTime]         DATETIME       NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_Roles] UNIQUE NONCLUSTERED ([Name] ASC, [InstituteId] ASC)
);


GO
PRINT N'Creating [dbo].[RolesOfUserInfoes]...';


GO
CREATE TABLE [dbo].[RolesOfUserInfoes] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [UserInfoId] INT NOT NULL,
    [RoleId]     INT NOT NULL,
    CONSTRAINT [PK_RolesOfUserInfoes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_RolesOfUserInfoes] UNIQUE NONCLUSTERED ([RoleId] ASC, [UserInfoId] ASC)
);


GO
PRINT N'Creating [dbo].[UserInfoSecurities]...';


GO
CREATE TABLE [dbo].[UserInfoSecurities] (
    [UserInfoId]                       INT            NOT NULL,
    [PasswordHash]                     NVARCHAR (MAX) NULL,
    [SecurityStamp]                    NVARCHAR (MAX) NULL,
    [IsActive]                         BIT            NOT NULL,
    [IsLockout]                        BIT            NOT NULL,
    [LastLoggedinAt]                   DATETIME       NULL,
    [LastPasswordChangedAt]            DATETIME       NULL,
    [LastLockoutAt]                    DATETIME       NULL,
    [FailedPasswordAttemptCount]       INT            NULL,
    [FailedPasswordAttemptWindowStart] DATETIME       NULL,
    [Comment]                          NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserInfoSecurities] PRIMARY KEY CLUSTERED ([UserInfoId] ASC)
);


GO
PRINT N'Creating [dbo].[FK_RightsOfPackages_Packages]...';


GO
ALTER TABLE [dbo].[RightsOfPackages] WITH NOCHECK
    ADD CONSTRAINT [FK_RightsOfPackages_Packages] FOREIGN KEY ([PackageId]) REFERENCES [dbo].[Packages] ([Id]);


GO
PRINT N'Creating [dbo].[FK_RightsOfPackages_Rights]...';


GO
ALTER TABLE [dbo].[RightsOfPackages] WITH NOCHECK
    ADD CONSTRAINT [FK_RightsOfPackages_Rights] FOREIGN KEY ([RightId]) REFERENCES [dbo].[Rights] ([Id]);


GO
PRINT N'Creating [dbo].[FK_RightsOfRoles_Roles]...';


GO
ALTER TABLE [dbo].[RightsOfRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_RightsOfRoles_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]);


GO
PRINT N'Creating [dbo].[FK_RightsOfRoles_Rights]...';


GO
ALTER TABLE [dbo].[RightsOfRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_RightsOfRoles_Rights] FOREIGN KEY ([RightId]) REFERENCES [dbo].[Rights] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Roles_Institutes]...';


GO
ALTER TABLE [dbo].[Roles] WITH NOCHECK
    ADD CONSTRAINT [FK_Roles_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Roles_GuardianTypes]...';


GO
ALTER TABLE [dbo].[Roles] WITH NOCHECK
    ADD CONSTRAINT [FK_Roles_GuardianTypes] FOREIGN KEY ([GuardianTypeId]) REFERENCES [dbo].[GuardianTypes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_RolesOfUserInfoes_UserInfoes]...';


GO
ALTER TABLE [dbo].[RolesOfUserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_RolesOfUserInfoes_UserInfoes] FOREIGN KEY ([UserInfoId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_RolesOfUserInfoes_Roles]...';


GO
ALTER TABLE [dbo].[RolesOfUserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_RolesOfUserInfoes_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserInfoSecurities_UserInfoes]...';


GO
ALTER TABLE [dbo].[UserInfoSecurities] WITH NOCHECK
    ADD CONSTRAINT [FK_UserInfoSecurities_UserInfoes] FOREIGN KEY ([UserInfoId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_StudentAttendanceDetails_Students]...';


GO
ALTER TABLE [dbo].[StudentAttendanceDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_StudentAttendanceDetails_Students] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([StudentId]);


GO
PRINT N'Creating [dbo].[SprGetRights]...';


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SprGetRights
	-- Add the parameters for the stored procedure here
	@UserInfoId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @rights table(right_code nvarchar(32))
    
    
    
	select right_code from @rights
END
GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[RightsOfPackages] WITH CHECK CHECK CONSTRAINT [FK_RightsOfPackages_Packages];

ALTER TABLE [dbo].[RightsOfPackages] WITH CHECK CHECK CONSTRAINT [FK_RightsOfPackages_Rights];

ALTER TABLE [dbo].[RightsOfRoles] WITH CHECK CHECK CONSTRAINT [FK_RightsOfRoles_Roles];

ALTER TABLE [dbo].[RightsOfRoles] WITH CHECK CHECK CONSTRAINT [FK_RightsOfRoles_Rights];

ALTER TABLE [dbo].[Roles] WITH CHECK CHECK CONSTRAINT [FK_Roles_Institutes];

ALTER TABLE [dbo].[Roles] WITH CHECK CHECK CONSTRAINT [FK_Roles_GuardianTypes];

ALTER TABLE [dbo].[RolesOfUserInfoes] WITH CHECK CHECK CONSTRAINT [FK_RolesOfUserInfoes_UserInfoes];

ALTER TABLE [dbo].[RolesOfUserInfoes] WITH CHECK CHECK CONSTRAINT [FK_RolesOfUserInfoes_Roles];

ALTER TABLE [dbo].[UserInfoSecurities] WITH CHECK CHECK CONSTRAINT [FK_UserInfoSecurities_UserInfoes];

ALTER TABLE [dbo].[StudentAttendanceDetails] WITH CHECK CHECK CONSTRAINT [FK_StudentAttendanceDetails_Students];


GO
PRINT N'Update complete.';


GO

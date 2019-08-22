
USE PNSMS;


GO
PRINT N'Altering [dbo].[Rights]...';


GO
ALTER TABLE [dbo].[Rights]
    ADD [EnableGoverningbody] BIT NULL;


GO
PRINT N'Altering [dbo].[Roles]...';


GO
ALTER TABLE [dbo].[Roles]
    ADD [EnableGoverningbody] BIT NULL;


GO
PRINT N'Creating [dbo].[Governingbodies]...';


GO
CREATE TABLE [dbo].[Governingbodies] (
    [GoverningbodyId] INT            NOT NULL,
    [FatherName]      NVARCHAR (128) NULL,
    [MotherName]      NVARCHAR (128) NULL,
    CONSTRAINT [PK_Governingbodies] PRIMARY KEY CLUSTERED ([GoverningbodyId] ASC)
);


GO
PRINT N'Creating [dbo].[FK_Governingbodies_UserInfoes]...';


GO
ALTER TABLE [dbo].[Governingbodies] WITH NOCHECK
    ADD CONSTRAINT [FK_Governingbodies_UserInfoes] FOREIGN KEY ([GoverningbodyId]) REFERENCES [dbo].[UserInfoes] ([Id]);


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
ALTER TABLE [dbo].[Governingbodies] WITH CHECK CHECK CONSTRAINT [FK_Governingbodies_UserInfoes];

update dbo._Migration set LastUpdate='0023'
insert into dbo.UserInfoTypes values(16,'Governingbody')
insert into dbo.UserInfoTypes values(15,'Global User')

GO
PRINT N'Update complete.';


GO

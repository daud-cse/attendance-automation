
USE PNSMS;

update dbo._Migration set LastUpdate='09'
GO
PRINT N'Creating [dbo].[QryRightsOfRoles]...';


GO
CREATE VIEW dbo.QryRightsOfRoles
AS
SELECT     dbo.Roles.InstituteId, dbo.Roles.IsForEmployee, dbo.Roles.IsForTeacher, dbo.Roles.IsForStudent, dbo.Roles.IsForGuardian, dbo.Roles.GuardianTypeId, 
                      dbo.Roles.IsActive, dbo.Rights.Code
FROM         dbo.RightsOfRoles LEFT OUTER JOIN
                      dbo.Roles ON dbo.RightsOfRoles.RoleId = dbo.Roles.Id LEFT OUTER JOIN
                      dbo.Rights ON dbo.RightsOfRoles.RightId = dbo.Rights.Id
GO
PRINT N'Creating [dbo].[QryRightsOfUserInfoes]...';


GO
CREATE VIEW dbo.QryRightsOfUserInfoes
AS
SELECT     dbo.RolesOfUserInfoes.UserInfoId, dbo.RolesOfUserInfoes.RoleId, dbo.RightsOfRoles.RightId, dbo.Roles.IsActive, dbo.Rights.Code
FROM         dbo.RightsOfRoles LEFT OUTER JOIN
                      dbo.Rights ON dbo.RightsOfRoles.RightId = dbo.Rights.Id LEFT OUTER JOIN
                      dbo.Roles ON dbo.RightsOfRoles.RoleId = dbo.Roles.Id LEFT OUTER JOIN
                      dbo.RolesOfUserInfoes ON dbo.RightsOfRoles.RoleId = dbo.RolesOfUserInfoes.RoleId
WHERE     (dbo.RolesOfUserInfoes.UserInfoId IS NOT NULL)
GO
PRINT N'Altering [dbo].[SprGetRights]...';


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SprGetRights]
	-- Add the parameters for the stored procedure here
	@UserInfoId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @rights table(rightsCode nvarchar(32),guardianTypeId int)    
    declare @tp int,@institute int,@gt int
    
    select @tp=UserInfoTypeId,@institute=InstituteId from dbo.UserInfoes where Id=@UserInfoId
    
    if @tp=11
    begin
		insert into @rights(rightsCode)
		select [Code]
			from dbo.QryRightsOfRoles
			where InstituteId=@institute and IsForStudent=1 and IsActive=1
    end
    else if @tp=12
    begin
		select @gt=GuardianTypeId from dbo.Guardians where GuardianId=@UserInfoId
		insert into @rights(rightsCode,guardianTypeId)
		select [Code],GuardianTypeId
			from dbo.QryRightsOfRoles
			where InstituteId=@institute and IsForGuardian=1 and IsActive=1
		delete from @rights where guardianTypeId != @gt
    end
    else
    begin
		insert into @rights(rightsCode)
		select [Code]
			from dbo.QryRightsOfUserInfoes 
			where UserInfoId=@UserInfoId and IsActive=1
    end
    
	select distinct rightsCode from @rights
END
GO


update dbo.UserInfoes set UserInfoTypeId=11

GO
/*
The column UserInfoTypeId on table [dbo].[UserInfoes] must be changed from NULL to NOT NULL. If the table contains data, the ALTER script may not work. To avoid this issue, you must add values to this column for all rows or mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[UserInfoes])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_UserInfoes_UserInfoTypes]...';


GO
ALTER TABLE [dbo].[UserInfoes] DROP CONSTRAINT [FK_UserInfoes_UserInfoTypes];


GO
PRINT N'Altering [dbo].[UserInfoes]...';


GO
ALTER TABLE [dbo].[UserInfoes] ALTER COLUMN [UserInfoTypeId] INT NOT NULL;


GO
PRINT N'Creating [dbo].[FK_UserInfoes_UserInfoTypes]...';


GO
ALTER TABLE [dbo].[UserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_UserInfoes_UserInfoTypes] FOREIGN KEY ([UserInfoTypeId]) REFERENCES [dbo].[UserInfoTypes] ([Id]);


GO
PRINT N'Refreshing [dbo].[SprGetRights]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[SprGetRights]';


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[UserInfoes] WITH CHECK CHECK CONSTRAINT [FK_UserInfoes_UserInfoTypes];


GO
PRINT N'Update complete.';


GO


USE PNSMS;


GO
/*
The type for column PIN in table [dbo].[UserInfoes] is currently  NCHAR (128) NULL but is being changed to  NVARCHAR (32) NULL. Data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[UserInfoes])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Altering [dbo].[Rights]...';


GO
ALTER TABLE [dbo].[Rights] ALTER COLUMN [EnableEmployee] BIT NULL;

ALTER TABLE [dbo].[Rights] ALTER COLUMN [EnableGuardian] BIT NULL;

ALTER TABLE [dbo].[Rights] ALTER COLUMN [EnableStudent] BIT NULL;

ALTER TABLE [dbo].[Rights] ALTER COLUMN [EnableTeacher] BIT NULL;


GO
PRINT N'Altering [dbo].[Roles]...';


GO
ALTER TABLE [dbo].[Roles] ALTER COLUMN [IsForEmployee] BIT NULL;

ALTER TABLE [dbo].[Roles] ALTER COLUMN [IsForGuardian] BIT NULL;

ALTER TABLE [dbo].[Roles] ALTER COLUMN [IsForStudent] BIT NULL;

ALTER TABLE [dbo].[Roles] ALTER COLUMN [IsForTeacher] BIT NULL;


GO
PRINT N'Altering [dbo].[UserInfoes]...';


GO
ALTER TABLE [dbo].[UserInfoes] ALTER COLUMN [PIN] NVARCHAR (32) NULL;


GO
PRINT N'Refreshing [dbo].[QryRightsOfRoles]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[QryRightsOfRoles]';


GO
PRINT N'Refreshing [dbo].[QryRightsOfUserInfoes]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[QryRightsOfUserInfoes]';


GO
PRINT N'Altering [dbo].[SprGetUserCode]...';


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SprGetUserCode]
	-- Add the parameters for the stored procedure here
	@UserInfoId int,
	@PIN nvarchar(128) output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	set @PIN='123'
END
GO
PRINT N'Refreshing [dbo].[SprGetRights]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[SprGetRights]';


GO

PRINT N'Dropping [dbo].[SprGetUserCode]...';


GO
DROP PROCEDURE [dbo].[SprGetUserCode];


GO
PRINT N'Creating [dbo].[SprGetPIN]...';


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[SprGetPIN]
	-- Add the parameters for the stored procedure here
	@UserInfoId int,
	@PIN nvarchar(128) output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	set @PIN='123'
END
GO
update dbo._Migration set LastUpdate='0022'
PRINT N'Update complete.';


GO

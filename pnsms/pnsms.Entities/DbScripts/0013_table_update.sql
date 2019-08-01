
USE PNSMS;


GO
PRINT N'Dropping [dbo].[FK_UserInfoes_Institutes]...';


GO
ALTER TABLE [dbo].[UserInfoes] DROP CONSTRAINT [FK_UserInfoes_Institutes];


GO
PRINT N'Dropping [dbo].[IX_UserInfoSecurities]...';


GO
ALTER TABLE [dbo].[UserInfoSecurities] DROP CONSTRAINT [IX_UserInfoSecurities];


GO
PRINT N'Altering [dbo].[UserInfoes]...';


GO
ALTER TABLE [dbo].[UserInfoes] ALTER COLUMN [InstituteId] INT NULL;


GO
PRINT N'Altering [dbo].[UserInfoSecurities]...';


GO
ALTER TABLE [dbo].[UserInfoSecurities] ALTER COLUMN [InstituteId] INT NULL;


GO
PRINT N'Creating [dbo].[IX_UserInfoSecurities]...';


GO
ALTER TABLE [dbo].[UserInfoSecurities]
    ADD CONSTRAINT [IX_UserInfoSecurities] UNIQUE NONCLUSTERED ([UserName] ASC, [InstituteId] ASC);


GO
PRINT N'Creating [dbo].[FK_UserInfoes_Institutes]...';


GO
ALTER TABLE [dbo].[UserInfoes] WITH NOCHECK
    ADD CONSTRAINT [FK_UserInfoes_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Refreshing [dbo].[SprGetRights]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[SprGetRights]';


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[UserInfoes] WITH CHECK CHECK CONSTRAINT [FK_UserInfoes_Institutes];


GO
update dbo._Migration set LastUpdate='0013'
PRINT N'Update complete.';


GO

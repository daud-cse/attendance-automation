
USE PNSMS;


GO
PRINT N'Creating [dbo].[PK__Migration]...';


GO
ALTER TABLE [dbo].[_Migration]
    ADD CONSTRAINT [PK__Migration] PRIMARY KEY CLUSTERED ([LastUpdate] ASC);


GO
update dbo._Migration set LastUpdate='0014'
PRINT N'Update complete.';


GO

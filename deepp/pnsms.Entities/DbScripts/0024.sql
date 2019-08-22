
USE PNSMS;


GO
/*
The column [dbo].[ShortMessageDetails].[Status] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[ShortMessageDetails])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_ShortMessageDetails_ShortMessages]...';


GO
ALTER TABLE [dbo].[ShortMessageDetails] DROP CONSTRAINT [FK_ShortMessageDetails_ShortMessages];


GO
PRINT N'Dropping [dbo].[FK_ShortMessageDetails_UserInfoes]...';


GO
ALTER TABLE [dbo].[ShortMessageDetails] DROP CONSTRAINT [FK_ShortMessageDetails_UserInfoes];


GO
PRINT N'Starting rebuilding table [dbo].[ShortMessageDetails]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_ShortMessageDetails] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [ShortMessageId]          INT            NOT NULL,
    [SmsText]                 NVARCHAR (512) NULL,
    [MobileNumber]            VARCHAR (50)   NOT NULL,
    [UserInfoId]              INT            NULL,
    [SmsCount]                INT            NOT NULL,
    [IsSent]                  BIT            NOT NULL,
    [ShortMessageStatusId]    INT            NULL,
    [GatewayIdentificationNo] NVARCHAR (128) NULL,
    [IsStatusUpdated]         BIT            NULL,
    [StatusUpdatedAt]         DATETIME       NULL,
    [Comments]                NVARCHAR (512) NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_ShortMessageDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[ShortMessageDetails])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ShortMessageDetails] ON;
        INSERT INTO [dbo].[tmp_ms_xx_ShortMessageDetails] ([Id], [ShortMessageId], [SmsText], [MobileNumber], [UserInfoId], [SmsCount], [IsSent], [GatewayIdentificationNo], [IsStatusUpdated], [StatusUpdatedAt])
        SELECT   [Id],
                 [ShortMessageId],
                 [SmsText],
                 [MobileNumber],
                 [UserInfoId],
                 [SmsCount],
                 [IsSent],
                 [GatewayIdentificationNo],
                 [IsStatusUpdated],
                 [StatusUpdatedAt]
        FROM     [dbo].[ShortMessageDetails]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ShortMessageDetails] OFF;
    END

DROP TABLE [dbo].[ShortMessageDetails];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_ShortMessageDetails]', N'ShortMessageDetails';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_ShortMessageDetails]', N'PK_ShortMessageDetails', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[ShortMessageStatuses]...';


GO
CREATE TABLE [dbo].[ShortMessageStatuses] (
    [Id]     INT            NOT NULL,
    [Status] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_ShortMessageStatuses] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_ShortMessageDetails_ShortMessages]...';


GO
ALTER TABLE [dbo].[ShortMessageDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_ShortMessageDetails_ShortMessages] FOREIGN KEY ([ShortMessageId]) REFERENCES [dbo].[ShortMessages] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ShortMessageDetails_UserInfoes]...';


GO
ALTER TABLE [dbo].[ShortMessageDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_ShortMessageDetails_UserInfoes] FOREIGN KEY ([UserInfoId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ShortMessageDetails_ShortMessageStatuses]...';


GO
ALTER TABLE [dbo].[ShortMessageDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_ShortMessageDetails_ShortMessageStatuses] FOREIGN KEY ([ShortMessageStatusId]) REFERENCES [dbo].[ShortMessageStatuses] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[ShortMessageDetails] WITH CHECK CHECK CONSTRAINT [FK_ShortMessageDetails_ShortMessages];

ALTER TABLE [dbo].[ShortMessageDetails] WITH CHECK CHECK CONSTRAINT [FK_ShortMessageDetails_UserInfoes];

ALTER TABLE [dbo].[ShortMessageDetails] WITH CHECK CHECK CONSTRAINT [FK_ShortMessageDetails_ShortMessageStatuses];


GO
insert into [dbo].[ShortMessageStatuses] values
(101,'Pending'),(102,'Delivered'),(103,'Undelivered')

PRINT N'Update complete.';


GO

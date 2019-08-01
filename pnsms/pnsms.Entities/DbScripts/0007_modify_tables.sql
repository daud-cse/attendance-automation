
USE PNSMS;


GO
/*
The column [dbo].[UserInfoSecurities].[UserName] on table [dbo].[UserInfoSecurities] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.

The column PasswordHash on table [dbo].[UserInfoSecurities] must be changed from NULL to NOT NULL. If the table contains data, the ALTER script may not work. To avoid this issue, you must add values to this column for all rows or mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[UserInfoSecurities])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_UserInfoSecurities_UserInfoes]...';


GO
ALTER TABLE [dbo].[UserInfoSecurities] DROP CONSTRAINT [FK_UserInfoSecurities_UserInfoes];


GO
PRINT N'Starting rebuilding table [dbo].[Images]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Images] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [RefCode]         NCHAR (16)      NOT NULL,
    [RefPrimaryKey]   INT             NOT NULL,
    [ImageBinaryData] VARBINARY (MAX) NULL,
    [ImageCaption]    NVARCHAR (MAX)  NULL,
    [ImageUrl]        NVARCHAR (MAX)  NULL,
    [IsActive]        BIT             NOT NULL,
    [LastUpdatedTime] DATETIME        NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Images] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Images])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Images] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Images] ([Id], [RefCode], [RefPrimaryKey], [ImageBinaryData], [ImageCaption], [IsActive], [LastUpdatedTime])
        SELECT   [Id],
                 [RefCode],
                 [RefPrimaryKey],
                 [ImageBinaryData],
                 [ImageCaption],
                 [IsActive],
                 [LastUpdatedTime]
        FROM     [dbo].[Images]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Images] OFF;
    END

DROP TABLE [dbo].[Images];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Images]', N'Images';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Images]', N'PK_Images', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Images].[IX_Images]...';


GO
CREATE NONCLUSTERED INDEX [IX_Images]
    ON [dbo].[Images]([RefCode] ASC, [RefPrimaryKey] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[UserInfoSecurities]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_UserInfoSecurities] (
    [UserInfoId]                       INT            NOT NULL,
    [UserName]                         NVARCHAR (128) NOT NULL,
    [PasswordHash]                     NVARCHAR (MAX) NOT NULL,
    [SecurityStamp]                    NVARCHAR (MAX) NULL,
    [IsActive]                         BIT            NOT NULL,
    [IsLockout]                        BIT            NOT NULL,
    [LastLoggedinAt]                   DATETIME       NULL,
    [LastPasswordChangedAt]            DATETIME       NULL,
    [LastLockoutAt]                    DATETIME       NULL,
    [FailedPasswordAttemptCount]       INT            NULL,
    [FailedPasswordAttemptWindowStart] DATETIME       NULL,
    [Comment]                          NVARCHAR (MAX) NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_UserInfoSecurities] PRIMARY KEY CLUSTERED ([UserInfoId] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_IX_UserInfoSecurities] UNIQUE NONCLUSTERED ([UserName] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[UserInfoSecurities])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_UserInfoSecurities] ([UserInfoId], [PasswordHash], [SecurityStamp], [IsActive], [IsLockout], [LastLoggedinAt], [LastPasswordChangedAt], [LastLockoutAt], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [Comment])
        SELECT   [UserInfoId],
                 [PasswordHash],
                 [SecurityStamp],
                 [IsActive],
                 [IsLockout],
                 [LastLoggedinAt],
                 [LastPasswordChangedAt],
                 [LastLockoutAt],
                 [FailedPasswordAttemptCount],
                 [FailedPasswordAttemptWindowStart],
                 [Comment]
        FROM     [dbo].[UserInfoSecurities]
        ORDER BY [UserInfoId] ASC;
    END

DROP TABLE [dbo].[UserInfoSecurities];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_UserInfoSecurities]', N'UserInfoSecurities';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_UserInfoSecurities]', N'PK_UserInfoSecurities', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_IX_UserInfoSecurities]', N'IX_UserInfoSecurities', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[FK_UserInfoSecurities_UserInfoes]...';


GO
ALTER TABLE [dbo].[UserInfoSecurities] WITH NOCHECK
    ADD CONSTRAINT [FK_UserInfoSecurities_UserInfoes] FOREIGN KEY ([UserInfoId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[UserInfoSecurities] WITH CHECK CHECK CONSTRAINT [FK_UserInfoSecurities_UserInfoes];


GO
PRINT N'Update complete.';


GO

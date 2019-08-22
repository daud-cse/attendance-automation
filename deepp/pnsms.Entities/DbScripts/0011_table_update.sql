USE PNSMS;


GO

update dbo.Institutes set Url='localhost'

/*
The column Url on table [dbo].[Institutes] must be changed from NULL to NOT NULL. If the table contains data, the ALTER script may not work. To avoid this issue, you must add values to this column for all rows or mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.

The type for column Url in table [dbo].[Institutes] is currently  NVARCHAR (512) NULL but is being changed to  NVARCHAR (128) NOT NULL. Data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Institutes])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[UserInfoSecurities].[InstituteId] on table [dbo].[UserInfoSecurities] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[UserInfoSecurities])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_UserInfoSecurities_UserInfoes]...';


GO
ALTER TABLE [dbo].[UserInfoSecurities] DROP CONSTRAINT [FK_UserInfoSecurities_UserInfoes];


GO
PRINT N'Altering [dbo].[Institutes]...';


GO
ALTER TABLE [dbo].[Institutes] ALTER COLUMN [Url] NVARCHAR (128) NOT NULL;


GO
PRINT N'Creating [dbo].[IX_Institutes]...';


GO
ALTER TABLE [dbo].[Institutes]
    ADD CONSTRAINT [IX_Institutes] UNIQUE NONCLUSTERED ([Url] ASC);


GO
PRINT N'Starting rebuilding table [dbo].[UserInfoSecurities]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_UserInfoSecurities] (
    [UserInfoId]                       INT            NOT NULL,
    [InstituteId]                      INT            NOT NULL,
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
    CONSTRAINT [tmp_ms_xx_constraint_IX_UserInfoSecurities] UNIQUE NONCLUSTERED ([UserName] ASC, [InstituteId] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[UserInfoSecurities])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_UserInfoSecurities] ([UserInfoId], [UserName], [PasswordHash], [SecurityStamp], [IsActive], [IsLockout], [LastLoggedinAt], [LastPasswordChangedAt], [LastLockoutAt], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [Comment])
        SELECT   [UserInfoId],
                 [UserName],
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

INSERT [dbo].[ImageTypes] ([Id], [Name]) VALUES (19, N'Galley_Default')
INSERT [dbo].[ImageTypes] ([Id], [Name]) VALUES (20, N'UserInfo_Photo_Small')
update dbo._Migration set LastUpdate='0011'
GO

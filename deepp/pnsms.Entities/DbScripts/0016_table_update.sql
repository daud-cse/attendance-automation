
USE PNSMS;


GO
PRINT N'Creating [dbo].[NoticeTypes]...';


GO
CREATE TABLE [dbo].[NoticeTypes] (
    [Id]   INT           NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_NoticeTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

update dbo._Migration set LastUpdate='0016'
INSERT [dbo].[NoticeTypes] ([Id], [Name]) VALUES (11, N'Student ')
INSERT [dbo].[NoticeTypes] ([Id], [Name]) VALUES (12, N'Teacher ')
INSERT [dbo].[NoticeTypes] ([Id], [Name]) VALUES (13, N'Employee ')

PRINT N'Update complete.';


GO

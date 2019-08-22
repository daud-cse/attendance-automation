
USE PNSMS;


GO
PRINT N'Altering [dbo].[Students]...';


GO
ALTER TABLE [dbo].[Students]
    ADD [IsCurrent] BIT NULL;


GO
PRINT N'Creating [dbo].[NotificationTagGroups]...';


GO
CREATE TABLE [dbo].[NotificationTagGroups] (
    [Id]   INT            NOT NULL,
    [Name] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_NotificationTagGroups] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[NotificationTags]...';


GO
CREATE TABLE [dbo].[NotificationTags] (
    [Id]                  INT            NOT NULL,
    [NotificationGroupId] INT            NOT NULL,
    [Tag]                 NVARCHAR (64)  NOT NULL,
    [MaxCharLength]       INT            NOT NULL,
    [IsForStudent]        BIT            NULL,
    [IsForGuardian]       BIT            NULL,
    [IsForTeacher]        BIT            NULL,
    [IsForEmployee]       BIT            NULL,
    [IsForGoverningBody]  BIT            NULL,
    [PreviewText]         NVARCHAR (128) NOT NULL,
    [TextToCalculate]     NVARCHAR (128) NOT NULL,
    [IsShowFromDate]      BIT            NULL,
    [IsShowToDate]        BIT            NULL,
    CONSTRAINT [PK_NotificationTags] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_NotificationTags_NotificationTagGroups]...';


GO
ALTER TABLE [dbo].[NotificationTags] WITH NOCHECK
    ADD CONSTRAINT [FK_NotificationTags_NotificationTagGroups] FOREIGN KEY ([NotificationGroupId]) REFERENCES [dbo].[NotificationTagGroups] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[NotificationTags] WITH CHECK CHECK CONSTRAINT [FK_NotificationTags_NotificationTagGroups];


PRINT N'Altering [dbo].[ShortMessages]...';


GO
ALTER TABLE [dbo].[ShortMessages]
    ADD [IsChecked] BIT NULL;


GO
PRINT N'Refreshing [dbo].[SprSmsGeneration]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[SprSmsGeneration]';

update dbo._Migration set LastUpdate='0028'

/****** Object:  Table [dbo].[NotificationTagGroups]    Script Date: 06/20/2015 01:25:17 ******/
INSERT [dbo].[NotificationTagGroups] ([Id], [Name]) VALUES (1, N'General')
INSERT [dbo].[NotificationTagGroups] ([Id], [Name]) VALUES (2, N'Attendance')
/****** Object:  Table [dbo].[NotificationTags]    Script Date: 06/20/2015 01:25:17 ******/
INSERT [dbo].[NotificationTags] ([Id], [NotificationGroupId], [Tag], [MaxCharLength], [IsForStudent], [IsForGuardian], [IsForTeacher], [IsForEmployee], [IsForGoverningBody], [PreviewText], [TextToCalculate], [IsShowFromDate], [IsShowToDate]) VALUES (101, 1, N'{NAME}', 25, 1, 1, 1, 1, 1, N'Md. Bari', N'aaaaaaaaaaaaaaaaaaaaaaaaa', NULL, NULL)
INSERT [dbo].[NotificationTags] ([Id], [NotificationGroupId], [Tag], [MaxCharLength], [IsForStudent], [IsForGuardian], [IsForTeacher], [IsForEmployee], [IsForGoverningBody], [PreviewText], [TextToCalculate], [IsShowFromDate], [IsShowToDate]) VALUES (102, 1, N'{STUDENT_NAME}', 25, NULL, 1, NULL, NULL, 0, N'Md. Ruef', N'aaaaaaaaaaaaaaaaaaaaaaaaa', NULL, NULL)
INSERT [dbo].[NotificationTags] ([Id], [NotificationGroupId], [Tag], [MaxCharLength], [IsForStudent], [IsForGuardian], [IsForTeacher], [IsForEmployee], [IsForGoverningBody], [PreviewText], [TextToCalculate], [IsShowFromDate], [IsShowToDate]) VALUES (201, 2, N'{FROM_DATE}', 9, 1, 1, 1, 1, 1, N'01-Jan-15', N'aaaaaaaaa', 1, NULL)
INSERT [dbo].[NotificationTags] ([Id], [NotificationGroupId], [Tag], [MaxCharLength], [IsForStudent], [IsForGuardian], [IsForTeacher], [IsForEmployee], [IsForGoverningBody], [PreviewText], [TextToCalculate], [IsShowFromDate], [IsShowToDate]) VALUES (202, 2, N'{TO_DATE}', 9, 1, 1, 1, 1, 1, N'31-Jan-15', N'aaaaaaaaa', NULL, 1)
INSERT [dbo].[NotificationTags] ([Id], [NotificationGroupId], [Tag], [MaxCharLength], [IsForStudent], [IsForGuardian], [IsForTeacher], [IsForEmployee], [IsForGoverningBody], [PreviewText], [TextToCalculate], [IsShowFromDate], [IsShowToDate]) VALUES (203, 2, N'{PRESENT_ST}', 3, 1, 1, 1, 1, NULL, N'18', N'aaa', 1, 1)
INSERT [dbo].[NotificationTags] ([Id], [NotificationGroupId], [Tag], [MaxCharLength], [IsForStudent], [IsForGuardian], [IsForTeacher], [IsForEmployee], [IsForGoverningBody], [PreviewText], [TextToCalculate], [IsShowFromDate], [IsShowToDate]) VALUES (204, 2, N'{ABSENT_ST}', 3, 1, 1, 1, 1, NULL, N'2', N'aaa', 1, 1)
INSERT [dbo].[NotificationTags] ([Id], [NotificationGroupId], [Tag], [MaxCharLength], [IsForStudent], [IsForGuardian], [IsForTeacher], [IsForEmployee], [IsForGoverningBody], [PreviewText], [TextToCalculate], [IsShowFromDate], [IsShowToDate]) VALUES (205, 2, N'{ABSCONDING_ST}', 3, 1, 1, NULL, NULL, NULL, N'0', N'aaa', 1, 1)

GO
PRINT N'Update complete.';




GO

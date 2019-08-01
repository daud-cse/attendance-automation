
USE [PNSMS]
GO
/****** Object:  Table [dbo].[Colours]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colours](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[ColorCode] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_Colours] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Colours] ([Id], [Name], [ColorCode]) VALUES (1, N'red', N'btn-danger')
INSERT [dbo].[Colours] ([Id], [Name], [ColorCode]) VALUES (2, N'green', N'btn-success')
/****** Object:  Table [dbo].[Packages]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Packages](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Packages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Packages] ([Id], [Name], [Description], [IsActive]) VALUES (101, N'Basic', N'Basic Features', 1)
/****** Object:  Table [dbo].[Institutes]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Institutes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[PackageId] [int] NOT NULL,
	[Code] [nvarchar](128) NULL,
	[About] [nvarchar](max) NULL,
	[BannerUrl] [nvarchar](256) NULL,
	[LogoUrl] [nvarchar](256) NULL,
	[SeoText] [varchar](max) NULL,
	[VideoURL] [varchar](512) NULL,
	[latitude] [decimal](9, 6) NULL,
	[longitude] [decimal](9, 6) NULL,
	[WelComeText] [varchar](max) NULL,
	[FacebookUrl] [nvarchar](256) NULL,
	[TwitterUrl] [nvarchar](256) NULL,
	[GoogleUrl] [nvarchar](256) NULL,
	[LinkedinUrl] [nvarchar](256) NULL,
	[BehanceUrl] [nvarchar](256) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Institutes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Institutes] ON
INSERT [dbo].[Institutes] ([Id], [Name], [PackageId], [Code], [About], [BannerUrl], [LogoUrl], [SeoText], [VideoURL], [latitude], [longitude], [WelComeText], [FacebookUrl], [TwitterUrl], [GoogleUrl], [LinkedinUrl], [BehanceUrl], [IsActive], [LastUpdateTime]) VALUES (1, N'CARDIFF', 101, N'099', N'CARDIFF School', NULL, NULL, N'school, english medium', NULL, NULL, NULL, N'welcome to school', NULL, NULL, NULL, NULL, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[Institutes] OFF
/****** Object:  Table [dbo].[Religions]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Religions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Religions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Religions] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Religions] ON
INSERT [dbo].[Religions] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'Muslim', NULL, 1, NULL)
INSERT [dbo].[Religions] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (2, 1, N'Shonaton', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[Religions] OFF
/****** Object:  Table [dbo].[Professions]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Professions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Professions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Professions] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Professions] ON
INSERT [dbo].[Professions] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'Service', NULL, 1, NULL)
INSERT [dbo].[Professions] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (2, 1, N'Business', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[Professions] OFF
/****** Object:  Table [dbo].[AcademicVersions]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcademicVersions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_AcademicVersions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_AcademicVersions] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AcademicVersions] ON
INSERT [dbo].[AcademicVersions] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'Bangla', NULL, 1, NULL)
INSERT [dbo].[AcademicVersions] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (3, 1, N'English', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[AcademicVersions] OFF
/****** Object:  Table [dbo].[AcademicShifts]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcademicShifts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[StartAt] [time](7) NULL,
	[EndAt] [time](7) NULL,
	[Remarks] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_AcademicShifts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_AcademicShifts] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AcademicShifts] ON
INSERT [dbo].[AcademicShifts] ([Id], [InstituteId], [Name], [StartAt], [EndAt], [Remarks], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'Morning', CAST(0x0700D85EAC3A0000 AS Time), CAST(0x07007870335C0000 AS Time), NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[AcademicShifts] OFF
/****** Object:  Table [dbo].[AcademicSessions]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcademicSessions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[StartAt] [date] NOT NULL,
	[EndAt] [date] NULL,
	[Remarks] [nvarchar](max) NULL,
	[IsCompleted] [bit] NOT NULL,
	[IsRunning] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_AcademicSessions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_AcademicSessions] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AcademicSessions] ON
INSERT [dbo].[AcademicSessions] ([Id], [InstituteId], [Name], [StartAt], [EndAt], [Remarks], [IsCompleted], [IsRunning], [LastUpdateTime]) VALUES (1, 1, N'2015-2016', CAST(0x233A0B00 AS Date), NULL, NULL, 0, 1, NULL)
SET IDENTITY_INSERT [dbo].[AcademicSessions] OFF
/****** Object:  Table [dbo].[AcademicSections]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcademicSections](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_AcademicSections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_AcademicSections] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AcademicSections] ON
INSERT [dbo].[AcademicSections] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'A', NULL, 1, NULL)
INSERT [dbo].[AcademicSections] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (2, 1, N'B', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[AcademicSections] OFF
/****** Object:  Table [dbo].[AcademicGroups]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcademicGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_AcademicGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_AcademicGroups] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AcademicGroups] ON
INSERT [dbo].[AcademicGroups] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'Science', NULL, 1, NULL)
INSERT [dbo].[AcademicGroups] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (3, 1, N'Commerse', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[AcademicGroups] OFF
/****** Object:  Table [dbo].[AcademicClasses]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcademicClasses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_AcademicClasses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_AcademicClasses] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AcademicClasses] ON
INSERT [dbo].[AcademicClasses] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'One', NULL, 1, NULL)
INSERT [dbo].[AcademicClasses] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (2, 1, N'Nine', NULL, 1, CAST(0x0000A47700DFAF56 AS DateTime))
SET IDENTITY_INSERT [dbo].[AcademicClasses] OFF
/****** Object:  Table [dbo].[AcademicBranches]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AcademicBranches](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[About] [nvarchar](max) NULL,
	[BannerUrl] [nvarchar](256) NULL,
	[LogoUrl] [nvarchar](256) NULL,
	[Description] [nvarchar](1024) NULL,
	[VideoURL] [varchar](512) NULL,
	[Latitude] [decimal](9, 6) NULL,
	[Longitude] [decimal](9, 6) NULL,
	[WelComeText] [varchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_AcademicBranches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_AcademicBranches] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[AcademicBranches] ON
INSERT [dbo].[AcademicBranches] ([Id], [InstituteId], [Name], [About], [BannerUrl], [LogoUrl], [Description], [VideoURL], [Latitude], [Longitude], [WelComeText], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'Dhanmondi', N'Dhanmondi branch', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[AcademicBranches] OFF
/****** Object:  Table [dbo].[BloodGroups]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BloodGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_BloodGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_BloodGroups] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BloodGroups] ON
INSERT [dbo].[BloodGroups] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'O+', NULL, 1, NULL)
INSERT [dbo].[BloodGroups] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (2, 1, N'O Negetive', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[BloodGroups] OFF
/****** Object:  Table [dbo].[AttendanceTypes]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendanceTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Flag] [nvarchar](2) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsUsedForStudent] [bit] NOT NULL,
	[IsUsedForEmployee] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
	[ColourId] [int] NOT NULL,
 CONSTRAINT [PK_AttendanceTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AttendanceTypes] ON
INSERT [dbo].[AttendanceTypes] ([Id], [InstituteId], [Name], [Flag], [Description], [IsUsedForStudent], [IsUsedForEmployee], [IsActive], [LastUpdateTime], [ColourId]) VALUES (3, 1, N'Present', N'P', N'Present', 1, 1, 1, NULL, 1)
INSERT [dbo].[AttendanceTypes] ([Id], [InstituteId], [Name], [Flag], [Description], [IsUsedForStudent], [IsUsedForEmployee], [IsActive], [LastUpdateTime], [ColourId]) VALUES (8, 1, N'Absent', N'A', N'Absent', 1, 1, 1, NULL, 2)
SET IDENTITY_INSERT [dbo].[AttendanceTypes] OFF
/****** Object:  Table [dbo].[AddressTypes]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_AddressTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_AddressTypes] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AddressTypes] ON
INSERT [dbo].[AddressTypes] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'Present', N'present address', 1, NULL)
INSERT [dbo].[AddressTypes] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (2, 1, N'Permanent', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[AddressTypes] OFF
/****** Object:  Table [dbo].[Designations]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Designations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Designations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Designations] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Designations] ON
INSERT [dbo].[Designations] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'Principle', NULL, 1, NULL)
INSERT [dbo].[Designations] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (2, 1, N'Assistant Principle', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[Designations] OFF
/****** Object:  Table [dbo].[Departments]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Departments] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Departments] ON
INSERT [dbo].[Departments] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'Accounts', NULL, 1, NULL)
INSERT [dbo].[Departments] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (2, 1, N'Security', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[Departments] OFF
/****** Object:  Table [dbo].[Countries]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Countries] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Countries] ON
INSERT [dbo].[Countries] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'Bangladesh', NULL, 1, NULL)
INSERT [dbo].[Countries] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (2, 1, N'USA', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[Countries] OFF
/****** Object:  Table [dbo].[EducationalQualifications]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EducationalQualifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_EducationalQualifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_EducationalQualifications] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[EducationalQualifications] ON
INSERT [dbo].[EducationalQualifications] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'SSC', NULL, 1, NULL)
INSERT [dbo].[EducationalQualifications] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (3, 1, N'Masters', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[EducationalQualifications] OFF
/****** Object:  Table [dbo].[GuardianTypes]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GuardianTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_GuardianTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GuardianTypes] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GuardianTypes] ON
INSERT [dbo].[GuardianTypes] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'Father', NULL, 1, NULL)
INSERT [dbo].[GuardianTypes] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (2, 1, N'Mother', NULL, 1, NULL)
INSERT [dbo].[GuardianTypes] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (3, 1, N'House Tutor', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[GuardianTypes] OFF
/****** Object:  Table [dbo].[Nationalities]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nationalities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Nationalities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Nationalities] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Nationalities] ON
INSERT [dbo].[Nationalities] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'Bangladeshi', NULL, 1, NULL)
INSERT [dbo].[Nationalities] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (2, 1, N'American', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[Nationalities] OFF
/****** Object:  Table [dbo].[MaritalStatuses]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaritalStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_MaritalStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_MaritalStatuses] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[MaritalStatuses] ON
INSERT [dbo].[MaritalStatuses] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'Married', NULL, 1, NULL)
INSERT [dbo].[MaritalStatuses] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (2, 1, N'Bachelor', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[MaritalStatuses] OFF
/****** Object:  Table [dbo].[Genders]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Genders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Genders] UNIQUE NONCLUSTERED 
(
	[InstituteId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Genders] ON
INSERT [dbo].[Genders] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'Male', NULL, 1, NULL)
INSERT [dbo].[Genders] ([Id], [InstituteId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (2, 1, N'Female', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[Genders] OFF
/****** Object:  Table [dbo].[UserInfoes]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfoes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[PIN] [nchar](128) NULL,
	[FirstName] [nvarchar](128) NULL,
	[LastName] [nvarchar](128) NULL,
	[Name] [nvarchar](128) NOT NULL,
	[ContactNumber1] [nvarchar](32) NULL,
	[ContactNumber2] [nvarchar](32) NULL,
	[EmailAddress] [nvarchar](128) NULL,
	[SSN] [nvarchar](128) NULL,
	[PassportNo] [nvarchar](128) NULL,
	[DOB] [date] NULL,
	[PhotoUrl] [nvarchar](128) NULL,
	[SmallPhotoUrl] [nvarchar](128) NULL,
	[GenderId] [int] NULL,
	[NationalityId] [int] NULL,
	[ReligionId] [int] NULL,
	[BloodGroupId] [int] NULL,
	[GoogleId] [nvarchar](128) NULL,
	[FacebookId] [nvarchar](128) NULL,
	[MicrosoftId] [nvarchar](128) NULL,
	[TwitterId] [nvarchar](128) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_UserInfoes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserInfoes] ON
INSERT [dbo].[UserInfoes] ([Id], [InstituteId], [PIN], [FirstName], [LastName], [Name], [ContactNumber1], [ContactNumber2], [EmailAddress], [SSN], [PassportNo], [DOB], [PhotoUrl], [SmallPhotoUrl], [GenderId], [NationalityId], [ReligionId], [BloodGroupId], [GoogleId], [FacebookId], [MicrosoftId], [TwitterId], [IsActive], [LastUpdateTime]) VALUES (1, 1, NULL, NULL, NULL, N'Faisal', NULL, NULL, NULL, NULL, NULL, CAST(0x712F0B00 AS Date), NULL, NULL, 1, 1, 1, 1, NULL, NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[UserInfoes] ([Id], [InstituteId], [PIN], [FirstName], [LastName], [Name], [ContactNumber1], [ContactNumber2], [EmailAddress], [SSN], [PassportNo], [DOB], [PhotoUrl], [SmallPhotoUrl], [GenderId], [NationalityId], [ReligionId], [BloodGroupId], [GoogleId], [FacebookId], [MicrosoftId], [TwitterId], [IsActive], [LastUpdateTime]) VALUES (2, 1, NULL, NULL, NULL, N'Abul', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, 1, 1, 1, NULL, NULL, NULL, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[UserInfoes] OFF
/****** Object:  Table [dbo].[DistrictOrStates]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DistrictOrStates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_DistrictOrStates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DistrictOrStates] ON
INSERT [dbo].[DistrictOrStates] ([Id], [CountryId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (1, 1, N'Dhaka', NULL, 1, NULL)
INSERT [dbo].[DistrictOrStates] ([Id], [CountryId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (2, 2, N'Alaska', NULL, 1, NULL)
INSERT [dbo].[DistrictOrStates] ([Id], [CountryId], [Name], [Description], [IsActive], [LastUpdateTime]) VALUES (3, 1, N'Chittagong', NULL, 1, CAST(0x0000A47700E03F29 AS DateTime))
SET IDENTITY_INSERT [dbo].[DistrictOrStates] OFF
/****** Object:  Table [dbo].[Addresses]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AddressTypeId] [int] NOT NULL,
	[RefCode] [nchar](8) NOT NULL,
	[RefPrimaryKey] [int] NOT NULL,
	[DistrictOrStateId] [int] NULL,
	[ZipCode] [nvarchar](32) NULL,
	[AddressBody] [nvarchar](512) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Addresses] ON
INSERT [dbo].[Addresses] ([Id], [AddressTypeId], [RefCode], [RefPrimaryKey], [DistrictOrStateId], [ZipCode], [AddressBody], [IsActive], [LastUpdateTime]) VALUES (2, 1, N'STU     ', 1, 1, N'1200', N'Gulshan1', 1, NULL)
INSERT [dbo].[Addresses] ([Id], [AddressTypeId], [RefCode], [RefPrimaryKey], [DistrictOrStateId], [ZipCode], [AddressBody], [IsActive], [LastUpdateTime]) VALUES (4, 2, N'STU     ', 1, 1, N'1234', N'Dhaka', 1, NULL)
SET IDENTITY_INSERT [dbo].[Addresses] OFF
/****** Object:  Table [dbo].[Guardians]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guardians](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuardianId] [int] NOT NULL,
	[GuardianTypeId] [int] NOT NULL,
	[EducationalQualificationId] [int] NULL,
	[ProfessionId] [int] NULL,
	[MonthlyIncome] [int] NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Guardians] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Guardians] UNIQUE NONCLUSTERED 
(
	[GuardianId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Guardians] ON
INSERT [dbo].[Guardians] ([Id], [GuardianId], [GuardianTypeId], [EducationalQualificationId], [ProfessionId], [MonthlyIncome], [LastUpdateTime]) VALUES (1, 2, 1, 1, 1, 100000, NULL)
SET IDENTITY_INSERT [dbo].[Guardians] OFF
/****** Object:  Table [dbo].[Employees]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[FatherName] [nvarchar](128) NULL,
	[MotherName] [nvarchar](128) NULL,
	[MaritalStatusId] [int] NULL,
	[DesignationId] [int] NULL,
	[DepartmentId] [int] NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeacherId] [int] NOT NULL,
	[FatherName] [nvarchar](128) NULL,
	[MotherName] [nvarchar](128) NULL,
	[MaritalStatusId] [int] NULL,
	[DesignationId] [int] NULL,
	[CurrentAcademicBranchId] [int] NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Teachers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Teachers] UNIQUE NONCLUSTERED 
(
	[TeacherId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Teachers] ON
INSERT [dbo].[Teachers] ([Id], [TeacherId], [FatherName], [MotherName], [MaritalStatusId], [DesignationId], [CurrentAcademicBranchId], [LastUpdateTime]) VALUES (1, 1, N'SS', N'SS', 1, 1, 1, NULL)
SET IDENTITY_INSERT [dbo].[Teachers] OFF
/****** Object:  Table [dbo].[Students]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[CurrentAcademicSessionId] [int] NULL,
	[CurrentAcademicBranchId] [int] NULL,
	[CurrentAcademicClassId] [int] NULL,
	[CurrentAcademicShiftId] [int] NULL,
	[CurrentAcademicSectionId] [int] NULL,
	[CurrentAcademicVerssionId] [int] NULL,
	[CurrentAcademicGroupId] [int] NULL,
	[CurrentRollNo] [nvarchar](128) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Students] UNIQUE NONCLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Students] ON
INSERT [dbo].[Students] ([Id], [StudentId], [CurrentAcademicSessionId], [CurrentAcademicBranchId], [CurrentAcademicClassId], [CurrentAcademicShiftId], [CurrentAcademicSectionId], [CurrentAcademicVerssionId], [CurrentAcademicGroupId], [CurrentRollNo], [LastUpdateTime]) VALUES (1, 1, 1, 1, 1, 1, 1, 1, 1, N'1', NULL)
INSERT [dbo].[Students] ([Id], [StudentId], [CurrentAcademicSessionId], [CurrentAcademicBranchId], [CurrentAcademicClassId], [CurrentAcademicShiftId], [CurrentAcademicSectionId], [CurrentAcademicVerssionId], [CurrentAcademicGroupId], [CurrentRollNo], [LastUpdateTime]) VALUES (2, 2, 1, 1, 1, 1, 1, 1, 1, N'2', NULL)
SET IDENTITY_INSERT [dbo].[Students] OFF
/****** Object:  Table [dbo].[Siblings]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Siblings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[SiblingId] [int] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Siblings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GuardiansOfStudents]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GuardiansOfStudents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[GuardianId] [int] NOT NULL,
	[IsLocalGuardian] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_GuardiansOfStudents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GuardiansOfStudents] ON
INSERT [dbo].[GuardiansOfStudents] ([Id], [StudentId], [GuardianId], [IsLocalGuardian], [LastUpdateTime]) VALUES (1, 1, 2, 1, NULL)
SET IDENTITY_INSERT [dbo].[GuardiansOfStudents] OFF
/****** Object:  Table [dbo].[StudentAttendances]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentAttendances](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AttendanceDate] [datetime] NOT NULL,
	[TeacherId] [int] NOT NULL,
	[AcademicSessionId] [int] NOT NULL,
	[AcademicBranchId] [int] NOT NULL,
	[AcademicClassId] [int] NOT NULL,
	[AcademicShiftId] [int] NULL,
	[AcademicSectionId] [int] NULL,
	[AcamedicGroupId] [int] NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_StudentAttendances] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[StudentAttendances] ON
INSERT [dbo].[StudentAttendances] ([Id], [AttendanceDate], [TeacherId], [AcademicSessionId], [AcademicBranchId], [AcademicClassId], [AcademicShiftId], [AcademicSectionId], [AcamedicGroupId], [LastUpdateTime]) VALUES (4, CAST(0x0000A4770052A79F AS DateTime), 1, 1, 1, 2, 1, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[StudentAttendances] OFF
/****** Object:  Table [dbo].[StudentAttendanceDetails]    Script Date: 04/17/2015 13:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentAttendanceDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentAttendanceId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[AttendanceTypeId] [int] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_StudentAttendanceDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[StudentAttendanceDetails] ON
INSERT [dbo].[StudentAttendanceDetails] ([Id], [StudentAttendanceId], [StudentId], [AttendanceTypeId], [LastUpdateTime]) VALUES (1, 4, 1, 3, CAST(0x0000A47700B77783 AS DateTime))
INSERT [dbo].[StudentAttendanceDetails] ([Id], [StudentAttendanceId], [StudentId], [AttendanceTypeId], [LastUpdateTime]) VALUES (2, 4, 2, 3, CAST(0x0000A47700B77790 AS DateTime))
SET IDENTITY_INSERT [dbo].[StudentAttendanceDetails] OFF
/****** Object:  ForeignKey [FK_Institutes_Packages]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Institutes]  WITH CHECK ADD  CONSTRAINT [FK_Institutes_Packages] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Packages] ([Id])
GO
ALTER TABLE [dbo].[Institutes] CHECK CONSTRAINT [FK_Institutes_Packages]
GO
/****** Object:  ForeignKey [FK_Religions_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Religions]  WITH CHECK ADD  CONSTRAINT [FK_Religions_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[Religions] CHECK CONSTRAINT [FK_Religions_Institutes]
GO
/****** Object:  ForeignKey [FK_Professions_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Professions]  WITH CHECK ADD  CONSTRAINT [FK_Professions_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[Professions] CHECK CONSTRAINT [FK_Professions_Institutes]
GO
/****** Object:  ForeignKey [FK_AcademicVersions_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[AcademicVersions]  WITH CHECK ADD  CONSTRAINT [FK_AcademicVersions_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[AcademicVersions] CHECK CONSTRAINT [FK_AcademicVersions_Institutes]
GO
/****** Object:  ForeignKey [FK_AcademicShifts_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[AcademicShifts]  WITH CHECK ADD  CONSTRAINT [FK_AcademicShifts_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[AcademicShifts] CHECK CONSTRAINT [FK_AcademicShifts_Institutes]
GO
/****** Object:  ForeignKey [FK_AcademicSessions_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[AcademicSessions]  WITH CHECK ADD  CONSTRAINT [FK_AcademicSessions_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[AcademicSessions] CHECK CONSTRAINT [FK_AcademicSessions_Institutes]
GO
/****** Object:  ForeignKey [FK_AcademicSections_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[AcademicSections]  WITH CHECK ADD  CONSTRAINT [FK_AcademicSections_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[AcademicSections] CHECK CONSTRAINT [FK_AcademicSections_Institutes]
GO
/****** Object:  ForeignKey [FK_AcademicGroups_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[AcademicGroups]  WITH CHECK ADD  CONSTRAINT [FK_AcademicGroups_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[AcademicGroups] CHECK CONSTRAINT [FK_AcademicGroups_Institutes]
GO
/****** Object:  ForeignKey [FK_AcademicClasses_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[AcademicClasses]  WITH CHECK ADD  CONSTRAINT [FK_AcademicClasses_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[AcademicClasses] CHECK CONSTRAINT [FK_AcademicClasses_Institutes]
GO
/****** Object:  ForeignKey [FK_AcademicBranches_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[AcademicBranches]  WITH CHECK ADD  CONSTRAINT [FK_AcademicBranches_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[AcademicBranches] CHECK CONSTRAINT [FK_AcademicBranches_Institutes]
GO
/****** Object:  ForeignKey [FK_BloodGroups_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[BloodGroups]  WITH CHECK ADD  CONSTRAINT [FK_BloodGroups_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[BloodGroups] CHECK CONSTRAINT [FK_BloodGroups_Institutes]
GO
/****** Object:  ForeignKey [FK_AttendanceTypes_Colours]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[AttendanceTypes]  WITH CHECK ADD  CONSTRAINT [FK_AttendanceTypes_Colours] FOREIGN KEY([ColourId])
REFERENCES [dbo].[Colours] ([Id])
GO
ALTER TABLE [dbo].[AttendanceTypes] CHECK CONSTRAINT [FK_AttendanceTypes_Colours]
GO
/****** Object:  ForeignKey [FK_AttendanceTypes_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[AttendanceTypes]  WITH CHECK ADD  CONSTRAINT [FK_AttendanceTypes_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[AttendanceTypes] CHECK CONSTRAINT [FK_AttendanceTypes_Institutes]
GO
/****** Object:  ForeignKey [FK_AddressTypes_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[AddressTypes]  WITH CHECK ADD  CONSTRAINT [FK_AddressTypes_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[AddressTypes] CHECK CONSTRAINT [FK_AddressTypes_Institutes]
GO
/****** Object:  ForeignKey [FK_Designations_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Designations]  WITH CHECK ADD  CONSTRAINT [FK_Designations_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[Designations] CHECK CONSTRAINT [FK_Designations_Institutes]
GO
/****** Object:  ForeignKey [FK_Departments_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Departments]  WITH CHECK ADD  CONSTRAINT [FK_Departments_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[Departments] CHECK CONSTRAINT [FK_Departments_Institutes]
GO
/****** Object:  ForeignKey [FK_Countries_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Countries]  WITH CHECK ADD  CONSTRAINT [FK_Countries_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[Countries] CHECK CONSTRAINT [FK_Countries_Institutes]
GO
/****** Object:  ForeignKey [FK_EducationalQualifications_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[EducationalQualifications]  WITH CHECK ADD  CONSTRAINT [FK_EducationalQualifications_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[EducationalQualifications] CHECK CONSTRAINT [FK_EducationalQualifications_Institutes]
GO
/****** Object:  ForeignKey [FK_GuardianTypes_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[GuardianTypes]  WITH CHECK ADD  CONSTRAINT [FK_GuardianTypes_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[GuardianTypes] CHECK CONSTRAINT [FK_GuardianTypes_Institutes]
GO
/****** Object:  ForeignKey [FK_Nationalities_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Nationalities]  WITH CHECK ADD  CONSTRAINT [FK_Nationalities_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[Nationalities] CHECK CONSTRAINT [FK_Nationalities_Institutes]
GO
/****** Object:  ForeignKey [FK_MaritalStatuses_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[MaritalStatuses]  WITH CHECK ADD  CONSTRAINT [FK_MaritalStatuses_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[MaritalStatuses] CHECK CONSTRAINT [FK_MaritalStatuses_Institutes]
GO
/****** Object:  ForeignKey [FK_Genders_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Genders]  WITH CHECK ADD  CONSTRAINT [FK_Genders_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[Genders] CHECK CONSTRAINT [FK_Genders_Institutes]
GO
/****** Object:  ForeignKey [FK_UserInfoes_BloodGroups]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[UserInfoes]  WITH CHECK ADD  CONSTRAINT [FK_UserInfoes_BloodGroups] FOREIGN KEY([BloodGroupId])
REFERENCES [dbo].[BloodGroups] ([Id])
GO
ALTER TABLE [dbo].[UserInfoes] CHECK CONSTRAINT [FK_UserInfoes_BloodGroups]
GO
/****** Object:  ForeignKey [FK_UserInfoes_Genders]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[UserInfoes]  WITH CHECK ADD  CONSTRAINT [FK_UserInfoes_Genders] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Genders] ([Id])
GO
ALTER TABLE [dbo].[UserInfoes] CHECK CONSTRAINT [FK_UserInfoes_Genders]
GO
/****** Object:  ForeignKey [FK_UserInfoes_Institutes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[UserInfoes]  WITH CHECK ADD  CONSTRAINT [FK_UserInfoes_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO
ALTER TABLE [dbo].[UserInfoes] CHECK CONSTRAINT [FK_UserInfoes_Institutes]
GO
/****** Object:  ForeignKey [FK_UserInfoes_Nationalities]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[UserInfoes]  WITH CHECK ADD  CONSTRAINT [FK_UserInfoes_Nationalities] FOREIGN KEY([NationalityId])
REFERENCES [dbo].[Nationalities] ([Id])
GO
ALTER TABLE [dbo].[UserInfoes] CHECK CONSTRAINT [FK_UserInfoes_Nationalities]
GO
/****** Object:  ForeignKey [FK_UserInfoes_Religions]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[UserInfoes]  WITH CHECK ADD  CONSTRAINT [FK_UserInfoes_Religions] FOREIGN KEY([ReligionId])
REFERENCES [dbo].[Religions] ([Id])
GO
ALTER TABLE [dbo].[UserInfoes] CHECK CONSTRAINT [FK_UserInfoes_Religions]
GO
/****** Object:  ForeignKey [FK_DistrictOrStates_Countries]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[DistrictOrStates]  WITH CHECK ADD  CONSTRAINT [FK_DistrictOrStates_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[DistrictOrStates] CHECK CONSTRAINT [FK_DistrictOrStates_Countries]
GO
/****** Object:  ForeignKey [FK_Addresses_AddressTypes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_AddressTypes] FOREIGN KEY([AddressTypeId])
REFERENCES [dbo].[AddressTypes] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_AddressTypes]
GO
/****** Object:  ForeignKey [FK_Addresses_DistrictOrStates]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_DistrictOrStates] FOREIGN KEY([DistrictOrStateId])
REFERENCES [dbo].[DistrictOrStates] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_DistrictOrStates]
GO
/****** Object:  ForeignKey [FK_Guardians_EducationalQualifications]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Guardians]  WITH CHECK ADD  CONSTRAINT [FK_Guardians_EducationalQualifications] FOREIGN KEY([EducationalQualificationId])
REFERENCES [dbo].[EducationalQualifications] ([Id])
GO
ALTER TABLE [dbo].[Guardians] CHECK CONSTRAINT [FK_Guardians_EducationalQualifications]
GO
/****** Object:  ForeignKey [FK_Guardians_GuardianTypes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Guardians]  WITH CHECK ADD  CONSTRAINT [FK_Guardians_GuardianTypes] FOREIGN KEY([GuardianTypeId])
REFERENCES [dbo].[GuardianTypes] ([Id])
GO
ALTER TABLE [dbo].[Guardians] CHECK CONSTRAINT [FK_Guardians_GuardianTypes]
GO
/****** Object:  ForeignKey [FK_Guardians_Professions]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Guardians]  WITH CHECK ADD  CONSTRAINT [FK_Guardians_Professions] FOREIGN KEY([ProfessionId])
REFERENCES [dbo].[Professions] ([Id])
GO
ALTER TABLE [dbo].[Guardians] CHECK CONSTRAINT [FK_Guardians_Professions]
GO
/****** Object:  ForeignKey [FK_Guardians_UserInfoes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Guardians]  WITH CHECK ADD  CONSTRAINT [FK_Guardians_UserInfoes] FOREIGN KEY([GuardianId])
REFERENCES [dbo].[UserInfoes] ([Id])
GO
ALTER TABLE [dbo].[Guardians] CHECK CONSTRAINT [FK_Guardians_UserInfoes]
GO
/****** Object:  ForeignKey [FK_Employees_Departments]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Departments]
GO
/****** Object:  ForeignKey [FK_Employees_Designations]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Designations] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[Designations] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Designations]
GO
/****** Object:  ForeignKey [FK_Employees_MaritalStatuses]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_MaritalStatuses] FOREIGN KEY([MaritalStatusId])
REFERENCES [dbo].[MaritalStatuses] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_MaritalStatuses]
GO
/****** Object:  ForeignKey [FK_Employees_UserInfoes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_UserInfoes] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[UserInfoes] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_UserInfoes]
GO
/****** Object:  ForeignKey [FK_Teachers_AcademicBranches]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD  CONSTRAINT [FK_Teachers_AcademicBranches] FOREIGN KEY([CurrentAcademicBranchId])
REFERENCES [dbo].[AcademicBranches] ([Id])
GO
ALTER TABLE [dbo].[Teachers] CHECK CONSTRAINT [FK_Teachers_AcademicBranches]
GO
/****** Object:  ForeignKey [FK_Teachers_Designations]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD  CONSTRAINT [FK_Teachers_Designations] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[Designations] ([Id])
GO
ALTER TABLE [dbo].[Teachers] CHECK CONSTRAINT [FK_Teachers_Designations]
GO
/****** Object:  ForeignKey [FK_Teachers_MaritalStatuses]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD  CONSTRAINT [FK_Teachers_MaritalStatuses] FOREIGN KEY([MaritalStatusId])
REFERENCES [dbo].[MaritalStatuses] ([Id])
GO
ALTER TABLE [dbo].[Teachers] CHECK CONSTRAINT [FK_Teachers_MaritalStatuses]
GO
/****** Object:  ForeignKey [FK_Teachers_UserInfoes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD  CONSTRAINT [FK_Teachers_UserInfoes] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[UserInfoes] ([Id])
GO
ALTER TABLE [dbo].[Teachers] CHECK CONSTRAINT [FK_Teachers_UserInfoes]
GO
/****** Object:  ForeignKey [FK_Students_AcademicBranches]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_AcademicBranches] FOREIGN KEY([CurrentAcademicBranchId])
REFERENCES [dbo].[AcademicBranches] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_AcademicBranches]
GO
/****** Object:  ForeignKey [FK_Students_AcademicClasses]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_AcademicClasses] FOREIGN KEY([CurrentAcademicClassId])
REFERENCES [dbo].[AcademicClasses] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_AcademicClasses]
GO
/****** Object:  ForeignKey [FK_Students_AcademicGroups]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_AcademicGroups] FOREIGN KEY([CurrentAcademicGroupId])
REFERENCES [dbo].[AcademicGroups] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_AcademicGroups]
GO
/****** Object:  ForeignKey [FK_Students_AcademicSections]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_AcademicSections] FOREIGN KEY([CurrentAcademicSectionId])
REFERENCES [dbo].[AcademicSections] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_AcademicSections]
GO
/****** Object:  ForeignKey [FK_Students_AcademicSessions]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_AcademicSessions] FOREIGN KEY([CurrentAcademicSessionId])
REFERENCES [dbo].[AcademicSessions] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_AcademicSessions]
GO
/****** Object:  ForeignKey [FK_Students_AcademicShifts]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_AcademicShifts] FOREIGN KEY([CurrentAcademicShiftId])
REFERENCES [dbo].[AcademicShifts] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_AcademicShifts]
GO
/****** Object:  ForeignKey [FK_Students_AcademicVersions]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_AcademicVersions] FOREIGN KEY([CurrentAcademicVerssionId])
REFERENCES [dbo].[AcademicVersions] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_AcademicVersions]
GO
/****** Object:  ForeignKey [FK_Students_UserInfoes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_UserInfoes] FOREIGN KEY([StudentId])
REFERENCES [dbo].[UserInfoes] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_UserInfoes]
GO
/****** Object:  ForeignKey [FK_Siblings_SiblingStudents]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Siblings]  WITH CHECK ADD  CONSTRAINT [FK_Siblings_SiblingStudents] FOREIGN KEY([SiblingId])
REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[Siblings] CHECK CONSTRAINT [FK_Siblings_SiblingStudents]
GO
/****** Object:  ForeignKey [FK_Siblings_Students]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[Siblings]  WITH CHECK ADD  CONSTRAINT [FK_Siblings_Students] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[Siblings] CHECK CONSTRAINT [FK_Siblings_Students]
GO
/****** Object:  ForeignKey [FK_GuardiansOfStudents_Guardians]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[GuardiansOfStudents]  WITH CHECK ADD  CONSTRAINT [FK_GuardiansOfStudents_Guardians] FOREIGN KEY([GuardianId])
REFERENCES [dbo].[Guardians] ([GuardianId])
GO
ALTER TABLE [dbo].[GuardiansOfStudents] CHECK CONSTRAINT [FK_GuardiansOfStudents_Guardians]
GO
/****** Object:  ForeignKey [FK_GuardiansOfStudents_Students]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[GuardiansOfStudents]  WITH CHECK ADD  CONSTRAINT [FK_GuardiansOfStudents_Students] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[GuardiansOfStudents] CHECK CONSTRAINT [FK_GuardiansOfStudents_Students]
GO
/****** Object:  ForeignKey [FK_StudentAttendances_AcademicBranches]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[StudentAttendances]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendances_AcademicBranches] FOREIGN KEY([AcademicBranchId])
REFERENCES [dbo].[AcademicBranches] ([Id])
GO
ALTER TABLE [dbo].[StudentAttendances] CHECK CONSTRAINT [FK_StudentAttendances_AcademicBranches]
GO
/****** Object:  ForeignKey [FK_StudentAttendances_AcademicClasses]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[StudentAttendances]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendances_AcademicClasses] FOREIGN KEY([AcademicClassId])
REFERENCES [dbo].[AcademicClasses] ([Id])
GO
ALTER TABLE [dbo].[StudentAttendances] CHECK CONSTRAINT [FK_StudentAttendances_AcademicClasses]
GO
/****** Object:  ForeignKey [FK_StudentAttendances_AcademicGroups]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[StudentAttendances]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendances_AcademicGroups] FOREIGN KEY([AcamedicGroupId])
REFERENCES [dbo].[AcademicGroups] ([Id])
GO
ALTER TABLE [dbo].[StudentAttendances] CHECK CONSTRAINT [FK_StudentAttendances_AcademicGroups]
GO
/****** Object:  ForeignKey [FK_StudentAttendances_AcademicSections]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[StudentAttendances]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendances_AcademicSections] FOREIGN KEY([AcademicSectionId])
REFERENCES [dbo].[AcademicSections] ([Id])
GO
ALTER TABLE [dbo].[StudentAttendances] CHECK CONSTRAINT [FK_StudentAttendances_AcademicSections]
GO
/****** Object:  ForeignKey [FK_StudentAttendances_AcademicSessions]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[StudentAttendances]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendances_AcademicSessions] FOREIGN KEY([AcademicSessionId])
REFERENCES [dbo].[AcademicSessions] ([Id])
GO
ALTER TABLE [dbo].[StudentAttendances] CHECK CONSTRAINT [FK_StudentAttendances_AcademicSessions]
GO
/****** Object:  ForeignKey [FK_StudentAttendances_AcademicShifts]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[StudentAttendances]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendances_AcademicShifts] FOREIGN KEY([AcademicShiftId])
REFERENCES [dbo].[AcademicShifts] ([Id])
GO
ALTER TABLE [dbo].[StudentAttendances] CHECK CONSTRAINT [FK_StudentAttendances_AcademicShifts]
GO
/****** Object:  ForeignKey [FK_StudentAttendances_Teachers]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[StudentAttendances]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendances_Teachers] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teachers] ([Id])
GO
ALTER TABLE [dbo].[StudentAttendances] CHECK CONSTRAINT [FK_StudentAttendances_Teachers]
GO
/****** Object:  ForeignKey [FK_StudentAttendanceDetails_AttendanceTypes]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[StudentAttendanceDetails]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendanceDetails_AttendanceTypes] FOREIGN KEY([AttendanceTypeId])
REFERENCES [dbo].[AttendanceTypes] ([Id])
GO
ALTER TABLE [dbo].[StudentAttendanceDetails] CHECK CONSTRAINT [FK_StudentAttendanceDetails_AttendanceTypes]
GO
/****** Object:  ForeignKey [FK_StudentAttendanceDetails_StudentAttendances]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[StudentAttendanceDetails]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendanceDetails_StudentAttendances] FOREIGN KEY([StudentAttendanceId])
REFERENCES [dbo].[StudentAttendances] ([Id])
GO
ALTER TABLE [dbo].[StudentAttendanceDetails] CHECK CONSTRAINT [FK_StudentAttendanceDetails_StudentAttendances]
GO
/****** Object:  ForeignKey [FK_StudentAttendanceDetails_Students]    Script Date: 04/17/2015 13:58:28 ******/
ALTER TABLE [dbo].[StudentAttendanceDetails]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendanceDetails_Students] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([Id])
GO
ALTER TABLE [dbo].[StudentAttendanceDetails] CHECK CONSTRAINT [FK_StudentAttendanceDetails_Students]
GO

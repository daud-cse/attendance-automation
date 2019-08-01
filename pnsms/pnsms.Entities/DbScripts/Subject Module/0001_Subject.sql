USE [PNSMS]
GO

ALTER TABLE [dbo].[Subject] DROP CONSTRAINT [FK_Subject_Tags]
GO

ALTER TABLE [dbo].[Subject] DROP CONSTRAINT [FK_Subject_Subject]
GO

ALTER TABLE [dbo].[Subject] DROP CONSTRAINT [FK_Subject_Institutes]
GO

/****** Object:  Table [dbo].[Subject]    Script Date: 10/12/2017 1:19:09 AM ******/
DROP TABLE [dbo].[Subject]
GO

/****** Object:  Table [dbo].[Subject]    Script Date: 10/12/2017 1:19:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Subject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[ParentSubjectId] [int] NULL,
	[Description] [varchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[TagId] [int] NULL,
	[LastUpdateTime] [datetime] NULL,
	[Code] [nvarchar](5) NULL,
	[OrderBy] [int] NULL,
	[ShortName] [nvarchar](32) NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_Subject_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO

ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_Subject_Institutes]
GO

ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_Subject_Subject] FOREIGN KEY([ParentSubjectId])
REFERENCES [dbo].[Subject] ([Id])
GO

ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_Subject_Subject]
GO

ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_Subject_Tags] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([Id])
GO

ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_Subject_Tags]
GO



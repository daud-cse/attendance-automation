USE [PNSMS]
GO

ALTER TABLE [dbo].[ExamTypeWiseTabulationSheetMaster] DROP CONSTRAINT [FK_ExamTypeWiseTabulationSheetMaster_ExamType]
GO

/****** Object:  Table [dbo].[ExamTypeWiseTabulationSheetMaster]    Script Date: 05-12-2017 1:11:26 PM ******/
DROP TABLE [dbo].[ExamTypeWiseTabulationSheetMaster]
GO

/****** Object:  Table [dbo].[ExamTypeWiseTabulationSheetMaster]    Script Date: 05-12-2017 1:11:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ExamTypeWiseTabulationSheetMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NULL,
	[ExamTypeId] [int] NULL,
	[AcademicClassesId] [int] NULL,
	[AcademicClassName] [nvarchar](256) NULL,
	[AcademicGroupId] [int] NULL,
	[AcademicGroupName] [nvarchar](256) NULL,
	[AcademicSectionId] [int] NULL,
	[AcademicSectionName] [nvarchar](256) NULL,
	[AcademicSessionId] [int] NULL,
	[AcademicSessionName] [nvarchar](256) NULL,
	[StudentId] [int] NULL,
	[StudentName] [varchar](128) NULL,
	[TotalMarks] [decimal](18, 2) NULL,
	[TotalSubject] [int] NULL,
	[AverageNumber] [decimal](18, 2) NULL,
	[ExamGradeId] [int] NULL,
	[ExamGradeName] [varchar](500) NULL,
	[ExamGradePoint] [decimal](18, 2) NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_ExamTypeWiseTabulation Sheet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ExamTypeWiseTabulationSheetMaster]  WITH CHECK ADD  CONSTRAINT [FK_ExamTypeWiseTabulationSheetMaster_ExamType] FOREIGN KEY([ExamTypeId])
REFERENCES [dbo].[ExamType] ([Id])
GO

ALTER TABLE [dbo].[ExamTypeWiseTabulationSheetMaster] CHECK CONSTRAINT [FK_ExamTypeWiseTabulationSheetMaster_ExamType]
GO



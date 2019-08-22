USE [PNSMS]
GO

ALTER TABLE [dbo].[ExamTypeWiseTabulationSheetDetails] DROP CONSTRAINT [FK_ExamTypeWiseTabulationSheetDetails_ExamType]
GO

/****** Object:  Table [dbo].[ExamTypeWiseTabulationSheetDetails]    Script Date: 05-12-2017 1:11:07 PM ******/
DROP TABLE [dbo].[ExamTypeWiseTabulationSheetDetails]
GO

/****** Object:  Table [dbo].[ExamTypeWiseTabulationSheetDetails]    Script Date: 05-12-2017 1:11:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ExamTypeWiseTabulationSheetDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExamTypeId] [int] NOT NULL,
	[InstituteId] [int] NOT NULL,
	[AcademicClassesId] [int] NULL,
	[AcademicClassName] [nvarchar](256) NULL,
	[AcademicGroupId] [int] NULL,
	[AcademicGroupName] [nvarchar](256) NULL,
	[AcademicSectionId] [int] NULL,
	[AcademicSectionName] [nvarchar](256) NULL,
	[AcademicSessionId] [int] NULL,
	[AcademicSessionName] [nvarchar](256) NULL,
	[StudentId] [int] NULL,
	[InstituteSubjectClassId] [int] NULL,
	[InstituteSubjectName] [varchar](500) NULL,
	[TotalMarks] [decimal](18, 2) NOT NULL,
	[AverageMarks] [decimal](18, 2) NOT NULL,
	[AcceptTotalMarks] [decimal](18, 2) NOT NULL,
	[ExamGradeId] [int] NULL,
	[ExamGradeName] [varchar](500) NULL,
	[ExamGradePoint] [decimal](18, 2) NULL,
 CONSTRAINT [PK_ExamWiseTotalMarks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ExamTypeWiseTabulationSheetDetails]  WITH CHECK ADD  CONSTRAINT [FK_ExamTypeWiseTabulationSheetDetails_ExamType] FOREIGN KEY([ExamTypeId])
REFERENCES [dbo].[ExamType] ([Id])
GO

ALTER TABLE [dbo].[ExamTypeWiseTabulationSheetDetails] CHECK CONSTRAINT [FK_ExamTypeWiseTabulationSheetDetails_ExamType]
GO



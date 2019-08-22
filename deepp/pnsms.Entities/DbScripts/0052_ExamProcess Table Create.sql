USE [PNSMS]
GO

/****** Object:  Table [dbo].[ExamProcesses]    Script Date: 30/11/2017 12:41:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ExamProcesses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[AcademicSessionId] [int] NOT NULL,
	[ExamTypeId] [int] NOT NULL,
	[AcademicClassesId] [int] NOT NULL,
	[AcademicBranchId] [int] NOT NULL,
	[AcademicShiftId] [int] NULL,
	[AcademicSectionId] [int] NULL,
	[Name] [nvarchar](128) NOT NULL,
	[RunExamProcessAt] [datetime] NULL,
	[RunReportCardProcessAt] [datetime] NULL,
	[RunConsolidateReportProcessAt] [datetime] NULL,
 CONSTRAINT [PK_ExamProcesses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ExamProcesses]  WITH CHECK ADD  CONSTRAINT [FK_ExamProcesses_AcademicBranches] FOREIGN KEY([AcademicBranchId])
REFERENCES [dbo].[AcademicBranches] ([Id])
GO

ALTER TABLE [dbo].[ExamProcesses] CHECK CONSTRAINT [FK_ExamProcesses_AcademicBranches]
GO

ALTER TABLE [dbo].[ExamProcesses]  WITH CHECK ADD  CONSTRAINT [FK_ExamProcesses_AcademicClasses] FOREIGN KEY([AcademicClassesId])
REFERENCES [dbo].[AcademicClasses] ([Id])
GO

ALTER TABLE [dbo].[ExamProcesses] CHECK CONSTRAINT [FK_ExamProcesses_AcademicClasses]
GO

ALTER TABLE [dbo].[ExamProcesses]  WITH CHECK ADD  CONSTRAINT [FK_ExamProcesses_AcademicSections] FOREIGN KEY([AcademicSectionId])
REFERENCES [dbo].[AcademicSections] ([Id])
GO

ALTER TABLE [dbo].[ExamProcesses] CHECK CONSTRAINT [FK_ExamProcesses_AcademicSections]
GO

ALTER TABLE [dbo].[ExamProcesses]  WITH CHECK ADD  CONSTRAINT [FK_ExamProcesses_AcademicSessions] FOREIGN KEY([AcademicSessionId])
REFERENCES [dbo].[AcademicSessions] ([Id])
GO

ALTER TABLE [dbo].[ExamProcesses] CHECK CONSTRAINT [FK_ExamProcesses_AcademicSessions]
GO

ALTER TABLE [dbo].[ExamProcesses]  WITH CHECK ADD  CONSTRAINT [FK_ExamProcesses_AcademicShifts] FOREIGN KEY([AcademicShiftId])
REFERENCES [dbo].[AcademicShifts] ([Id])
GO

ALTER TABLE [dbo].[ExamProcesses] CHECK CONSTRAINT [FK_ExamProcesses_AcademicShifts]
GO

ALTER TABLE [dbo].[ExamProcesses]  WITH CHECK ADD  CONSTRAINT [FK_ExamProcesses_ExamType] FOREIGN KEY([ExamTypeId])
REFERENCES [dbo].[ExamType] ([Id])
GO

ALTER TABLE [dbo].[ExamProcesses] CHECK CONSTRAINT [FK_ExamProcesses_ExamType]
GO

ALTER TABLE [dbo].[ExamProcesses]  WITH CHECK ADD  CONSTRAINT [FK_ExamProcesses_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO

ALTER TABLE [dbo].[ExamProcesses] CHECK CONSTRAINT [FK_ExamProcesses_Institutes]
GO



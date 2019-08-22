USE [PNSMS]
GO

/****** Object:  Table [dbo].[SubjectStudentMappings]    Script Date: 12/12/2017 12:58:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SubjectStudentMappings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[AcademicSessionId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[SubjectTypeId] [int] NULL,
 CONSTRAINT [PK_SubjectStudentMappings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SubjectStudentMappings]  WITH NOCHECK ADD  CONSTRAINT [FK_SubjectStudentMappings_AcademicSessions] FOREIGN KEY([AcademicSessionId])
REFERENCES [dbo].[AcademicSessions] ([Id])
GO

ALTER TABLE [dbo].[SubjectStudentMappings] CHECK CONSTRAINT [FK_SubjectStudentMappings_AcademicSessions]
GO

ALTER TABLE [dbo].[SubjectStudentMappings]  WITH NOCHECK ADD  CONSTRAINT [FK_SubjectStudentMappings_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO

ALTER TABLE [dbo].[SubjectStudentMappings] CHECK CONSTRAINT [FK_SubjectStudentMappings_Institutes]
GO

ALTER TABLE [dbo].[SubjectStudentMappings]  WITH NOCHECK ADD  CONSTRAINT [FK_SubjectStudentMappings_Students] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentId])
GO

ALTER TABLE [dbo].[SubjectStudentMappings] CHECK CONSTRAINT [FK_SubjectStudentMappings_Students]
GO

ALTER TABLE [dbo].[SubjectStudentMappings]  WITH NOCHECK ADD  CONSTRAINT [FK_SubjectStudentMappings_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])
GO

ALTER TABLE [dbo].[SubjectStudentMappings] CHECK CONSTRAINT [FK_SubjectStudentMappings_Subject]
GO

ALTER TABLE [dbo].[SubjectStudentMappings]  WITH CHECK ADD  CONSTRAINT [FK_SubjectStudentMappings_SubjectTypes] FOREIGN KEY([SubjectTypeId])
REFERENCES [dbo].[SubjectTypes] ([Id])
GO

ALTER TABLE [dbo].[SubjectStudentMappings] CHECK CONSTRAINT [FK_SubjectStudentMappings_SubjectTypes]
GO



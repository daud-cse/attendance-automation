USE [PNSMS]
GO

/****** Object:  Table [dbo].[SubjectAcademicClassMappings]    Script Date: 10/12/2017 1:21:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SubjectAcademicClassMappings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[AcademicClassId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[ParentSubjectId] [int] NULL,
	[OrderBy] [int] NULL,
	[MarksEntryTypeKey] [int] NULL,
	[IsSubjectGroupWise] [bit] NULL,
	[AcademicSessionId] [int] NULL,
	[AcademicGroupId] [int] NULL,
	[ExamDate] [date] NULL,
	[IsFailApplicable] [bit] NOT NULL,
 CONSTRAINT [PK_SubjectAcademicClassMappings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SubjectAcademicClassMappings] ADD  CONSTRAINT [DF_SubjectAcademicClassMappings_IsFailApplicable]  DEFAULT ((1)) FOR [IsFailApplicable]
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappings]  WITH CHECK ADD  CONSTRAINT [FK_SubjectAcademicClassMappings_AcademicClasses] FOREIGN KEY([AcademicClassId])
REFERENCES [dbo].[AcademicClasses] ([Id])
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappings] CHECK CONSTRAINT [FK_SubjectAcademicClassMappings_AcademicClasses]
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappings]  WITH CHECK ADD  CONSTRAINT [FK_SubjectAcademicClassMappings_AcademicGroups] FOREIGN KEY([AcademicGroupId])
REFERENCES [dbo].[AcademicGroups] ([Id])
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappings] CHECK CONSTRAINT [FK_SubjectAcademicClassMappings_AcademicGroups]
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappings]  WITH CHECK ADD  CONSTRAINT [FK_SubjectAcademicClassMappings_AcademicSessions] FOREIGN KEY([AcademicSessionId])
REFERENCES [dbo].[AcademicSessions] ([Id])
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappings] CHECK CONSTRAINT [FK_SubjectAcademicClassMappings_AcademicSessions]
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappings]  WITH CHECK ADD  CONSTRAINT [FK_SubjectAcademicClassMappings_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappings] CHECK CONSTRAINT [FK_SubjectAcademicClassMappings_Institutes]
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappings]  WITH CHECK ADD  CONSTRAINT [FK_SubjectAcademicClassMappings_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappings] CHECK CONSTRAINT [FK_SubjectAcademicClassMappings_Subject]
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappings]  WITH CHECK ADD  CONSTRAINT [FK_SubjectAcademicClassMappings_SubjectParent] FOREIGN KEY([ParentSubjectId])
REFERENCES [dbo].[Subject] ([Id])
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappings] CHECK CONSTRAINT [FK_SubjectAcademicClassMappings_SubjectParent]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'101 > Best one, 102 > Average, 103 > Average of {Value}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SubjectAcademicClassMappings', @level2type=N'COLUMN',@level2name=N'MarksEntryTypeKey'
GO



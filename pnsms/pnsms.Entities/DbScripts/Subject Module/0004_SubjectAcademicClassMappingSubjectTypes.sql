USE [PNSMS]
GO

/****** Object:  Table [dbo].[SubjectAcademicClassMappingSubjectTypes]    Script Date: 10/12/2017 1:22:43 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SubjectAcademicClassMappingSubjectTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubjectAcademicClassMappingId] [int] NOT NULL,
	[InstituteId] [int] NOT NULL,
	[AcademicClassId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[SubjectTypeId] [int] NOT NULL,
 CONSTRAINT [PK_SubjectAcademicClassMappingSubjectTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SubjectAcademicClassMappingSubjectTypes]  WITH NOCHECK ADD  CONSTRAINT [FK_SubjectAcademicClassMappingSubjectTypes_AcademicClasses] FOREIGN KEY([AcademicClassId])
REFERENCES [dbo].[AcademicClasses] ([Id])
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappingSubjectTypes] CHECK CONSTRAINT [FK_SubjectAcademicClassMappingSubjectTypes_AcademicClasses]
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappingSubjectTypes]  WITH NOCHECK ADD  CONSTRAINT [FK_SubjectAcademicClassMappingSubjectTypes_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappingSubjectTypes] CHECK CONSTRAINT [FK_SubjectAcademicClassMappingSubjectTypes_Institutes]
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappingSubjectTypes]  WITH NOCHECK ADD  CONSTRAINT [FK_SubjectAcademicClassMappingSubjectTypes_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappingSubjectTypes] CHECK CONSTRAINT [FK_SubjectAcademicClassMappingSubjectTypes_Subject]
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappingSubjectTypes]  WITH CHECK ADD  CONSTRAINT [FK_SubjectAcademicClassMappingSubjectTypes_SubjectAcademicClassMappings] FOREIGN KEY([SubjectAcademicClassMappingId])
REFERENCES [dbo].[SubjectAcademicClassMappings] ([Id])
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappingSubjectTypes] CHECK CONSTRAINT [FK_SubjectAcademicClassMappingSubjectTypes_SubjectAcademicClassMappings]
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappingSubjectTypes]  WITH NOCHECK ADD  CONSTRAINT [FK_SubjectAcademicClassMappingSubjectTypes_SubjectTypes] FOREIGN KEY([SubjectTypeId])
REFERENCES [dbo].[SubjectTypes] ([Id])
GO

ALTER TABLE [dbo].[SubjectAcademicClassMappingSubjectTypes] CHECK CONSTRAINT [FK_SubjectAcademicClassMappingSubjectTypes_SubjectTypes]
GO



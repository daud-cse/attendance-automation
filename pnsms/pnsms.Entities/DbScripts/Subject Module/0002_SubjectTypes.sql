USE [PNSMS]
GO

/****** Object:  Table [dbo].[SubjectTypes]    Script Date: 10/12/2017 1:20:49 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SubjectTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[IsMandatory] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
	[IsFailApplicable] [bit] NULL,
	[HandicapGradePoint] [decimal](18, 2) NOT NULL,
	[HandicapMarks] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_SubjectTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SubjectTypes] ADD  CONSTRAINT [DF_SubjectTypes_HandicapGradePoint]  DEFAULT ((0)) FOR [HandicapGradePoint]
GO

ALTER TABLE [dbo].[SubjectTypes] ADD  CONSTRAINT [DF_SubjectTypes_HandicapMarks]  DEFAULT ((0)) FOR [HandicapMarks]
GO

ALTER TABLE [dbo].[SubjectTypes]  WITH NOCHECK ADD  CONSTRAINT [FK_SubjectTypes_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO

ALTER TABLE [dbo].[SubjectTypes] CHECK CONSTRAINT [FK_SubjectTypes_Institutes]
GO



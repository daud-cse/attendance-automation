USE [PNSMS]
GO

/****** Object:  Table [dbo].[AcademicPeriod]    Script Date: 06/12/2017 10:32:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AcademicPeriod](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_AcademicPeriod] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AcademicPeriod]  WITH CHECK ADD  CONSTRAINT [FK_AcademicPeriod_Institutes] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institutes] ([Id])
GO

ALTER TABLE [dbo].[AcademicPeriod] CHECK CONSTRAINT [FK_AcademicPeriod_Institutes]
GO



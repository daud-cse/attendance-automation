

/****** Object:  Table [dbo].[ExamWiseTotalMarks]    Script Date: 11/16/2017 17:13:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ExamWiseTotalMarks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstituteId] [int] NOT NULL,
	[ExamId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[TotalMarks] [decimal](18, 2) NOT NULL,
	[AverageMarks] [decimal](18, 2) NOT NULL,
	[AcceptTotalMarks] [decimal](18, 2) NOT NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_ExamWiseTotalMarks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



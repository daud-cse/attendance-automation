USE [PNSMS]
GO

/****** Object:  Table [dbo].[SSO]    Script Date: 20-11-2017 6:10:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SSO](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [varchar](500) NULL,
	[ClientId] [varchar](500) NULL,
	[Tokenkey] [varchar](500) NULL,
	[IssuedUtc] [datetime] NULL,
	[ExpiresUtc] [datetime] NULL,
	[SessionId] [varchar](500) NULL,
	[LogDate] [datetime] NULL,
 CONSTRAINT [PK_Token] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



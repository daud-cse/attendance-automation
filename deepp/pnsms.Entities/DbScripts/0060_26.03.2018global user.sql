

ALTER TABLE [dbo].[GlobalUsers] DROP CONSTRAINT [FK_GlobalUsers_UserInfoes]
GO

ALTER TABLE [dbo].[GlobalUsers] DROP CONSTRAINT [FK_GlobalUsers_MaritalStatuses]
GO

ALTER TABLE [dbo].[GlobalUsers] DROP CONSTRAINT [FK_GlobalUsers_GlobalUserTypes]
GO

ALTER TABLE [dbo].[GlobalUsers] DROP CONSTRAINT [FK_GlobalUsers_GlobalSubDistricts]
GO

ALTER TABLE [dbo].[GlobalUsers] DROP CONSTRAINT [FK_GlobalUsers_GlobalDivisions]
GO

ALTER TABLE [dbo].[GlobalUsers] DROP CONSTRAINT [FK_GlobalUsers_GlobalDistricts]
GO

ALTER TABLE [dbo].[GlobalUsers] DROP CONSTRAINT [FK_GlobalUsers_GlobalCountries]
GO

ALTER TABLE [dbo].[GlobalUsers] DROP CONSTRAINT [FK_GlobalUsers_Designations]
GO

ALTER TABLE [dbo].[GlobalUsers] DROP CONSTRAINT [FK_GlobalUsers_Departments]
GO

/****** Object:  Table [dbo].[GlobalUsers]    Script Date: 26-Mar-18 9:48:35 AM ******/
DROP TABLE [dbo].[GlobalUsers]
GO

/****** Object:  Table [dbo].[GlobalUsers]    Script Date: 26-Mar-18 9:48:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GlobalUsers](
	[GlobalUserId] [int] NOT NULL,
	[GlobalUserTypeId] [int] NOT NULL,
	[FatherName] [nvarchar](128) NULL,
	[MotherName] [nvarchar](128) NULL,
	[MaritalStatusId] [int] NULL,
	[DesignationId] [int] NULL,
	[DepartmentId] [int] NULL,
	[GlobalCountryId] [int] NULL,
	[GlobalDivisionId] [int] NULL,
	[GlobalDistrictId] [int] NULL,
	[GlobalSubDistrictId] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_GlobalUsers] PRIMARY KEY CLUSTERED 
(
	[GlobalUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GlobalUsers]  WITH CHECK ADD  CONSTRAINT [FK_GlobalUsers_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO

ALTER TABLE [dbo].[GlobalUsers] CHECK CONSTRAINT [FK_GlobalUsers_Departments]
GO

ALTER TABLE [dbo].[GlobalUsers]  WITH CHECK ADD  CONSTRAINT [FK_GlobalUsers_Designations] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[Designations] ([Id])
GO

ALTER TABLE [dbo].[GlobalUsers] CHECK CONSTRAINT [FK_GlobalUsers_Designations]
GO

ALTER TABLE [dbo].[GlobalUsers]  WITH CHECK ADD  CONSTRAINT [FK_GlobalUsers_GlobalCountries] FOREIGN KEY([GlobalCountryId])
REFERENCES [dbo].[GlobalCountries] ([Id])
GO

ALTER TABLE [dbo].[GlobalUsers] CHECK CONSTRAINT [FK_GlobalUsers_GlobalCountries]
GO

ALTER TABLE [dbo].[GlobalUsers]  WITH CHECK ADD  CONSTRAINT [FK_GlobalUsers_GlobalDistricts] FOREIGN KEY([GlobalDistrictId])
REFERENCES [dbo].[GlobalDistricts] ([Id])
GO

ALTER TABLE [dbo].[GlobalUsers] CHECK CONSTRAINT [FK_GlobalUsers_GlobalDistricts]
GO

ALTER TABLE [dbo].[GlobalUsers]  WITH CHECK ADD  CONSTRAINT [FK_GlobalUsers_GlobalDivisions] FOREIGN KEY([GlobalDivisionId])
REFERENCES [dbo].[GlobalDivisions] ([Id])
GO

ALTER TABLE [dbo].[GlobalUsers] CHECK CONSTRAINT [FK_GlobalUsers_GlobalDivisions]
GO

ALTER TABLE [dbo].[GlobalUsers]  WITH CHECK ADD  CONSTRAINT [FK_GlobalUsers_GlobalSubDistricts] FOREIGN KEY([GlobalSubDistrictId])
REFERENCES [dbo].[GlobalSubDistricts] ([Id])
GO

ALTER TABLE [dbo].[GlobalUsers] CHECK CONSTRAINT [FK_GlobalUsers_GlobalSubDistricts]
GO

ALTER TABLE [dbo].[GlobalUsers]  WITH CHECK ADD  CONSTRAINT [FK_GlobalUsers_GlobalUserTypes] FOREIGN KEY([GlobalUserTypeId])
REFERENCES [dbo].[GlobalUserTypes] ([Id])
GO

ALTER TABLE [dbo].[GlobalUsers] CHECK CONSTRAINT [FK_GlobalUsers_GlobalUserTypes]
GO

ALTER TABLE [dbo].[GlobalUsers]  WITH CHECK ADD  CONSTRAINT [FK_GlobalUsers_MaritalStatuses] FOREIGN KEY([MaritalStatusId])
REFERENCES [dbo].[MaritalStatuses] ([Id])
GO

ALTER TABLE [dbo].[GlobalUsers] CHECK CONSTRAINT [FK_GlobalUsers_MaritalStatuses]
GO

ALTER TABLE [dbo].[GlobalUsers]  WITH CHECK ADD  CONSTRAINT [FK_GlobalUsers_UserInfoes] FOREIGN KEY([GlobalUserId])
REFERENCES [dbo].[UserInfoes] ([Id])
GO

ALTER TABLE [dbo].[GlobalUsers] CHECK CONSTRAINT [FK_GlobalUsers_UserInfoes]
GO



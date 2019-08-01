
USE PNSMS;


GO
/*
The column [dbo].[FeesGenerateStudents].[TotalAmountDue] on table [dbo].[FeesGenerateStudents] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[FeesGenerateStudents])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_FeesGenerateStudentDetails_FeesGenerateStudents]...';


GO
ALTER TABLE [dbo].[FeesGenerateStudentDetails] DROP CONSTRAINT [FK_FeesGenerateStudentDetails_FeesGenerateStudents];


GO
PRINT N'Dropping [dbo].[FK_FeesGenerateStudents_Students]...';


GO
ALTER TABLE [dbo].[FeesGenerateStudents] DROP CONSTRAINT [FK_FeesGenerateStudents_Students];


GO
PRINT N'Dropping [dbo].[FK_FeesGenerateStudents_FeesGenerates]...';


GO
ALTER TABLE [dbo].[FeesGenerateStudents] DROP CONSTRAINT [FK_FeesGenerateStudents_FeesGenerates];


GO
PRINT N'Creating [dbo].[UdtFeesGenerateStudent]...';


GO
CREATE TYPE [dbo].[UdtFeesGenerateStudent] AS TABLE (
    [StudentId]  INT             NULL,
    [FeesHeadId] INT             NULL,
    [Amount]     DECIMAL (18, 2) NULL);


GO
PRINT N'Altering [dbo].[Events]...';


GO
ALTER TABLE [dbo].[Events]
    ADD [ContactPerson] NVARCHAR (128) NULL,
        [ContactNumber] NVARCHAR (128) NULL,
        [ContactEmail]  NVARCHAR (128) NULL,
        [WebAddress]    NVARCHAR (256) NULL;


GO
PRINT N'Altering [dbo].[FeesGenerates]...';


GO
ALTER TABLE [dbo].[FeesGenerates]
    ADD [IsActive] BIT NULL;


GO
PRINT N'Starting rebuilding table [dbo].[FeesGenerateStudents]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_FeesGenerateStudents] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [FeesGenerateId] INT             NOT NULL,
    [StudentId]      INT             NOT NULL,
    [TotalAmountDue] DECIMAL (18, 2) NOT NULL,
    [AmountPaid]     DECIMAL (18, 2) NOT NULL,
    [AmountDue]      DECIMAL (18, 2) NOT NULL,
    [IsCompleted]    BIT             NULL,
    [IsPublished]    BIT             NULL,
    [HasAnyAdvance]  BIT             NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_FeesGenerateStudents] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[FeesGenerateStudents])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_FeesGenerateStudents] ON;
        INSERT INTO [dbo].[tmp_ms_xx_FeesGenerateStudents] ([Id], [FeesGenerateId], [StudentId], [AmountDue], [AmountPaid], [IsCompleted], [IsPublished])
        SELECT   [Id],
                 [FeesGenerateId],
                 [StudentId],
                 [AmountDue],
                 [AmountPaid],
                 [IsCompleted],
                 [IsPublished]
        FROM     [dbo].[FeesGenerateStudents]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_FeesGenerateStudents] OFF;
    END

DROP TABLE [dbo].[FeesGenerateStudents];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_FeesGenerateStudents]', N'FeesGenerateStudents';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_FeesGenerateStudents]', N'PK_FeesGenerateStudents', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[FeesBooth]...';


GO
CREATE TABLE [dbo].[FeesBooth] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [AcademicBracnchId] INT            NOT NULL,
    [Name]              NVARCHAR (256) NOT NULL,
    [Description]       NVARCHAR (512) NULL,
    [IsActive]          BIT            NOT NULL,
    [LastUpdateTime]    DATETIME       NULL,
    CONSTRAINT [PK_FeesBooth] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FeesCollectionDetails]...';


GO
CREATE TABLE [dbo].[FeesCollectionDetails] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [FeesCollectionId] INT             NOT NULL,
    [FeesGenerateId]   INT             NOT NULL,
    [Amount]           DECIMAL (18, 2) NOT NULL,
    [IsCompleted]      BIT             NULL,
    CONSTRAINT [PK_FeesCollectionDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FeesCollections]...';


GO
CREATE TABLE [dbo].[FeesCollections] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [FeesBoothId]          INT             NULL,
    [FeesCollectionTypeId] INT             NULL,
    [StudentId]            INT             NOT NULL,
    [CollectionDate]       DATE            NOT NULL,
    [TotalReceiveAmount]   DECIMAL (18, 2) NOT NULL,
    [IsSelfPaid]           BIT             NOT NULL,
    [IsActive]             BIT             NOT NULL,
    [LastUpdateTime]       DATETIME        NULL,
    CONSTRAINT [PK_FeesCollections] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FeesCollectionTypes]...';


GO
CREATE TABLE [dbo].[FeesCollectionTypes] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT            NOT NULL,
    [Name]           NVARCHAR (256) NOT NULL,
    [Description]    NVARCHAR (512) NULL,
    [IsActive]       BIT            NOT NULL,
    [LastUpdateTime] DATETIME       NULL,
    [IsShowRefNo]    BIT            NULL,
    [RefNoTitle]     NVARCHAR (512) NULL,
    [IsShowMobileNo] BIT            NULL,
    [MobileNoTitle]  NVARCHAR (512) NULL,
    CONSTRAINT [PK_FeesCollectionTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateStudentDetails_FeesGenerateStudents]...';


GO
ALTER TABLE [dbo].[FeesGenerateStudentDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateStudentDetails_FeesGenerateStudents] FOREIGN KEY ([FeesGenerateStudentId]) REFERENCES [dbo].[FeesGenerateStudents] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateStudents_Students]...';


GO
ALTER TABLE [dbo].[FeesGenerateStudents] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateStudents_Students] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([StudentId]);


GO
PRINT N'Creating [dbo].[FK_FeesGenerateStudents_FeesGenerates]...';


GO
ALTER TABLE [dbo].[FeesGenerateStudents] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesGenerateStudents_FeesGenerates] FOREIGN KEY ([FeesGenerateId]) REFERENCES [dbo].[FeesGenerates] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesBooth_AcademicBranches]...';


GO
ALTER TABLE [dbo].[FeesBooth] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesBooth_AcademicBranches] FOREIGN KEY ([AcademicBracnchId]) REFERENCES [dbo].[AcademicBranches] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesCollectionDetails_FeesCollections]...';


GO
ALTER TABLE [dbo].[FeesCollectionDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesCollectionDetails_FeesCollections] FOREIGN KEY ([FeesCollectionId]) REFERENCES [dbo].[FeesCollections] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesCollectionDetails_FeesGenerates]...';


GO
ALTER TABLE [dbo].[FeesCollectionDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesCollectionDetails_FeesGenerates] FOREIGN KEY ([FeesGenerateId]) REFERENCES [dbo].[FeesGenerates] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesCollections_Students]...';


GO
ALTER TABLE [dbo].[FeesCollections] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesCollections_Students] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([StudentId]);


GO
PRINT N'Creating [dbo].[FK_FeesCollections_FeesCollectionTypes]...';


GO
ALTER TABLE [dbo].[FeesCollections] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesCollections_FeesCollectionTypes] FOREIGN KEY ([FeesCollectionTypeId]) REFERENCES [dbo].[FeesCollectionTypes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_FeesCollections_FeesBooth]...';


GO
ALTER TABLE [dbo].[FeesCollections] WITH NOCHECK
    ADD CONSTRAINT [FK_FeesCollections_FeesBooth] FOREIGN KEY ([FeesBoothId]) REFERENCES [dbo].[FeesBooth] ([Id]);


GO
PRINT N'Altering [dbo].[SprInstituteDefaultSetup]...';


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SprInstituteDefaultSetup] 
	-- Add the parameters for the stored procedure here
	@InstituteId int,
	@IsSuccess bit output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @userid int,@roleid int,@packageId int
	
	insert into dbo.AcademicBranches ([InstituteId],[Name],[IsActive]) values(@InstituteId,'Default',1)
    
	insert into dbo.AcademicClasses(InstituteId,Name,IsActive)values(@InstituteId,'One',1),(@InstituteId,'Two',1),(@InstituteId,'Three',1),(@InstituteId,'Four',1),(@InstituteId,'Five',1)
	insert into dbo.AcademicSections (InstituteId,Name,IsActive)values(@InstituteId,'A',1),(@InstituteId,'B',1)
	insert into dbo.AcademicSessions(InstituteId,Name,StartAt,IsCompleted,IsRunning) values(@InstituteId,convert(nvarchar(4),year(getdate())),getdate(),0,1)
	insert into dbo.AcademicShifts (InstituteId,Name,IsActive)values(@InstituteId,'Day',1)
	insert into dbo.AcademicVersions (InstituteId,Name,IsActive)values(@InstituteId,'Bangla',1)
	insert into dbo.AddressTypes (InstituteId,Name,IsActive)values(@InstituteId,'Present',1),(@InstituteId,'Permanent',1)
	
	insert into dbo.AttendanceTypes (InstituteId,Name,Flag
									,IsUsedForStudent,IsUsedForEmployee,IsUsedForTeacher
									,ColourId,IsDefault,IsPresent,IsActive)
				values(@InstituteId,'Present','P',1,1,1,2,1,1,1)
						,(@InstituteId,'Absent','A',1,1,1,1,0,0,1)
	
	insert into dbo.BloodGroups(InstituteId,Name,IsActive,GlobalBloodGroupId)
				select @InstituteId,Name,IsActive,Id from dbo.GlobalBloodGroups
				
	insert into dbo.CertificatePrintTypes(InstituteId,Name,IsActive)values(@InstituteId,'Testimonial',1)
	
	insert into dbo.CoCurricularActivities(InstituteId,Name,IsActive,GlobalCoCurricularActivitiyId)
				select @InstituteId,Name,IsActive,Id from dbo.GlobalCoCurricularActivities
	
	insert into dbo.Countries(InstituteId,Name,IsActive,GlobalCountryId)
				select @InstituteId,Name,IsActive,Id from dbo.GlobalCountries
	
	insert into dbo.Departments (InstituteId,Name,IsActive)values(@InstituteId,'Accounts',1)
	insert into dbo.Designations (InstituteId,Name,IsActive,Ordering,StaffRequired)
				values(@InstituteId,'Head Master',1,1,1),(@InstituteId,'Assistant Head Master',1,2,2)
				,(@InstituteId,'Teacher',1,3,4),(@InstituteId,'Accounts Officer',1,4,1)
	
	insert into dbo.DistrictOrStates(CountryId,Name,IsActive,GlobalDistrictId)
				SELECT	dbo.Countries.Id, dbo.GlobalDistricts.Name, dbo.GlobalDistricts.IsActive, dbo.GlobalDistricts.Id
				FROM	dbo.GlobalCountries LEFT OUTER JOIN
				dbo.Countries ON dbo.GlobalCountries.Id = dbo.Countries.GlobalCountryId RIGHT OUTER JOIN
				dbo.GlobalDivisions ON dbo.GlobalCountries.Id = dbo.GlobalDivisions.GlobalCountryId RIGHT OUTER JOIN
				dbo.GlobalDistricts ON dbo.GlobalDivisions.Id = dbo.GlobalDistricts.GlobalDivisionId
				where  dbo.Countries.InstituteId=@InstituteId
				
	insert into dbo.EducationalQualifications(InstituteId,Name,IsActive,GlobalEducationalQualificationId)
				select @InstituteId,Name,IsActive,Id from dbo.GlobalEducationalQualifications
	
	insert into dbo.Genders(InstituteId,Name,IsActive,GlobalGenderId)
				select @InstituteId,Name,IsActive,Id from dbo.GlobalGenders
	
	insert into dbo.GuardianTypes(InstituteId,Name,IsActive)values(@InstituteId,'Father',1),(@InstituteId,'Mother',1)
	
	insert into dbo.MaritalStatuses(InstituteId,Name,IsActive)values(@InstituteId,'Un Married',1),(@InstituteId,'Married',1)	
	insert into dbo.Nationalities(InstituteId,Name,IsActive)values(@InstituteId,'Bangladeshi',1)
	
	insert into dbo.Professions(InstituteId,Name,IsActive,GlobalProfessionId)
				select @InstituteId,Name,IsActive,Id from dbo.GlobalProfessions
	--temp
	insert into dbo.Religions(InstituteId,Name,IsActive)values(@InstituteId,'Muslim',1),(@InstituteId,'Shonaton',1),(@InstituteId,'Buddha',1),(@InstituteId,'Chrishtan',1)
	
	insert into dbo.UserInfoes(UserInfoTypeId,InstituteId,FirstName,Name,IsActive)values(14,@InstituteId,'Admin','Admin',1)
	set @userid=SCOPE_IDENTITY()
	insert into dbo.Employees(EmployeeId)values(@userid)
	
	select @packageId=PackageId from dbo.Institutes where Id=@InstituteId

	--no access role
	insert into dbo.Roles(InstituteId,Name,IsForEmployee,IsForTeacher,IsActive)values(@InstituteId,'Not Access',1,1,1)

	--admin role
	insert into dbo.Roles(InstituteId,Name,IsForEmployee,IsForTeacher,IsActive)values(@InstituteId,'Admin',1,1,1)
	set @roleid=SCOPE_IDENTITY()
	--admin role rights
	insert into [dbo].[RightsOfRoles]([RoleId],[RightId])
	SELECT @roleid,dbo.RightsOfPackages.RightId
	FROM  dbo.Rights RIGHT OUTER JOIN
	dbo.RightsOfPackages ON dbo.Rights.Id = dbo.RightsOfPackages.RightId
	where dbo.RightsOfPackages.PackageId=@packageId
	
	insert into dbo.RolesOfUserInfoes(UserInfoId,RoleId) values(@userid,@roleid)
	
	--employee role
	insert into dbo.Roles(InstituteId,Name,IsForEmployee,IsActive)values(@InstituteId,'Employee',1,1)
	set @roleid=SCOPE_IDENTITY()
	--employee role rights
	insert into [dbo].[RightsOfRoles]([RoleId],[RightId])
	SELECT @roleid,dbo.RightsOfPackages.RightId
	FROM  dbo.Rights RIGHT OUTER JOIN
	dbo.RightsOfPackages ON dbo.Rights.Id = dbo.RightsOfPackages.RightId
	where dbo.RightsOfPackages.PackageId=@packageId and dbo.Rights.EnableEmployee=1 
	and 
	(
	dbo.RightsOfPackages.RightId not like '10%'
	and dbo.RightsOfPackages.RightId not like '18%'
	)

	--teacher role
	insert into dbo.Roles(InstituteId,Name,IsForTeacher,IsActive)values(@InstituteId,'Teacher',1,1)
	set @roleid=SCOPE_IDENTITY()
	--teacher role rights
	insert into [dbo].[RightsOfRoles]([RoleId],[RightId])
	SELECT @roleid,dbo.RightsOfPackages.RightId
	FROM  dbo.Rights RIGHT OUTER JOIN
	dbo.RightsOfPackages ON dbo.Rights.Id = dbo.RightsOfPackages.RightId
	where dbo.RightsOfPackages.PackageId=@packageId and dbo.Rights.EnableTeacher=1 
	and 
	(
	dbo.RightsOfPackages.RightId not like '10%'
	and dbo.RightsOfPackages.RightId not like '18%'
	and dbo.RightsOfPackages.RightId not like '19%'
	and dbo.RightsOfPackages.RightId not like '20%'
	and dbo.RightsOfPackages.RightId not like '21%'
	)
	
	
	insert into dbo.Scholarships(InstituteId,Name,IsActive,GlobalScholarshipId)
				select @InstituteId,Name,IsActive,Id from dbo.GlobalScholarships
	
	insert into dbo.WeekDays(InstituteId,[DayOfWeek],Name)values(@InstituteId,1,'Sun'),(@InstituteId,2,'Mon'),(@InstituteId,3,'Tue'),(@InstituteId,4,'Wed'),(@InstituteId,5,'Thu'),(@InstituteId,6,'Fri'),(@InstituteId,7,'Sat')
	
	
	
	insert into dbo.AcademicBranchesOfUserInfoes(UserInfoId,AcademicBranchId)
			select @userid,Id from dbo.AcademicBranches where InstituteId=@InstituteId
	
	
	
	insert into dbo.UserInfoSecurities(UserInfoId,InstituteId,UserName,PasswordHash,IsActive,IsLockout)
				values (@userid,@InstituteId,'admin','4QrcOUm6Wau+VuBX8g+IPg==',1,0)
	
	set @IsSuccess=1
END
GO
PRINT N'Creating [dbo].[SprFeesCollection]...';


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SprFeesCollection]
	-- Add the parameters for the stored procedure here
	 @FeesBoothId int = null
	,@FeesCollectionTypeId int = null
	,@StudentId int
	,@CollectionDate date
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
END
GO
PRINT N'Creating [dbo].[SprFeesGenerate]...';


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SprFeesGenerate]
	-- Add the parameters for the stored procedure here
	 @FeesGenerateId int
	,@FeesGenerateStudentAmmenment dbo.UdtFeesGenerateStudent readonly 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
END
GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[FeesGenerateStudentDetails] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateStudentDetails_FeesGenerateStudents];

ALTER TABLE [dbo].[FeesGenerateStudents] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateStudents_Students];

ALTER TABLE [dbo].[FeesGenerateStudents] WITH CHECK CHECK CONSTRAINT [FK_FeesGenerateStudents_FeesGenerates];

ALTER TABLE [dbo].[FeesBooth] WITH CHECK CHECK CONSTRAINT [FK_FeesBooth_AcademicBranches];

ALTER TABLE [dbo].[FeesCollectionDetails] WITH CHECK CHECK CONSTRAINT [FK_FeesCollectionDetails_FeesCollections];

ALTER TABLE [dbo].[FeesCollectionDetails] WITH CHECK CHECK CONSTRAINT [FK_FeesCollectionDetails_FeesGenerates];

ALTER TABLE [dbo].[FeesCollections] WITH CHECK CHECK CONSTRAINT [FK_FeesCollections_Students];

ALTER TABLE [dbo].[FeesCollections] WITH CHECK CHECK CONSTRAINT [FK_FeesCollections_FeesCollectionTypes];

ALTER TABLE [dbo].[FeesCollections] WITH CHECK CHECK CONSTRAINT [FK_FeesCollections_FeesBooth];


GO
update dbo._Migration set LastUpdate='0042'
PRINT N'Update complete.';


GO

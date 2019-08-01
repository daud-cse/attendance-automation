
USE PNSMS;


GO
PRINT N'Altering [dbo].[Institutes]...';


GO
ALTER TABLE [dbo].[Institutes]
    ADD [Asset]       NVARCHAR (MAX) NULL,
        [IncExp]      NVARCHAR (MAX) NULL,
        [Sanitation]  NVARCHAR (MAX) NULL,
        [Multimedia]  NVARCHAR (MAX) NULL,
        [LibraryInfo] NVARCHAR (MAX) NULL,
        [Email]       NVARCHAR (128) NULL,
        [Contact]     NVARCHAR (128) NULL;


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
	insert into dbo.UserInfoes(UserInfoTypeId,InstituteId,FirstName,Name,IsActive)values(14,@InstituteId,'Admin','Admin',1)
	set @userid=SCOPE_IDENTITY()
	insert into dbo.Employees(EmployeeId)values(@userid)
	
	insert into dbo.AcademicBranchesOfUserInfoes(UserInfoId,AcademicBranchId)
			select @userid,Id from dbo.AcademicBranches where InstituteId=@InstituteId
	
	insert into dbo.RolesOfUserInfoes(UserInfoId,RoleId) values(@userid,@roleid)
	
	insert into dbo.UserInfoSecurities(UserInfoId,InstituteId,UserName,PasswordHash,IsActive,IsLockout)
				values (@userid,@InstituteId,'admin','4QrcOUm6Wau+VuBX8g+IPg==',1,0)
	
	set @IsSuccess=1
END
GO
update dbo._Migration set LastUpdate='0039'
PRINT N'Update complete.';


GO

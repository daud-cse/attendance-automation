Alter procedure spInstituteDashBoard
                 @InstituteId int
				,@CurrentSessionId int
	as 
	declare @TotalStudentsCount int
	       ,@TotalTeachersCount int
		   ,@TotalEmployeesCount int
		   ,@TotalTeachersRequired int
		   ,@TotalAvailableSMS int

		 set @TotalStudentsCount=1
	     set @TotalTeachersCount =0
		 set @TotalEmployeesCount=1
		 set @TotalTeachersRequired=1
		 set  @TotalAvailableSMS =0

   --Total Counting Part
   select @TotalStudentsCount=count(StudentId)  from Students AS S
	 INNER JOIN UserInfoes as uinfo on uinfo.Id=s.StudentId where uinfo.InstituteId=@InstituteId

   select @TotalTeachersCount=count(TeacherId) from Teachers AS T
	 INNER JOIN UserInfoes as uinfo on uinfo.Id=T.TeacherId where uinfo.InstituteId=@InstituteId

   select @TotalEmployeesCount=count(EmployeeId)  from Employees AS E
	 INNER JOIN UserInfoes as uinfo on uinfo.Id=E.EmployeeId where uinfo.InstituteId=@InstituteId

   select @TotalStudentsCount as TotalStudentsCount
         ,@TotalTeachersCount as TotalTeachersCount
		 ,@TotalEmployeesCount as TotalEmployeesCount
		 ,@TotalTeachersRequired as TotalTeachersRequired
		 ,@TotalAvailableSMS as TotalAvailableSMS

   --Class Wise Student
      select St.CurrentAcademicClassId as AcademicClassId,AcC.Name as ClassName,Count(st.StudentId) as CountClassWise from 
                Students as St 
				inner join UserInfoes as UInfo on UInfo.Id=St.StudentId and UInfo.InstituteId=@InstituteId 
                inner join AcademicClasses as AcC on AcC.Id=St.CurrentAcademicClassId
			    where UInfo.InstituteId=@InstituteId and AcC.InstituteId=@InstituteId and UserInfoTypeId=11
				 group by St.CurrentAcademicClassId,AcC.Name


   --Attendance part
   select  DATENAME(month,StAtt.AttendanceDate) as AttendanceMonthName
           ,month(StAtt.AttendanceDate) as AttendanceMonth
		   ,StAtt.AcademicClassId,AcC.Name as AcademicClassName
		   ,0 as PercentageOfAttendance 		   
		  from StudentAttendances as StAtt 
                inner join Students as St on St.StudentId=st.StudentId 
				inner join UserInfoes as UInfo on UInfo.Id=St.StudentId and UInfo.InstituteId=@InstituteId 
                inner join AcademicClasses as AcC on AcC.Id=StAtt.AcademicClassId
			    where UInfo.InstituteId=@InstituteId and AcC.InstituteId=@InstituteId 
				 group by DATENAME(month,StAtt.AttendanceDate),month(StAtt.AttendanceDate),StAtt.AcademicClassId,AcC.Name

  --Male Female Ratio
    select UInfo.GenderId as GenderId,Gend.Name as GenderName,Count(st.StudentId) as CountGenderWise from 
                Students as St 
				inner join UserInfoes as UInfo on UInfo.Id=St.StudentId and UInfo.InstituteId=@InstituteId 
                inner join Genders as Gend on Gend.Id=UInfo.GenderId
			    where UInfo.InstituteId=@InstituteId and Gend.InstituteId=@InstituteId  and UserInfoTypeId=11
				 group by UInfo.GenderId,Gend.Name

   --select * from StudentAttendances

--exec spInstituteDashBoard 5,12
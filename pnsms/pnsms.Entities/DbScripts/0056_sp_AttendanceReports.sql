USE [PNSMS]
GO
/****** Object:  StoredProcedure [dbo].[AttendanceReports]    Script Date: 17-12-2017 1:13:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[AttendanceReports]
             @InstituteId int

as 

select SAtt.AttendanceTypeId,At.Name as AttendanceTypeName,st.CurrentAcademicClassId,AC.Name as ClassName
             ,COUNT(SAtt.AttendanceTypeId) as TotalStatus
	 from StudentAttendanceDetails as SAtt
    inner join AttendanceTypes as at on at.Id=SAtt.AttendanceTypeId
	inner join Students as st on st.StudentId=SAtt.StudentId
	inner join AcademicClasses as Ac on Ac.Id =st.CurrentAcademicClassId
	where at.InstituteId=@InstituteId

	group by SAtt.AttendanceTypeId,At.Name,st.CurrentAcademicClassId,AC.Name



      
	  --exec AttendanceReports 5
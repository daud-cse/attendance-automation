USE [PNSMS]
GO
/****** Object:  StoredProcedure [dbo].[ExamTypeWiseProcess]    Script Date: 05-12-2017 2:36:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[ExamTypeWiseProcess] 
                  @InstituteId int
				 ,@SessionId int
				 ,@ClassId int
                 ,@ExamTypeId int
                 ,@Msg varchar(2000) output 
	             ,@testing bit=0   
 
 as 
 BEGIN
  declare @TName varchar(100)
    set @TName='ExamTypeWiseProcess'
	begin tran @TName

	 begin try	     
		 if not exists (select * from ExamType where InstituteId=@InstituteId) begin
			set @Msg='No Exam Type Found'			
			raiserror(@Msg,15,1)		
			end	
		 if not exists (select * from Exam where InstituteId=@InstituteId and ExamTypeId=@ExamTypeId) begin
			set @Msg='No Exam Found'
			raiserror(@Msg,15,1)		
			end	
          if not exists (select * from ExamSubjectMarks where InstituteId=@InstituteId) begin
			set @Msg='Exam Subject Marks not Found'
			raiserror(@Msg,15,1)		
			end	
         if not exists (select * from [dbo].[ExamGrades] where InstituteId=@InstituteId) begin
			set @Msg='Exam ExamGrades not Found'
			raiserror(@Msg,15,1)		
			end	

			 --update ExamSubjectMarks table batch update
			 update [dbo].[ExamSubjectMarks]  set  AcceptMarksTotal =(isnull(Ex.AcceptMarks,0)*MarksObtained)
			     from [dbo].[ExamSubjectMarks] as Exs 
				 inner join Exam as Ex on Ex.Id=Exs.ExamId
				 inner join ExamType as Et on Et.Id=Ex.ExamTypeId
				     where Ex.ExamTypeId=@ExamTypeId and Ex.InstituteId=@InstituteId    

            --Empty Table
			declare @ExamTypeWiseTabulationSheetMasterId int
			--select @ExamTypeWiseTabulationSheetMasterId=id from ExamTypeWiseTabulationSheetMaster where  InstituteId=@InstituteId  and [ExamTypeId]  =@ExamTypeId 
			delete  from ExamTypeWiseTabulationSheetMaster where InstituteId=@InstituteId  and AcademicSessionId=@SessionId and AcademicClassesId=@ClassId and  [ExamTypeId]  =@ExamTypeId
			delete  from ExamTypeWiseTabulationSheetDetails where InstituteId=@InstituteId and AcademicSessionId=@SessionId and AcademicClassesId=@ClassId and  [ExamTypeId]  =@ExamTypeId


			---ExamTypeWiseTabulationSheetMaster data insert
			insert into ExamTypeWiseTabulationSheetMaster 
			      select @InstituteId
						,@ExamTypeId 
						,st.CurrentAcademicClassId
						,ac.Name
						,st.CurrentAcademicGroupId
						,ag.Name 
					    ,st.CurrentAcademicSectionId
						,acsection.Name
						,st.CurrentAcademicSessionId
						,acsess.Name  
						,Exs.StudentId
						,ui.Name
						,sum([AcceptMarksTotal])
						,count(InstituteSubjectClassId)
						,sum([AcceptMarksTotal])/count(InstituteSubjectClassId)
						,NULL
						,NULL
						,NULL
						,getdate()
			from dbo.ExamSubjectMarks  as Exs
			inner join [dbo].[Exam] as Ex on Ex.Id=Exs.ExamId 
			inner join Students as st on st.StudentId=Exs.StudentId
			inner join UserInfoes as ui on ui.Id=st.StudentId
			inner join AcademicClasses as ac on ac.Id=st.CurrentAcademicClassId
			inner join AcademicGroups as ag on ag.Id=st.CurrentAcademicGroupId
			inner join AcademicSections as acsection on acsection.Id=st.CurrentAcademicSectionId
			inner join AcademicSessions as acsess on acsess.Id=st.CurrentAcademicSessionId
                 where Exs.InstituteId=@InstituteId   and acsess.Id=@SessionId and ac.Id=@ClassId and  Ex.[ExamTypeId]  =@ExamTypeId    
			group by   st.CurrentAcademicClassId,ac.Name,st.CurrentAcademicGroupId
						,ag.Name 
					    ,st.CurrentAcademicSectionId
						,acsection.Name
						,st.CurrentAcademicSessionId
						,acsess.Name 
						,Exs.StudentId
						,Ui.Name
						

		--	select @ExamTypeWiseTabulationSheetMasterId=id from ExamTypeWiseTabulationSheetMaster where  InstituteId=@InstituteId  and [ExamTypeId]  =@ExamTypeId 

			--ExamTypeWiseTabulationSheetDetails table insert
			insert into dbo.ExamTypeWiseTabulationSheetDetails           	
			            select 						
						 @ExamTypeId
						,@InstituteId
						,st.CurrentAcademicClassId
						,ac.Name
						,st.CurrentAcademicGroupId
						,ag.Name 
					    ,st.CurrentAcademicSectionId
						,acsection.Name
						,st.CurrentAcademicSessionId
						,acsess.Name  
						,Exs.StudentId
						,InstituteSubjectClassId
						,InsSub.Name
						,sum(MarksObtained)
						,sum(MarksObtained)/count(InstituteSubjectClassId)
						,sum([AcceptMarksTotal])
						,NULL
						,NULL
						,NULL
			from dbo.ExamSubjectMarks  as Exs
			inner join [dbo].[Exam] as Ex on Ex.Id=Exs.ExamId 
			inner join Students as st on st.StudentId=Exs.StudentId
			inner join AcademicClasses as ac on ac.Id=st.CurrentAcademicClassId
			inner join AcademicGroups as ag on ag.Id=st.CurrentAcademicGroupId
			inner join AcademicSections as acsection on acsection.Id=st.CurrentAcademicSectionId
			inner join AcademicSessions as acsess on acsess.Id=st.CurrentAcademicSessionId
			inner join InstituteSubjectClass as InsubClass on InsubClass.Id=Exs.InstituteSubjectClassId
			inner join InstituteSubject as InsSub on InsSub.Id=InsubClass.InstituteSubjectId
                 where Exs.InstituteId=@InstituteId   and acsess.Id=@SessionId and ac.Id=@ClassId and  Ex.[ExamTypeId]  =@ExamTypeId      
			group by  st.CurrentAcademicClassId,ac.Name,st.CurrentAcademicGroupId
						,ag.Name 
					    ,st.CurrentAcademicSectionId
						,acsection.Name
						,st.CurrentAcademicSessionId
						,acsess.Name 
						,Exs.StudentId
						,InstituteSubjectClassId
						,InsSub.Name

		 --cursor define for every grading configure
		 declare @InstituteIdCur int,@ExamGradeId int,@MarksFrom decimal(18,2),@MarksUpto  decimal(18,2),@GradePoint decimal(18,2),@GradeName varchar(500)
			declare crs cursor for 
			select  InstituteId,Id,MarksFrom,MarksUpto,GradePoint,GradeName  from ExamGrades where  InstituteId=@InstituteId
	
			open crs
			fetch from crs into @InstituteIdCur ,@ExamGradeId ,@MarksFrom ,@MarksUpto ,@GradePoint,@GradeName
			WHILE @@FETCH_STATUS=0 BEGIN 

					--select  @InstituteIdCur ,@ExamGradeId ,@MarksFrom ,@MarksUpto ,@GradePoint,@GradeName
					update dbo.ExamTypeWiseTabulationSheetMaster  
									 set ExamGradeId=case when   AverageNumber between @MarksFrom and @MarksUpto
											 then @ExamGradeId
											 end
										,ExamGradeName=case when  AverageNumber between  @MarksFrom and @MarksUpto
								           then @GradeName
										   end
								       ,ExamGradePoint=case when AverageNumber between  @MarksFrom and @MarksUpto
								           then @GradePoint
										   end				 					
						 where InstituteId=@InstituteId   and AcademicSessionId=@SessionId and AcademicClassesId=@ClassId and  [ExamTypeId]  =@ExamTypeId and ExamGradeId is null 
						          and  ExamGradeName is null and ExamGradePoint is null
				 
				     				
					---Update ExamTypeWiseTabulationSheetDetails Grade ,point,GradeName
					update dbo.ExamTypeWiseTabulationSheetDetails  
									    set ExamGradeId=case when   AcceptTotalMarks between @MarksFrom and @MarksUpto
											 then @ExamGradeId
											 end
										,ExamGradeName=case when  AcceptTotalMarks between  @MarksFrom and @MarksUpto
								           then @GradeName
										   end
								       ,ExamGradePoint=case when AcceptTotalMarks between  @MarksFrom and @MarksUpto
								           then @GradePoint
										   end				 			
						 where InstituteId=@InstituteId   and AcademicSessionId=@SessionId and AcademicClassesId=@ClassId and  [ExamTypeId]  =@ExamTypeId and ExamGradeId is null 
						          and  ExamGradeName is null and ExamGradePoint is null     

			fetch next from crs into @InstituteIdCur ,@ExamGradeId ,@MarksFrom ,@MarksUpto ,@GradePoint,@GradeName
			 END
			close crs
			deallocate crs 


		   set @Msg='Exam Process Successfully Executed'
		   print @Msg
		 commit tran @TName 
    end try
    begin catch
    	set @Msg=error_message()
    	print @Msg
		--deallocate crs
    rollback transaction @TName		
    end catch
    
 END   
 
 
/* 
    declare @Msg varchar(2000)
	exec [dbo].[ExamTypeWiseProcess] 5,2,10,8,@Msg output,@testing=0  

	select * from ExamTypeWiseTabulationSheetMaster   where InstituteId=5  

	select * from ExamTypeWiseTabulationSheetDetails   where InstituteId=5  


	select  [ExamId],[StudentId],[InstituteSubjectClassId]
	,[MarksObtained],AcceptMarksTotal  
	from [dbo].[ExamSubjectMarks]  where InstituteId=5  
	     
	update [dbo].[ExamSubjectMarks] set AcceptMarksTotal=0  
	 where InstituteId=5

	
	   
	delete  from ExamTypeWiseTabulationSheetDetails   
	 delete  from ExamTypeWiseTabulationSheetMaster   
	
	
*/
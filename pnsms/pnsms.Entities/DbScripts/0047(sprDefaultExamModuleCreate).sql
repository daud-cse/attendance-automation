alter Procedure sprDefaultExamModuleCreate

as 
begin 

	begin tran
	declare @InstituteId int,@BranchId int,@ClassId  int
			,@SectionId int,@ExamTypeId int
	  
	  set @InstituteId=5
	  
	  select top(1) @BranchId=Id from dbo.AcademicBranches where InstituteId=@InstituteId
	  select top(1) @ClassId=Id from dbo.AcademicClasses where InstituteId=@InstituteId
	  select top(1) @SectionId=Id from dbo.AcademicSections where InstituteId=@InstituteId
    select @ClassId
	--Subject table
	insert into Subject values(@InstituteId,'Math',NULL,'Test',1,NULL)
	insert into Subject values(@InstituteId,'English',NULL,'Test',1,NULL)
	insert into Subject values(@InstituteId,'Bangla',NULL,'Test',1,NULL)
	insert into Subject values(@InstituteId,'General Science',NULL,'Test',1,NULL)

	--instituteSubject Table
	  insert into instituteSubject 
				select InstituteId,Id,Name,'test',1 from  Subject where InstituteId=@InstituteId
	            
	 --InstituteSubjectClass Table
	  insert into InstituteSubjectClass 
				select Id,@ClassId,1 from  instituteSubject where InstituteId=@InstituteId
	            
	 
	 
	--Exam Type Table           
	insert into   ExamType values(@InstituteId,'1st Term','test',1)
	insert into   ExamType values(@InstituteId,'2nd Term','test',1)
	insert into   ExamType values(@InstituteId,'3rd Term','test',1)


	select top(1) @ExamTypeId=Id from dbo.ExamType where InstituteId=@InstituteId
	--Exam table

	  insert into Exam values(@InstituteId,@BranchId,@ClassId,@SectionId,@ExamTypeId,1,'1st Term Final',getdate(),getdate(),'10AM',100,40,'.80',100,getdate(),1)
	  insert into Exam values(@InstituteId,@BranchId,@ClassId,@SectionId,@ExamTypeId,1,'1st Term Class Test',getdate(),getdate(),'10AM',20,10,'.10',100,getdate(),1)
	  insert into Exam values(@InstituteId,@BranchId,@ClassId,@SectionId,@ExamTypeId,1,'1st term Quiz Test',getdate(),getdate(),'10AM',10,5,'.10',100,getdate(),1)
	  
	select * from instituteSubject where InstituteId=@InstituteId
	select * from ExamType where InstituteId=@InstituteId
	select * from Exam where InstituteId=@InstituteId
	
	
	   declare @ExamId int,@InstituteIdCur int,@InstituteSubjectClassId int
		declare crs cursor for 
		select  InstituteId,Id  from Exam where  InstituteId=@InstituteId and ExamTypeId=@ExamTypeId
	
        open crs
		fetch from crs into  @InstituteIdCur,@ExamId 
		WHILE @@FETCH_STATUS=0 BEGIN
              select * from dbo.ExamSubjectMarks
             select @InstituteSubjectClassId=id from InstituteSubjectClass where  ClassId=@ClassId
            -- set @InstituteSubjectClassId=97
             select @InstituteSubjectClassId
           		--select @InstituteId,@ExamId
           		insert into ExamSubjectMarks 
           		select @InstituteIdCur,@ExamId,@InstituteSubjectClassId,s.StudentId,50,'good',getdate() 
           		    from  dbo.Students as s
           		 inner join dbo.AcademicClasses ac on ac.id=s.CurrentAcademicClassId 
           		 inner join dbo.AcademicSections as asection on asection.Id=s.CurrentAcademicSectionId
           		-- inner join instituteSubject as insub on insub.InstituteId=@InstituteIdCur
           	where asection.InstituteId=@InstituteIdCur	--and ac.Id=@ClassId and asection.Id=	@SectionId	
			fetch next from crs into @InstituteIdCur,@ExamId 
		 END
		close crs
		deallocate crs   
      
      select * from ExamSubjectMarks
end    
--commit
rollback


--exec sprDefaultExamModuleCreate

--select * from Students as s 
--    inner join dbo.AcademicClasses ac on ac.id=s.CurrentAcademicClassId
--    where InstituteId=5 and ac.id=10
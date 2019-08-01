begin tran

  ALTER TABLE dbo.Exam
    ADD  PassMarks decimal(18,2)
    
  ALTER TABLE dbo.Exam
    ADD  AcceptMarks decimal(18,2)
    
       
  ALTER TABLE dbo.ExamSubjectMarks
    ADD  Comment varchar(500)
    
    
    ALTER TABLE dbo.ExamSubjectMarks
    ADD  MarksObtained decimal(18,2)
    
  
  rollback
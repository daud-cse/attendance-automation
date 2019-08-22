begin tran
alter table  [Teachers]
   add  AboutTeacher nvarchar(max)

alter table  [dbo].[UserInfoes]
   add  AboutUser nvarchar(max)

   rollback

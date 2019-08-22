
USE PNSMS;


GO
PRINT N'Altering [dbo].[ShortMessageDetails]...';


GO
ALTER TABLE [dbo].[ShortMessageDetails]
    ADD [StudentId] INT NULL;


GO
PRINT N'Altering [dbo].[ShortMessages]...';


GO
ALTER TABLE [dbo].[ShortMessages]
    ADD [DateFrom] DATE NULL,
        [DateTo]   DATE NULL;


GO
PRINT N'Creating [dbo].[FK_ShortMessageDetails_Students]...';


GO
ALTER TABLE [dbo].[ShortMessageDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_ShortMessageDetails_Students] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([StudentId]);


GO
PRINT N'Creating [dbo].[QryStudentAttendanceDetails]...';


GO
CREATE VIEW dbo.QryStudentAttendanceDetails
AS
SELECT     dbo.StudentAttendanceDetails.StudentAttendanceId, dbo.StudentAttendanceDetails.StudentId, dbo.StudentAttendanceDetails.AttendanceTypeId, 
                      dbo.StudentAttendanceDetails.IsAbsconding, dbo.AttendanceTypes.Flag, dbo.AttendanceTypes.IsPresent, dbo.StudentAttendances.AttendanceDate
FROM         dbo.StudentAttendances RIGHT OUTER JOIN
                      dbo.StudentAttendanceDetails ON dbo.StudentAttendances.Id = dbo.StudentAttendanceDetails.StudentAttendanceId LEFT OUTER JOIN
                      dbo.AttendanceTypes ON dbo.StudentAttendanceDetails.AttendanceTypeId = dbo.AttendanceTypes.Id
GO
PRINT N'Altering [dbo].[SprSmsGeneration]...';


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SprSmsGeneration]
	-- Add the parameters for the stored procedure here
	@Status nvarchar(50) output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @Tag table(Tag nvarchar(100),CharLimit int)
	insert into @Tag values('{NAME}',25),('{STUDENT_NAME}',25),('{FROM_DATE}',7),('{TO_DATE}',7)
	,('{PRESENT_ST}',3),('{ABSENT_ST}',3),('{ABSCONDING_ST}',3)
	
	--'Dear {NAME} Your child {STUDENT_NAME} was Present {PRESENT_ST} Absent {ABSENT_ST} Absconding {ABSCONDING_ST} from {FROM_DATE} to {TO_DATE}'
	
	declare @id int, @isStatic bit,@smsTemplate nvarchar(512),@dateFrom date,@dateTo date
	declare @smsCount int,@totalSmsCount int,@tmpCount int
	
	select top(1) @id=Id,@isStatic=IsStaticSms,@smsTemplate=SmsTemplate 
	,@dateFrom=DateFrom, @dateTo=isnull(DateTo,getdate())
	from dbo.ShortMessages where IsGenerated=0
	while @@ROWCOUNT>0
	begin
		
		if(@isStatic=1)
		begin
			set @smsCount=dbo.FunShortMessageCount(@smsTemplate)
			update dbo.ShortMessageDetails 
				set SmsText=@smsTemplate
					,MobileNumber=dbo.FunRectifyMobileNumber(MobileNumber)
					,SmsCount=@smsCount
			where ShortMessageId = @id
			set @totalSmsCount=@@ROWCOUNT*@smsCount
		end
		else
		begin
			declare @Data table(
			Id int,UserInfoId int, StudentId int,SmsText nvarchar(512)
			, Name nvarchar(128), StudentName nvarchar(128)			
			,PresentCount int, AbsentCount int, AbscondingCount int
			,SmsCount int
			)
			declare @fromDate nvarchar(7),@toDate nvarchar(7)
			declare @Tmp table(StudentId int,PresentCount int, AbsentCount int, AbscondingCount int)
			
			
			insert into @Data(Id,UserInfoId,StudentId,SmsText) 
			select Id,UserInfoId,StudentId,@smsTemplate
			from dbo.ShortMessageDetails where ShortMessageId=@id
			
			if charindex('{NAME}',@smsTemplate)>0
			begin
				update D set D.Name = substring(U.Name,0,25)
				from @Data D inner join dbo.UserInfoes U
				on D.UserInfoId=U.Id
				
				update @Data set SmsText = REPLACE(SmsText,'{NAME}',Name)			
			end
			if charindex('{STUDENT_NAME}',@smsTemplate)>0
			begin
				update D set D.StudentName=substring(U.Name,0,25)
				from @Data D inner join dbo.UserInfoes U
				on D.StudentId=U.Id
				
				update @Data set SmsText = REPLACE(SmsText,'{STUDENT_NAME}',StudentName)
			end
			if charindex('{FROM_DATE}',@smsTemplate)>0
			begin
				set @fromDate = REPLACE(CONVERT(VARCHAR(11),@dateFrom,6),' ','')
				update @Data set SmsText = REPLACE(SmsText,'{FROM_DATE}',@fromDate)
			end
			if charindex('{TO_DATE}',@smsTemplate)>0
			begin
				set @toDate = REPLACE(CONVERT(VARCHAR(11),@dateTo,6),' ','')
				update @Data set SmsText = REPLACE(SmsText,'{TO_DATE}',@toDate)
			end
			if	(charindex('{PRESENT_ST}',@smsTemplate)>0
				or charindex('{ABSENT_ST}',@smsTemplate)>0
				or charindex('{ABSCONDING_ST}',@smsTemplate)>0
				)and @dateFrom is not null
				
			begin				
				insert into @Tmp (PresentCount , AbsentCount , AbscondingCount,StudentId)
				select sum(case T.IsPresent when 1 then 1 else 0 end )
				,sum(case T.IsPresent when 1 then 0 else 1 end )
				,sum(case T.IsAbsconding when 1 then 1 else 0 end )
				,T.StudentId
				from @Data D inner join dbo.QryStudentAttendanceDetails T
				on D.StudentId=T.StudentId
				where T.AttendanceDate between @dateFrom and @dateTo
				group by T.StudentId
				
				update D set D.PresentCount = isnull(T.PresentCount,0)
				,D.AbsentCount=isnull(T.AbsentCount,0)
				,D.AbscondingCount=isnull(T.AbsentCount,0)
				from @Data D inner join @Tmp T
				on D.StudentId=T.StudentId
				
				update @Data set SmsText = REPLACE(SmsText,'{PRESENT_ST}',CONVERT(VARCHAR(3),PresentCount))
				update @Data set SmsText = REPLACE(SmsText,'{ABSENT_ST}',CONVERT(VARCHAR(3),AbsentCount))
				update @Data set SmsText = REPLACE(SmsText,'{ABSCONDING_ST}',CONVERT(VARCHAR(3),AbscondingCount))
				
			end		
			
			update @Data set SmsCount=dbo.FunShortMessageCount(SmsText)
			
			update T set T.SmsText=D.SmsText
			,T.MobileNumber=dbo.FunRectifyMobileNumber(T.MobileNumber)
			,SmsCount=dbo.FunShortMessageCount(D.SmsText)
			from dbo.ShortMessageDetails T inner join @Data D
			on T.Id=D.Id
			
			select @totalSmsCount=SUM(SmsCount) from dbo.ShortMessageDetails where ShortMessageId=@id
			
		end
		
		update dbo.ShortMessages set IsGenerated=1,TotalSmsCount=@totalSmsCount where Id=@id
		
		select top(1) @id=Id,@isStatic=IsStaticSms,@smsTemplate=SmsTemplate 
		,@dateFrom=DateFrom, @dateTo=DateTo
		from dbo.ShortMessages where IsGenerated=0
	end
	
	
	set @Status='success';
END
GO
PRINT N'Creating [dbo].[QryStudentAttendanceDetails].[MS_DiagramPane1]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[66] 4[5] 2[12] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "StudentAttendances"
            Begin Extent = 
               Top = 1
               Left = 583
               Bottom = 285
               Right = 782
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "StudentAttendanceDetails"
            Begin Extent = 
               Top = 18
               Left = 42
               Bottom = 169
               Right = 235
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AttendanceTypes"
            Begin Extent = 
               Top = 93
               Left = 378
               Bottom = 308
               Right = 562
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'QryStudentAttendanceDetails';


GO
PRINT N'Creating [dbo].[QryStudentAttendanceDetails].[MS_DiagramPaneCount]...';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'QryStudentAttendanceDetails';


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[ShortMessageDetails] WITH CHECK CHECK CONSTRAINT [FK_ShortMessageDetails_Students];


GO
update dbo._Migration set LastUpdate='0027'
PRINT N'Update complete.';


GO

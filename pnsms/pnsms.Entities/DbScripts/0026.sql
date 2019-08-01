
USE PNSMS;


GO
PRINT N'Altering [dbo].[AttendanceTypes]...';


GO
ALTER TABLE [dbo].[AttendanceTypes]
    ADD [IsUsedForTeacher] BIT NULL;


GO
PRINT N'Altering [dbo].[ShortMessageDetails]...';


GO
ALTER TABLE [dbo].[ShortMessageDetails] ALTER COLUMN [SmsCount] INT NULL;


GO
PRINT N'Altering [dbo].[ShortMessages]...';


GO
ALTER TABLE [dbo].[ShortMessages] ALTER COLUMN [TotalSmsCount] INT NULL;


GO
PRINT N'Creating [dbo].[UserAttendanceDetails]...';


GO
CREATE TABLE [dbo].[UserAttendanceDetails] (
    [Id]               INT      IDENTITY (1, 1) NOT NULL,
    [UserAttendanceId] INT      NOT NULL,
    [UserInfoId]       INT      NOT NULL,
    [AttendanceTypeId] INT      NOT NULL,
    [InTime]           DATETIME NULL,
    [OutTime]          DATETIME NULL,
    [LastUpdateTime]   DATETIME NULL,
    CONSTRAINT [PK_UserAttendanceDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[UserAttendances]...';


GO
CREATE TABLE [dbo].[UserAttendances] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [UserInfoTypeId]    INT             NOT NULL,
    [AcademicBranchId]  INT             NOT NULL,
    [AttendanceDate]    DATETIME        NOT NULL,
    [PresentCount]      INT             NULL,
    [AbsentCount]       INT             NULL,
    [TotalCount]        INT             NULL,
    [PresentPercentage] DECIMAL (18, 2) NULL,
    [AbsentPercentage]  DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_UserAttendances] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_UserAttendanceDetails_UserAttendances]...';


GO
ALTER TABLE [dbo].[UserAttendanceDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_UserAttendanceDetails_UserAttendances] FOREIGN KEY ([UserAttendanceId]) REFERENCES [dbo].[UserAttendances] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserAttendanceDetails_UserInfoes]...';


GO
ALTER TABLE [dbo].[UserAttendanceDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_UserAttendanceDetails_UserInfoes] FOREIGN KEY ([UserInfoId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserAttendanceDetails_AddressTypes]...';


GO
ALTER TABLE [dbo].[UserAttendanceDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_UserAttendanceDetails_AddressTypes] FOREIGN KEY ([AttendanceTypeId]) REFERENCES [dbo].[AddressTypes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserAttendances_UserInfoTypes]...';


GO
ALTER TABLE [dbo].[UserAttendances] WITH NOCHECK
    ADD CONSTRAINT [FK_UserAttendances_UserInfoTypes] FOREIGN KEY ([UserInfoTypeId]) REFERENCES [dbo].[UserInfoTypes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_UserAttendances_AcademicBranches]...';


GO
ALTER TABLE [dbo].[UserAttendances] WITH NOCHECK
    ADD CONSTRAINT [FK_UserAttendances_AcademicBranches] FOREIGN KEY ([AcademicBranchId]) REFERENCES [dbo].[AcademicBranches] ([Id]);


GO
PRINT N'Creating [dbo].[FunRectifyMobileNumber]...';


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[FunRectifyMobileNumber]
(
	-- Add the parameters for the function here
	@MobileNumber nvarchar(50)
)
RETURNS nvarchar(50)
AS
BEGIN
	declare @l int
	
	WHILE PATINDEX('%[^0-9]%', @MobileNumber) > 0
    BEGIN
        SET @MobileNumber = STUFF(@MobileNumber, PATINDEX('%[^0-9]%', @MobileNumber), 1, '')
    END
    
	set @l=LEN(@MobileNumber);	
	set @MobileNumber = '880'+SUBSTRING(@MobileNumber,@l-9,@l)
	
	
	return @MobileNumber
END
GO
PRINT N'Creating [dbo].[FunShortMessageCount]...';


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[FunShortMessageCount]
(
	-- Add the parameters for the function here
	@Message nvarchar(512)
)
RETURNS int
AS
BEGIN
	declare @count int
	declare @len int
	
	set @len = LEN(@Message)
	
	if(@len<=160)
	begin
		set @count=1		
	end
	else
	begin
		set @len=@len-160;		
		set @count = CEILING((@len/140.00))+1;
	end
	
	return @count
END
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
	
	set @Status='success';
END
GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[UserAttendanceDetails] WITH CHECK CHECK CONSTRAINT [FK_UserAttendanceDetails_UserAttendances];

ALTER TABLE [dbo].[UserAttendanceDetails] WITH CHECK CHECK CONSTRAINT [FK_UserAttendanceDetails_UserInfoes];

ALTER TABLE [dbo].[UserAttendanceDetails] WITH CHECK CHECK CONSTRAINT [FK_UserAttendanceDetails_AddressTypes];

ALTER TABLE [dbo].[UserAttendances] WITH CHECK CHECK CONSTRAINT [FK_UserAttendances_UserInfoTypes];

ALTER TABLE [dbo].[UserAttendances] WITH CHECK CHECK CONSTRAINT [FK_UserAttendances_AcademicBranches];


GO
update dbo._Migration set LastUpdate='0026'
PRINT N'Update complete.';


GO

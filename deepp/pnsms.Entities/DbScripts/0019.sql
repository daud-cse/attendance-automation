
USE PNSMS;


GO
PRINT N'Creating [dbo].[Exam]...';


GO
CREATE TABLE [dbo].[Exam] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT             NOT NULL,
    [BranchId]       INT             NOT NULL,
    [ClassId]        INT             NOT NULL,
    [SectionId]      INT             NULL,
    [ExamTypeId]     INT             NOT NULL,
    [IsGroupExam]    BIT             NOT NULL,
    [Name]           NVARCHAR (125)  NOT NULL,
    [ExamDateFrom]   DATE            NULL,
    [ExamDateTo]     DATE            NULL,
    [ExamTime]       NVARCHAR (50)   NULL,
    [TotalMarks]     DECIMAL (18, 2) NULL,
    [HighestMarks]   DECIMAL (18, 2) NULL,
    [LastUpdateTime] DATETIME        NOT NULL,
    [IsActive]       BIT             NOT NULL,
    CONSTRAINT [PK_Exam] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[ExamSubject]...';


GO
CREATE TABLE [dbo].[ExamSubject] (
    [Id]                      INT             IDENTITY (1, 1) NOT NULL,
    [ExamId]                  INT             NOT NULL,
    [TecherId]                INT             NOT NULL,
    [InstituteSubjectClassId] INT             NOT NULL,
    [TotalMarks]              DECIMAL (18, 2) NULL,
    [PassMarks]               DECIMAL (18, 2) NULL,
    [HighestMarks]            DECIMAL (18, 2) NULL,
    [ExamDate]                DATE            NULL,
    [ExamTime]                NVARCHAR (50)   NULL,
    [TotalAttended]           INT             NULL,
    [IsActive]                BIT             NOT NULL,
    CONSTRAINT [PK_ExamSubject_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[ExamSubjectMarks]...';


GO
CREATE TABLE [dbo].[ExamSubjectMarks] (
    [Id]                      INT             IDENTITY (1, 1) NOT NULL,
    [InstituteId]             INT             NOT NULL,
    [ExamId]                  INT             NOT NULL,
    [InstituteSubjectClassId] INT             NOT NULL,
    [StudentId]               INT             NOT NULL,
    [Marks]                   DECIMAL (18, 2) NULL,
    [LastUpdateTime]          DATETIME        NOT NULL,
    CONSTRAINT [PK_ExamSubjectMarks] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[ExamType]...';


GO
CREATE TABLE [dbo].[ExamType] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [InstituteId] INT           NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (MAX) NULL,
    [IsActive]    BIT           NOT NULL,
    CONSTRAINT [PK_ExamType] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[InstituteSubject]...';


GO
CREATE TABLE [dbo].[InstituteSubject] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [InstituteId] INT           NOT NULL,
    [SubjectId]   INT           NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (MAX) NULL,
    [IsActive]    BIT           NOT NULL,
    CONSTRAINT [PK_InstituteSubject] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[InstituteSubjectClass]...';


GO
CREATE TABLE [dbo].[InstituteSubjectClass] (
    [Id]                 INT IDENTITY (1, 1) NOT NULL,
    [InstituteSubjectId] INT NOT NULL,
    [ClassId]            INT NOT NULL,
    [IsActive]           BIT NOT NULL,
    CONSTRAINT [PK_InstituteSubjectClass] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[MobilePayments]...';


GO
CREATE TABLE [dbo].[MobilePayments] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [InstituteId]     INT           NOT NULL,
    [MobileNo]        NVARCHAR (32) NOT NULL,
    [ReffStudentId]   INT           NULL,
    [Payment]         MONEY         NOT NULL,
    [TransactionId]   NVARCHAR (50) NOT NULL,
    [TransactionDate] DATE          NOT NULL,
    [PaymentTypeId]   INT           NOT NULL,
    [LastActionBy]    INT           NOT NULL,
    [LastUpdateTime]  DATETIME      NULL,
    CONSTRAINT [PK_MobilePayment] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[PaymentTypes]...';


GO
CREATE TABLE [dbo].[PaymentTypes] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (128) NOT NULL,
    [IsActive] BIT            NOT NULL,
    CONSTRAINT [PK_PaymentType] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[ShortMessageDetails]...';


GO
CREATE TABLE [dbo].[ShortMessageDetails] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [ShortMessageId]          INT            NOT NULL,
    [SmsText]                 NVARCHAR (512) NULL,
    [MobileNumber]            VARCHAR (50)   NOT NULL,
    [UserInfoId]              INT            NULL,
    [SmsCount]                INT            NOT NULL,
    [IsSent]                  BIT            NOT NULL,
    [Status]                  NVARCHAR (128) NULL,
    [GatewayIdentificationNo] NVARCHAR (128) NULL,
    [IsStatusUpdated]         BIT            NULL,
    [StatusUpdatedAt]         DATETIME       NULL,
    CONSTRAINT [PK_ShortMessageDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[ShortMessages]...';


GO
CREATE TABLE [dbo].[ShortMessages] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]   INT            NOT NULL,
    [SmsTemplate]   NVARCHAR (512) NOT NULL,
    [IsStaticSms]   BIT            NOT NULL,
    [SendAt]        DATETIME       NOT NULL,
    [TotalSmsCount] INT            NOT NULL,
    [Mask]          NVARCHAR (16)  NULL,
    [IsSent]        BIT            NOT NULL,
    [IsGenerated]   BIT            NOT NULL,
    CONSTRAINT [PK_ShortMessages] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Subject]...';


GO
CREATE TABLE [dbo].[Subject] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (50) NULL,
    [RefSubjectId] INT          NULL,
    [Description]  VARCHAR (50) NULL,
    [IsActive]     INT          NOT NULL,
    CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_Exam_AcademicBranches]...';


GO
ALTER TABLE [dbo].[Exam] WITH NOCHECK
    ADD CONSTRAINT [FK_Exam_AcademicBranches] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[AcademicBranches] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Exam_AcademicClasses]...';


GO
ALTER TABLE [dbo].[Exam] WITH NOCHECK
    ADD CONSTRAINT [FK_Exam_AcademicClasses] FOREIGN KEY ([ClassId]) REFERENCES [dbo].[AcademicClasses] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Exam_AcademicSections]...';


GO
ALTER TABLE [dbo].[Exam] WITH NOCHECK
    ADD CONSTRAINT [FK_Exam_AcademicSections] FOREIGN KEY ([SectionId]) REFERENCES [dbo].[AcademicSections] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Exam_ExamType]...';


GO
ALTER TABLE [dbo].[Exam] WITH NOCHECK
    ADD CONSTRAINT [FK_Exam_ExamType] FOREIGN KEY ([ExamTypeId]) REFERENCES [dbo].[ExamType] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Exam_Institutes]...';


GO
ALTER TABLE [dbo].[Exam] WITH NOCHECK
    ADD CONSTRAINT [FK_Exam_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ExamSubject_Exam]...';


GO
ALTER TABLE [dbo].[ExamSubject] WITH NOCHECK
    ADD CONSTRAINT [FK_ExamSubject_Exam] FOREIGN KEY ([ExamId]) REFERENCES [dbo].[Exam] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ExamSubject_InstituteSubjectClass]...';


GO
ALTER TABLE [dbo].[ExamSubject] WITH NOCHECK
    ADD CONSTRAINT [FK_ExamSubject_InstituteSubjectClass] FOREIGN KEY ([InstituteSubjectClassId]) REFERENCES [dbo].[InstituteSubjectClass] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ExamSubject_Teachers]...';


GO
ALTER TABLE [dbo].[ExamSubject] WITH NOCHECK
    ADD CONSTRAINT [FK_ExamSubject_Teachers] FOREIGN KEY ([TecherId]) REFERENCES [dbo].[Teachers] ([TeacherId]);


GO
PRINT N'Creating [dbo].[FK_ExamSubjectMarks_Exam]...';


GO
ALTER TABLE [dbo].[ExamSubjectMarks] WITH NOCHECK
    ADD CONSTRAINT [FK_ExamSubjectMarks_Exam] FOREIGN KEY ([ExamId]) REFERENCES [dbo].[Exam] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ExamSubjectMarks_Institutes]...';


GO
ALTER TABLE [dbo].[ExamSubjectMarks] WITH NOCHECK
    ADD CONSTRAINT [FK_ExamSubjectMarks_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ExamSubjectMarks_InstituteSubjectClass]...';


GO
ALTER TABLE [dbo].[ExamSubjectMarks] WITH NOCHECK
    ADD CONSTRAINT [FK_ExamSubjectMarks_InstituteSubjectClass] FOREIGN KEY ([InstituteSubjectClassId]) REFERENCES [dbo].[InstituteSubjectClass] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ExamSubjectMarks_Students]...';


GO
ALTER TABLE [dbo].[ExamSubjectMarks] WITH NOCHECK
    ADD CONSTRAINT [FK_ExamSubjectMarks_Students] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([StudentId]);


GO
PRINT N'Creating [dbo].[FK_ExamType_Institutes]...';


GO
ALTER TABLE [dbo].[ExamType] WITH NOCHECK
    ADD CONSTRAINT [FK_ExamType_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_InstituteSubject_Institutes]...';


GO
ALTER TABLE [dbo].[InstituteSubject] WITH NOCHECK
    ADD CONSTRAINT [FK_InstituteSubject_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_InstituteSubject_Subject]...';


GO
ALTER TABLE [dbo].[InstituteSubject] WITH NOCHECK
    ADD CONSTRAINT [FK_InstituteSubject_Subject] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subject] ([Id]);


GO
PRINT N'Creating [dbo].[FK_InstituteSubjectClass_AcademicClasses]...';


GO
ALTER TABLE [dbo].[InstituteSubjectClass] WITH NOCHECK
    ADD CONSTRAINT [FK_InstituteSubjectClass_AcademicClasses] FOREIGN KEY ([ClassId]) REFERENCES [dbo].[AcademicClasses] ([Id]);


GO
PRINT N'Creating [dbo].[FK_InstituteSubjectClass_InstituteSubject]...';


GO
ALTER TABLE [dbo].[InstituteSubjectClass] WITH NOCHECK
    ADD CONSTRAINT [FK_InstituteSubjectClass_InstituteSubject] FOREIGN KEY ([InstituteSubjectId]) REFERENCES [dbo].[InstituteSubject] ([Id]);


GO
PRINT N'Creating [dbo].[FK_MobilePayment_PaymentType]...';


GO
ALTER TABLE [dbo].[MobilePayments] WITH NOCHECK
    ADD CONSTRAINT [FK_MobilePayment_PaymentType] FOREIGN KEY ([PaymentTypeId]) REFERENCES [dbo].[PaymentTypes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_MobilePayment_Student]...';


GO
ALTER TABLE [dbo].[MobilePayments] WITH NOCHECK
    ADD CONSTRAINT [FK_MobilePayment_Student] FOREIGN KEY ([ReffStudentId]) REFERENCES [dbo].[Students] ([StudentId]);


GO
PRINT N'Creating [dbo].[FK_MobilePayments_Institute]...';


GO
ALTER TABLE [dbo].[MobilePayments] WITH NOCHECK
    ADD CONSTRAINT [FK_MobilePayments_Institute] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ShortMessageDetails_ShortMessages]...';


GO
ALTER TABLE [dbo].[ShortMessageDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_ShortMessageDetails_ShortMessages] FOREIGN KEY ([ShortMessageId]) REFERENCES [dbo].[ShortMessages] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ShortMessageDetails_UserInfoes]...';


GO
ALTER TABLE [dbo].[ShortMessageDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_ShortMessageDetails_UserInfoes] FOREIGN KEY ([UserInfoId]) REFERENCES [dbo].[UserInfoes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ShortMessages_Institutes]...';


GO
ALTER TABLE [dbo].[ShortMessages] WITH NOCHECK
    ADD CONSTRAINT [FK_ShortMessages_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Subject_Subject]...';


GO
ALTER TABLE [dbo].[Subject] WITH NOCHECK
    ADD CONSTRAINT [FK_Subject_Subject] FOREIGN KEY ([RefSubjectId]) REFERENCES [dbo].[Subject] ([Id]);


GO
PRINT N'Creating [dbo].[SprSmsGeneration]...';


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SprSmsGeneration]
	-- Add the parameters for the stored procedure here
	@Status nvarchar(50) output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	update dbo._Migration set LastUpdate='1800'
	
	set @Status='success';
END
GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[Exam] WITH CHECK CHECK CONSTRAINT [FK_Exam_AcademicBranches];

ALTER TABLE [dbo].[Exam] WITH CHECK CHECK CONSTRAINT [FK_Exam_AcademicClasses];

ALTER TABLE [dbo].[Exam] WITH CHECK CHECK CONSTRAINT [FK_Exam_AcademicSections];

ALTER TABLE [dbo].[Exam] WITH CHECK CHECK CONSTRAINT [FK_Exam_ExamType];

ALTER TABLE [dbo].[Exam] WITH CHECK CHECK CONSTRAINT [FK_Exam_Institutes];

ALTER TABLE [dbo].[ExamSubject] WITH CHECK CHECK CONSTRAINT [FK_ExamSubject_Exam];

ALTER TABLE [dbo].[ExamSubject] WITH CHECK CHECK CONSTRAINT [FK_ExamSubject_InstituteSubjectClass];

ALTER TABLE [dbo].[ExamSubject] WITH CHECK CHECK CONSTRAINT [FK_ExamSubject_Teachers];

ALTER TABLE [dbo].[ExamSubjectMarks] WITH CHECK CHECK CONSTRAINT [FK_ExamSubjectMarks_Exam];

ALTER TABLE [dbo].[ExamSubjectMarks] WITH CHECK CHECK CONSTRAINT [FK_ExamSubjectMarks_Institutes];

ALTER TABLE [dbo].[ExamSubjectMarks] WITH CHECK CHECK CONSTRAINT [FK_ExamSubjectMarks_InstituteSubjectClass];

ALTER TABLE [dbo].[ExamSubjectMarks] WITH CHECK CHECK CONSTRAINT [FK_ExamSubjectMarks_Students];

ALTER TABLE [dbo].[ExamType] WITH CHECK CHECK CONSTRAINT [FK_ExamType_Institutes];

ALTER TABLE [dbo].[InstituteSubject] WITH CHECK CHECK CONSTRAINT [FK_InstituteSubject_Institutes];

ALTER TABLE [dbo].[InstituteSubject] WITH CHECK CHECK CONSTRAINT [FK_InstituteSubject_Subject];

ALTER TABLE [dbo].[InstituteSubjectClass] WITH CHECK CHECK CONSTRAINT [FK_InstituteSubjectClass_AcademicClasses];

ALTER TABLE [dbo].[InstituteSubjectClass] WITH CHECK CHECK CONSTRAINT [FK_InstituteSubjectClass_InstituteSubject];

ALTER TABLE [dbo].[MobilePayments] WITH CHECK CHECK CONSTRAINT [FK_MobilePayment_PaymentType];

ALTER TABLE [dbo].[MobilePayments] WITH CHECK CHECK CONSTRAINT [FK_MobilePayment_Student];

ALTER TABLE [dbo].[MobilePayments] WITH CHECK CHECK CONSTRAINT [FK_MobilePayments_Institute];

ALTER TABLE [dbo].[ShortMessageDetails] WITH CHECK CHECK CONSTRAINT [FK_ShortMessageDetails_ShortMessages];

ALTER TABLE [dbo].[ShortMessageDetails] WITH CHECK CHECK CONSTRAINT [FK_ShortMessageDetails_UserInfoes];

ALTER TABLE [dbo].[ShortMessages] WITH CHECK CHECK CONSTRAINT [FK_ShortMessages_Institutes];

ALTER TABLE [dbo].[Subject] WITH CHECK CHECK CONSTRAINT [FK_Subject_Subject];


GO

update dbo._Migration set LastUpdate='0019'
PRINT N'Update complete.';


GO

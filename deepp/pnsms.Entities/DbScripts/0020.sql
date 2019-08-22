
USE PNSMS;


GO
PRINT N'Altering [dbo].[CoCurricularActivities]...';


GO
ALTER TABLE [dbo].[CoCurricularActivities]
    ADD [GlobalCoCurricularActivitiyId] INT NULL;


GO
PRINT N'Altering [dbo].[Countries]...';


GO
ALTER TABLE [dbo].[Countries]
    ADD [GlobalCountryId] INT NULL;


GO
PRINT N'Altering [dbo].[DistrictOrStates]...';


GO
ALTER TABLE [dbo].[DistrictOrStates]
    ADD [GlobalDistrictId] INT NULL;


GO
PRINT N'Altering [dbo].[EducationalQualifications]...';


GO
ALTER TABLE [dbo].[EducationalQualifications]
    ADD [GlobalEducationalQualificationId] INT NULL;


GO
PRINT N'Altering [dbo].[Institutes]...';


GO
ALTER TABLE [dbo].[Institutes]
    ADD [GlobalCountryId]       INT NULL,
        [GlobalDivisionId]      INT NULL,
        [GlobalDistrictId]      INT NULL,
        [GlobalSubDistrictId]   INT NULL,
        [VisitorToday]          INT NULL,
        [VisitorThisMonth]      INT NULL,
        [VisitorTotal]          INT NULL,
        [GlobalInstituteTypeId] INT NULL;


GO
PRINT N'Altering [dbo].[Professions]...';


GO
ALTER TABLE [dbo].[Professions]
    ADD [GlobalProfessionId] INT NULL;


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
PRINT N'Creating [dbo].[GlobalCoCurricularActivities]...';


GO
CREATE TABLE [dbo].[GlobalCoCurricularActivities] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (256) NOT NULL,
    [IsActive] BIT            NOT NULL,
    CONSTRAINT [PK_GlobalCoCurricularActivities] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[GlobalCountries]...';


GO
CREATE TABLE [dbo].[GlobalCountries] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (256) NOT NULL,
    [IsActive] BIT            NOT NULL,
    CONSTRAINT [PK_GlobalCountries] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[GlobalDistricts]...';


GO
CREATE TABLE [dbo].[GlobalDistricts] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [GlobalDivisionId] INT            NOT NULL,
    [Name]             NVARCHAR (256) NOT NULL,
    [IsActive]         BIT            NOT NULL,
    CONSTRAINT [PK_GlobalDistricts] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[GlobalDivisions]...';


GO
CREATE TABLE [dbo].[GlobalDivisions] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [GlobalCountryId] INT            NOT NULL,
    [Name]            NVARCHAR (256) NOT NULL,
    [IsActive]        BIT            NOT NULL,
    CONSTRAINT [PK_GlobalDivisions] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[GlobalEducationalQualifications]...';


GO
CREATE TABLE [dbo].[GlobalEducationalQualifications] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (256) NOT NULL,
    [IsActive] BIT            NOT NULL,
    CONSTRAINT [PK_GlobalEducationalQualifications] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[GlobalInstituteTypes]...';


GO
CREATE TABLE [dbo].[GlobalInstituteTypes] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (256) NOT NULL,
    [IsActive] BIT            NOT NULL,
    CONSTRAINT [PK_GlobalInstituteTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[GlobalProfessions]...';


GO
CREATE TABLE [dbo].[GlobalProfessions] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (256) NOT NULL,
    [IsActive] BIT            NOT NULL,
    CONSTRAINT [PK_GlobalProfessions] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[GlobalScholarships]...';


GO
CREATE TABLE [dbo].[GlobalScholarships] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (256) NOT NULL,
    [IsActive] BIT            NOT NULL,
    CONSTRAINT [PK_GlobalScholarships] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[GlobalSubDistricts]...';


GO
CREATE TABLE [dbo].[GlobalSubDistricts] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [GlobalDistrictId]        INT            NOT NULL,
    [GlobalSubDistrictTypeId] INT            NOT NULL,
    [Name]                    NVARCHAR (256) NOT NULL,
    [IsActive]                BIT            NOT NULL,
    CONSTRAINT [PK_GlobalSubDistricts] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[GlobalSubDistrictTypes]...';


GO
CREATE TABLE [dbo].[GlobalSubDistrictTypes] (
    [Id]   INT            NOT NULL,
    [Name] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_GlobalSubDistrictTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
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
PRINT N'Creating [dbo].[MessageTagGroups]...';


GO
CREATE TABLE [dbo].[MessageTagGroups] (
    [Id]            INT            NOT NULL,
    [Name]          NVARCHAR (128) NOT NULL,
    [IsForStudent]  BIT            NOT NULL,
    [IsForTeacher]  BIT            NOT NULL,
    [IsForEmployee] BIT            NOT NULL,
    CONSTRAINT [PK_MessageTagGroups] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[MessageTags]...';


GO
CREATE TABLE [dbo].[MessageTags] (
    [Id]                INT            NOT NULL,
    [MessageTagGroupId] INT            NOT NULL,
    [TagName]           NVARCHAR (128) NOT NULL,
    [TagDescription]    NVARCHAR (512) NULL,
    [CharCount]         INT            NOT NULL,
    CONSTRAINT [PK_MessageTags] PRIMARY KEY CLUSTERED ([Id] ASC)
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
PRINT N'Creating [dbo].[ScholarshipOfStudents]...';


GO
CREATE TABLE [dbo].[ScholarshipOfStudents] (
    [Id]                INT IDENTITY (1, 1) NOT NULL,
    [StudentId]         INT NOT NULL,
    [ScholarshipId]     INT NOT NULL,
    [AcademicSessionId] INT NOT NULL,
    CONSTRAINT [PK_ScholarshipOfStudents] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Scholarships]...';


GO
CREATE TABLE [dbo].[Scholarships] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [InstituteId]         INT             NOT NULL,
    [Name]                NVARCHAR (256)  NOT NULL,
    [Description]         NVARCHAR (1024) NULL,
    [IsActive]            BIT             NOT NULL,
    [LastUpdateTime]      DATETIME        NULL,
    [GlobalScholarshipId] INT             NULL,
    CONSTRAINT [PK_Scholarships] PRIMARY KEY CLUSTERED ([Id] ASC)
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
PRINT N'Creating [dbo].[ShortMessageTemplates]...';


GO
CREATE TABLE [dbo].[ShortMessageTemplates] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT            NOT NULL,
    [SmsTemplate]    NVARCHAR (512) NOT NULL,
    [IsStaticSms]    BIT            NOT NULL,
    [IsActive]       BIT            NOT NULL,
    [IsForGeneral]   BIT            NOT NULL,
    [IsForStudent]   BIT            NOT NULL,
    [IsForTeacher]   BIT            NOT NULL,
    [IsForEmployee]  BIT            NOT NULL,
    [LastUpdateTime] DATETIME       NULL,
    [SmsCount]       INT            NOT NULL,
    CONSTRAINT [PK_ShortMessageTemplates] PRIMARY KEY CLUSTERED ([Id] ASC)
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
PRINT N'Creating [dbo].[VisitorCounts]...';


GO
CREATE TABLE [dbo].[VisitorCounts] (
    [Id]           INT  IDENTITY (1, 1) NOT NULL,
    [InstituteId]  INT  NOT NULL,
    [Date]         DATE NOT NULL,
    [VisitorCount] INT  NOT NULL,
    CONSTRAINT [PK_VisitorCounts] PRIMARY KEY CLUSTERED ([Id] ASC)
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
PRINT N'Creating [dbo].[FK_GlobalDistricts_GlobalDivisions]...';


GO
ALTER TABLE [dbo].[GlobalDistricts] WITH NOCHECK
    ADD CONSTRAINT [FK_GlobalDistricts_GlobalDivisions] FOREIGN KEY ([GlobalDivisionId]) REFERENCES [dbo].[GlobalDivisions] ([Id]);


GO
PRINT N'Creating [dbo].[FK_GlobalDivisions_GlobalCountries]...';


GO
ALTER TABLE [dbo].[GlobalDivisions] WITH NOCHECK
    ADD CONSTRAINT [FK_GlobalDivisions_GlobalCountries] FOREIGN KEY ([GlobalCountryId]) REFERENCES [dbo].[GlobalCountries] ([Id]);


GO
PRINT N'Creating [dbo].[FK_GlobalSubDistricts_GlobalDistricts]...';


GO
ALTER TABLE [dbo].[GlobalSubDistricts] WITH NOCHECK
    ADD CONSTRAINT [FK_GlobalSubDistricts_GlobalDistricts] FOREIGN KEY ([GlobalDistrictId]) REFERENCES [dbo].[GlobalDistricts] ([Id]);


GO
PRINT N'Creating [dbo].[FK_GlobalSubDistricts_GlobalSubDistrictTypes]...';


GO
ALTER TABLE [dbo].[GlobalSubDistricts] WITH NOCHECK
    ADD CONSTRAINT [FK_GlobalSubDistricts_GlobalSubDistrictTypes] FOREIGN KEY ([GlobalSubDistrictTypeId]) REFERENCES [dbo].[GlobalSubDistrictTypes] ([Id]);


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
PRINT N'Creating [dbo].[FK_MessageTags_MessageTagGroups]...';


GO
ALTER TABLE [dbo].[MessageTags] WITH NOCHECK
    ADD CONSTRAINT [FK_MessageTags_MessageTagGroups] FOREIGN KEY ([MessageTagGroupId]) REFERENCES [dbo].[MessageTagGroups] ([Id]);


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
PRINT N'Creating [dbo].[FK_ScholarshipOfStudents_Students]...';


GO
ALTER TABLE [dbo].[ScholarshipOfStudents] WITH NOCHECK
    ADD CONSTRAINT [FK_ScholarshipOfStudents_Students] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([StudentId]);


GO
PRINT N'Creating [dbo].[FK_ScholarshipOfStudents_Scholarships]...';


GO
ALTER TABLE [dbo].[ScholarshipOfStudents] WITH NOCHECK
    ADD CONSTRAINT [FK_ScholarshipOfStudents_Scholarships] FOREIGN KEY ([ScholarshipId]) REFERENCES [dbo].[Scholarships] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ScholarshipOfStudents_AcademicSessions]...';


GO
ALTER TABLE [dbo].[ScholarshipOfStudents] WITH NOCHECK
    ADD CONSTRAINT [FK_ScholarshipOfStudents_AcademicSessions] FOREIGN KEY ([AcademicSessionId]) REFERENCES [dbo].[AcademicSessions] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Scholarships_Institutes]...';


GO
ALTER TABLE [dbo].[Scholarships] WITH NOCHECK
    ADD CONSTRAINT [FK_Scholarships_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Scholarships_GlobalScholarships]...';


GO
ALTER TABLE [dbo].[Scholarships] WITH NOCHECK
    ADD CONSTRAINT [FK_Scholarships_GlobalScholarships] FOREIGN KEY ([GlobalScholarshipId]) REFERENCES [dbo].[GlobalScholarships] ([Id]);


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
PRINT N'Creating [dbo].[FK_ShortMessageTemplates_Institutes]...';


GO
ALTER TABLE [dbo].[ShortMessageTemplates] WITH NOCHECK
    ADD CONSTRAINT [FK_ShortMessageTemplates_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Subject_Subject]...';


GO
ALTER TABLE [dbo].[Subject] WITH NOCHECK
    ADD CONSTRAINT [FK_Subject_Subject] FOREIGN KEY ([RefSubjectId]) REFERENCES [dbo].[Subject] ([Id]);


GO
PRINT N'Creating [dbo].[FK_VisitorCounts_Institutes]...';


GO
ALTER TABLE [dbo].[VisitorCounts] WITH NOCHECK
    ADD CONSTRAINT [FK_VisitorCounts_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_CoCurricularActivities_GlobalCoCurricularActivities]...';


GO
ALTER TABLE [dbo].[CoCurricularActivities] WITH NOCHECK
    ADD CONSTRAINT [FK_CoCurricularActivities_GlobalCoCurricularActivities] FOREIGN KEY ([GlobalCoCurricularActivitiyId]) REFERENCES [dbo].[GlobalCoCurricularActivities] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Countries_GlobalCountries]...';


GO
ALTER TABLE [dbo].[Countries] WITH NOCHECK
    ADD CONSTRAINT [FK_Countries_GlobalCountries] FOREIGN KEY ([GlobalCountryId]) REFERENCES [dbo].[GlobalCountries] ([Id]);


GO
PRINT N'Creating [dbo].[FK_DistrictOrStates_GlobalDistricts]...';


GO
ALTER TABLE [dbo].[DistrictOrStates] WITH NOCHECK
    ADD CONSTRAINT [FK_DistrictOrStates_GlobalDistricts] FOREIGN KEY ([GlobalDistrictId]) REFERENCES [dbo].[GlobalDistricts] ([Id]);


GO
PRINT N'Creating [dbo].[FK_EducationalQualifications_GlobalEducationalQualifications]...';


GO
ALTER TABLE [dbo].[EducationalQualifications] WITH NOCHECK
    ADD CONSTRAINT [FK_EducationalQualifications_GlobalEducationalQualifications] FOREIGN KEY ([GlobalEducationalQualificationId]) REFERENCES [dbo].[GlobalEducationalQualifications] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Institutes_GlobalCountries]...';


GO
ALTER TABLE [dbo].[Institutes] WITH NOCHECK
    ADD CONSTRAINT [FK_Institutes_GlobalCountries] FOREIGN KEY ([GlobalCountryId]) REFERENCES [dbo].[GlobalCountries] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Institutes_GlobalDistricts]...';


GO
ALTER TABLE [dbo].[Institutes] WITH NOCHECK
    ADD CONSTRAINT [FK_Institutes_GlobalDistricts] FOREIGN KEY ([GlobalDistrictId]) REFERENCES [dbo].[GlobalDistricts] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Institutes_GlobalDivisions]...';


GO
ALTER TABLE [dbo].[Institutes] WITH NOCHECK
    ADD CONSTRAINT [FK_Institutes_GlobalDivisions] FOREIGN KEY ([GlobalDivisionId]) REFERENCES [dbo].[GlobalDivisions] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Institutes_GlobalInstituteTypes]...';


GO
ALTER TABLE [dbo].[Institutes] WITH NOCHECK
    ADD CONSTRAINT [FK_Institutes_GlobalInstituteTypes] FOREIGN KEY ([GlobalInstituteTypeId]) REFERENCES [dbo].[GlobalInstituteTypes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Institutes_GlobalSubDistricts]...';


GO
ALTER TABLE [dbo].[Institutes] WITH NOCHECK
    ADD CONSTRAINT [FK_Institutes_GlobalSubDistricts] FOREIGN KEY ([GlobalSubDistrictId]) REFERENCES [dbo].[GlobalSubDistricts] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Professions_GlobalProfessions]...';


GO
ALTER TABLE [dbo].[Professions] WITH NOCHECK
    ADD CONSTRAINT [FK_Professions_GlobalProfessions] FOREIGN KEY ([GlobalProfessionId]) REFERENCES [dbo].[GlobalProfessions] ([Id]);


GO
PRINT N'Creating [dbo].[SprGetUserCode]...';


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SprGetUserCode
	-- Add the parameters for the stored procedure here
	@UserInfoId int,
	@PIN nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	set @PIN='123'
END
GO
PRINT N'Creating [dbo].[SprInstituteDefaultSetup]...';


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SprInstituteDefaultSetup 
	-- Add the parameters for the stored procedure here
	@InstituteId int,
	@IsSuccess bit output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	set @IsSuccess=1
END
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

ALTER TABLE [dbo].[GlobalDistricts] WITH CHECK CHECK CONSTRAINT [FK_GlobalDistricts_GlobalDivisions];

ALTER TABLE [dbo].[GlobalDivisions] WITH CHECK CHECK CONSTRAINT [FK_GlobalDivisions_GlobalCountries];

ALTER TABLE [dbo].[GlobalSubDistricts] WITH CHECK CHECK CONSTRAINT [FK_GlobalSubDistricts_GlobalDistricts];

ALTER TABLE [dbo].[GlobalSubDistricts] WITH CHECK CHECK CONSTRAINT [FK_GlobalSubDistricts_GlobalSubDistrictTypes];

ALTER TABLE [dbo].[InstituteSubject] WITH CHECK CHECK CONSTRAINT [FK_InstituteSubject_Institutes];

ALTER TABLE [dbo].[InstituteSubject] WITH CHECK CHECK CONSTRAINT [FK_InstituteSubject_Subject];

ALTER TABLE [dbo].[InstituteSubjectClass] WITH CHECK CHECK CONSTRAINT [FK_InstituteSubjectClass_AcademicClasses];

ALTER TABLE [dbo].[InstituteSubjectClass] WITH CHECK CHECK CONSTRAINT [FK_InstituteSubjectClass_InstituteSubject];

ALTER TABLE [dbo].[MessageTags] WITH CHECK CHECK CONSTRAINT [FK_MessageTags_MessageTagGroups];

ALTER TABLE [dbo].[MobilePayments] WITH CHECK CHECK CONSTRAINT [FK_MobilePayment_PaymentType];

ALTER TABLE [dbo].[MobilePayments] WITH CHECK CHECK CONSTRAINT [FK_MobilePayment_Student];

ALTER TABLE [dbo].[MobilePayments] WITH CHECK CHECK CONSTRAINT [FK_MobilePayments_Institute];

ALTER TABLE [dbo].[ScholarshipOfStudents] WITH CHECK CHECK CONSTRAINT [FK_ScholarshipOfStudents_Students];

ALTER TABLE [dbo].[ScholarshipOfStudents] WITH CHECK CHECK CONSTRAINT [FK_ScholarshipOfStudents_Scholarships];

ALTER TABLE [dbo].[ScholarshipOfStudents] WITH CHECK CHECK CONSTRAINT [FK_ScholarshipOfStudents_AcademicSessions];

ALTER TABLE [dbo].[Scholarships] WITH CHECK CHECK CONSTRAINT [FK_Scholarships_Institutes];

ALTER TABLE [dbo].[Scholarships] WITH CHECK CHECK CONSTRAINT [FK_Scholarships_GlobalScholarships];

ALTER TABLE [dbo].[ShortMessageDetails] WITH CHECK CHECK CONSTRAINT [FK_ShortMessageDetails_ShortMessages];

ALTER TABLE [dbo].[ShortMessageDetails] WITH CHECK CHECK CONSTRAINT [FK_ShortMessageDetails_UserInfoes];

ALTER TABLE [dbo].[ShortMessages] WITH CHECK CHECK CONSTRAINT [FK_ShortMessages_Institutes];

ALTER TABLE [dbo].[ShortMessageTemplates] WITH CHECK CHECK CONSTRAINT [FK_ShortMessageTemplates_Institutes];

ALTER TABLE [dbo].[Subject] WITH CHECK CHECK CONSTRAINT [FK_Subject_Subject];

ALTER TABLE [dbo].[VisitorCounts] WITH CHECK CHECK CONSTRAINT [FK_VisitorCounts_Institutes];

ALTER TABLE [dbo].[CoCurricularActivities] WITH CHECK CHECK CONSTRAINT [FK_CoCurricularActivities_GlobalCoCurricularActivities];

ALTER TABLE [dbo].[Countries] WITH CHECK CHECK CONSTRAINT [FK_Countries_GlobalCountries];

ALTER TABLE [dbo].[DistrictOrStates] WITH CHECK CHECK CONSTRAINT [FK_DistrictOrStates_GlobalDistricts];

ALTER TABLE [dbo].[EducationalQualifications] WITH CHECK CHECK CONSTRAINT [FK_EducationalQualifications_GlobalEducationalQualifications];

ALTER TABLE [dbo].[Institutes] WITH CHECK CHECK CONSTRAINT [FK_Institutes_GlobalCountries];

ALTER TABLE [dbo].[Institutes] WITH CHECK CHECK CONSTRAINT [FK_Institutes_GlobalDistricts];

ALTER TABLE [dbo].[Institutes] WITH CHECK CHECK CONSTRAINT [FK_Institutes_GlobalDivisions];

ALTER TABLE [dbo].[Institutes] WITH CHECK CHECK CONSTRAINT [FK_Institutes_GlobalInstituteTypes];

ALTER TABLE [dbo].[Institutes] WITH CHECK CHECK CONSTRAINT [FK_Institutes_GlobalSubDistricts];

ALTER TABLE [dbo].[Professions] WITH CHECK CHECK CONSTRAINT [FK_Professions_GlobalProfessions];


GO
update dbo._Migration set LastUpdate='0020'
PRINT N'Update complete.';


GO

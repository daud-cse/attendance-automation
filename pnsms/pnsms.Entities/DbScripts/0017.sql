
USE PNSMS;


GO
PRINT N'Altering [dbo].[Notices]...';


GO
ALTER TABLE [dbo].[Notices]
    ADD [NoticeTypeId] INT NULL;


GO
PRINT N'Creating [dbo].[CoCurricularActivities]...';


GO
CREATE TABLE [dbo].[CoCurricularActivities] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT             NOT NULL,
    [Name]           NVARCHAR (256)  NOT NULL,
    [Description]    NVARCHAR (1024) NULL,
    [IsActive]       BIT             NOT NULL,
    [LastUpdateTime] DATETIME        NULL,
    CONSTRAINT [PK_CoCurricularActivities] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[CoCurricularActivitiesOfStudents]...';


GO
CREATE TABLE [dbo].[CoCurricularActivitiesOfStudents] (
    [Id]                     INT IDENTITY (1, 1) NOT NULL,
    [StudentId]              INT NOT NULL,
    [CoCurricularActivityId] INT NOT NULL,
    CONSTRAINT [PK_CoCurricularActivitiesOfStudents] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_CoCurricularActivities_Institutes]...';


GO
ALTER TABLE [dbo].[CoCurricularActivities] WITH NOCHECK
    ADD CONSTRAINT [FK_CoCurricularActivities_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_CoCurricularActivitiesOfStudents_Students]...';


GO
ALTER TABLE [dbo].[CoCurricularActivitiesOfStudents] WITH NOCHECK
    ADD CONSTRAINT [FK_CoCurricularActivitiesOfStudents_Students] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([StudentId]);


GO
PRINT N'Creating [dbo].[FK_CoCurricularActivitiesOfStudents_CoCurricularActivities]...';


GO
ALTER TABLE [dbo].[CoCurricularActivitiesOfStudents] WITH NOCHECK
    ADD CONSTRAINT [FK_CoCurricularActivitiesOfStudents_CoCurricularActivities] FOREIGN KEY ([CoCurricularActivityId]) REFERENCES [dbo].[CoCurricularActivities] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Notices_NoticeTypes]...';


GO
ALTER TABLE [dbo].[Notices] WITH NOCHECK
    ADD CONSTRAINT [FK_Notices_NoticeTypes] FOREIGN KEY ([NoticeTypeId]) REFERENCES [dbo].[NoticeTypes] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[CoCurricularActivities] WITH CHECK CHECK CONSTRAINT [FK_CoCurricularActivities_Institutes];

ALTER TABLE [dbo].[CoCurricularActivitiesOfStudents] WITH CHECK CHECK CONSTRAINT [FK_CoCurricularActivitiesOfStudents_Students];

ALTER TABLE [dbo].[CoCurricularActivitiesOfStudents] WITH CHECK CHECK CONSTRAINT [FK_CoCurricularActivitiesOfStudents_CoCurricularActivities];

ALTER TABLE [dbo].[Notices] WITH CHECK CHECK CONSTRAINT [FK_Notices_NoticeTypes];


GO
update dbo._Migration set LastUpdate='0017'
PRINT N'Update complete.';


GO

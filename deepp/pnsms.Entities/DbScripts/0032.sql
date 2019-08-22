
USE PNSMS;


GO
PRINT N'Creating [dbo].[ContentDetails]...';


GO
CREATE TABLE [dbo].[ContentDetails] (
    [Id]              INT IDENTITY (1, 1) NOT NULL,
    [ContentId]       INT NOT NULL,
    [AcademicClassId] INT NULL,
    [SubjectId]       INT NULL,
    CONSTRAINT [PK_ContentDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Contents]...';


GO
CREATE TABLE [dbo].[Contents] (
    [Id]             INT      IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT      NOT NULL,
    [TeacherId]      INT      NOT NULL,
    [IsActive]       BIT      NOT NULL,
    [LastUpdateTime] DATETIME NULL,
    CONSTRAINT [PK_Contents] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_ContentDetails_Contents]...';


GO
ALTER TABLE [dbo].[ContentDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_ContentDetails_Contents] FOREIGN KEY ([ContentId]) REFERENCES [dbo].[Contents] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ContentDetails_AcademicClasses]...';


GO
ALTER TABLE [dbo].[ContentDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_ContentDetails_AcademicClasses] FOREIGN KEY ([AcademicClassId]) REFERENCES [dbo].[AcademicClasses] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ContentDetails_Subject]...';


GO
ALTER TABLE [dbo].[ContentDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_ContentDetails_Subject] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subject] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Contents_Institutes]...';


GO
ALTER TABLE [dbo].[Contents] WITH NOCHECK
    ADD CONSTRAINT [FK_Contents_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Contents_Teachers]...';


GO
ALTER TABLE [dbo].[Contents] WITH NOCHECK
    ADD CONSTRAINT [FK_Contents_Teachers] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[Teachers] ([TeacherId]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[ContentDetails] WITH CHECK CHECK CONSTRAINT [FK_ContentDetails_Contents];

ALTER TABLE [dbo].[ContentDetails] WITH CHECK CHECK CONSTRAINT [FK_ContentDetails_AcademicClasses];

ALTER TABLE [dbo].[ContentDetails] WITH CHECK CHECK CONSTRAINT [FK_ContentDetails_Subject];

ALTER TABLE [dbo].[Contents] WITH CHECK CHECK CONSTRAINT [FK_Contents_Institutes];

ALTER TABLE [dbo].[Contents] WITH CHECK CHECK CONSTRAINT [FK_Contents_Teachers];


GO
update dbo._Migration set LastUpdate='0032'
PRINT N'Update complete.';


GO


USE PNSMS;


GO
/*
The column [dbo].[Notices].[AcademicBranchId] is being dropped, data loss could occur.

The column [dbo].[Notices].[AcademicClassId] is being dropped, data loss could occur.

The column [dbo].[Notices].[AcademicSectionId] is being dropped, data loss could occur.

The column [dbo].[Notices].[StudentId] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Notices])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_Notices_AcademicBranches]...';


GO
ALTER TABLE [dbo].[Notices] DROP CONSTRAINT [FK_Notices_AcademicBranches];


GO
PRINT N'Dropping [dbo].[FK_Notices_AcademicClasses]...';


GO
ALTER TABLE [dbo].[Notices] DROP CONSTRAINT [FK_Notices_AcademicClasses];


GO
PRINT N'Dropping [dbo].[FK_Notices_AcademicSections]...';


GO
ALTER TABLE [dbo].[Notices] DROP CONSTRAINT [FK_Notices_AcademicSections];


GO
PRINT N'Dropping [dbo].[FK_Notices_Students]...';


GO
ALTER TABLE [dbo].[Notices] DROP CONSTRAINT [FK_Notices_Students];


GO
PRINT N'Altering [dbo].[Notices]...';


GO
ALTER TABLE [dbo].[Notices] DROP COLUMN [AcademicBranchId], COLUMN [AcademicClassId], COLUMN [AcademicSectionId], COLUMN [StudentId];


GO
PRINT N'Creating [dbo].[Testimonials]...';


GO
CREATE TABLE [dbo].[Testimonials] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]     INT            NOT NULL,
    [TestimonialBody] NVARCHAR (MAX) NOT NULL,
    [TestimonialBy]   NVARCHAR (128) NOT NULL,
    [IsActive]        BIT            NOT NULL,
    [LastUpdateTime]  DATETIME       NULL,
    CONSTRAINT [PK_Testimonials] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_Testimonials_Institutes]...';


GO
ALTER TABLE [dbo].[Testimonials] WITH NOCHECK
    ADD CONSTRAINT [FK_Testimonials_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[Testimonials] WITH CHECK CHECK CONSTRAINT [FK_Testimonials_Institutes];


GO
PRINT N'Update complete.';


GO

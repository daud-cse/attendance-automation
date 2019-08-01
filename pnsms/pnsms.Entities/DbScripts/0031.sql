
USE PNSMS;


GO
PRINT N'Creating [dbo].[BuildingRooms]...';


GO
CREATE TABLE [dbo].[BuildingRooms] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [BuildingId]     INT             NOT NULL,
    [Name]           NVARCHAR (128)  NOT NULL,
    [Description]    NVARCHAR (1024) NULL,
    [IsActive]       BIT             NOT NULL,
    [LastUpdateTime] DATETIME        NULL,
    CONSTRAINT [PK_BuildingRooms] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Buildings]...';


GO
CREATE TABLE [dbo].[Buildings] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT             NOT NULL,
    [Name]           NVARCHAR (128)  NOT NULL,
    [Description]    NVARCHAR (1024) NULL,
    [IsActive]       BIT             NOT NULL,
    [LastUpdateTime] DATETIME        NULL,
    CONSTRAINT [PK_Buildings] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[RoutineDetails]...';


GO
CREATE TABLE [dbo].[RoutineDetails] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [RoutineId]       INT            NOT NULL,
    [WeekDayId]       INT            NOT NULL,
    [RoutinePeriodId] INT            NOT NULL,
    [SubjectId]       INT            NOT NULL,
    [TeacherId]       INT            NULL,
    [BuildingRoomId]  INT            NULL,
    [Remarks]         NVARCHAR (256) NULL,
    [IsActive]        BIT            NOT NULL,
    [LastUpdateTime]  DATETIME       NULL,
    CONSTRAINT [PK_RoutineDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[RoutinePeriods]...';


GO
CREATE TABLE [dbo].[RoutinePeriods] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT            NOT NULL,
    [Name]           NVARCHAR (128) NULL,
    [StartTime]      TIME (7)       NULL,
    [EndTime]        TIME (7)       NULL,
    [IsLeasure]      BIT            NOT NULL,
    [IsActive]       BIT            NOT NULL,
    [LastUpdateTime] DATETIME       NULL,
    CONSTRAINT [PK_RoutinePeriods] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Routines]...';


GO
CREATE TABLE [dbo].[Routines] (
    [Id]                INT      NOT NULL,
    [InstituteId]       INT      NOT NULL,
    [AcademicBranchId]  INT      NOT NULL,
    [AcademicClassId]   INT      NOT NULL,
    [AcademicShiftId]   INT      NULL,
    [AcademicSectionId] INT      NULL,
    [AcamedicGroupId]   INT      NULL,
    [IsActive]          BIT      NOT NULL,
    [LastUpdateTime]    DATETIME NULL,
    CONSTRAINT [PK_Routines] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[WeekDays]...';


GO
CREATE TABLE [dbo].[WeekDays] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [InstituteId] INT            NOT NULL,
    [DayOfWeek]   INT            NOT NULL,
    [Name]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_WeekDays] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_BuildingRooms_Buildings]...';


GO
ALTER TABLE [dbo].[BuildingRooms] WITH NOCHECK
    ADD CONSTRAINT [FK_BuildingRooms_Buildings] FOREIGN KEY ([BuildingId]) REFERENCES [dbo].[Buildings] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Buildings_Institutes]...';


GO
ALTER TABLE [dbo].[Buildings] WITH NOCHECK
    ADD CONSTRAINT [FK_Buildings_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_RoutineDetails_Routines]...';


GO
ALTER TABLE [dbo].[RoutineDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_RoutineDetails_Routines] FOREIGN KEY ([RoutineId]) REFERENCES [dbo].[Routines] ([Id]);


GO
PRINT N'Creating [dbo].[FK_RoutineDetails_RoutinePeriods]...';


GO
ALTER TABLE [dbo].[RoutineDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_RoutineDetails_RoutinePeriods] FOREIGN KEY ([RoutinePeriodId]) REFERENCES [dbo].[RoutinePeriods] ([Id]);


GO
PRINT N'Creating [dbo].[FK_RoutineDetails_BuildingRooms]...';


GO
ALTER TABLE [dbo].[RoutineDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_RoutineDetails_BuildingRooms] FOREIGN KEY ([BuildingRoomId]) REFERENCES [dbo].[BuildingRooms] ([Id]);


GO
PRINT N'Creating [dbo].[FK_RoutineDetails_WeekDays]...';


GO
ALTER TABLE [dbo].[RoutineDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_RoutineDetails_WeekDays] FOREIGN KEY ([WeekDayId]) REFERENCES [dbo].[WeekDays] ([Id]);


GO
PRINT N'Creating [dbo].[FK_RoutineDetails_Subject]...';


GO
ALTER TABLE [dbo].[RoutineDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_RoutineDetails_Subject] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subject] ([Id]);


GO
PRINT N'Creating [dbo].[FK_RoutineDetails_Teachers]...';


GO
ALTER TABLE [dbo].[RoutineDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_RoutineDetails_Teachers] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[Teachers] ([TeacherId]);


GO
PRINT N'Creating [dbo].[FK_RoutinePeriods_Institutes]...';


GO
ALTER TABLE [dbo].[RoutinePeriods] WITH NOCHECK
    ADD CONSTRAINT [FK_RoutinePeriods_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Routines_AcademicGroups]...';


GO
ALTER TABLE [dbo].[Routines] WITH NOCHECK
    ADD CONSTRAINT [FK_Routines_AcademicGroups] FOREIGN KEY ([AcamedicGroupId]) REFERENCES [dbo].[AcademicGroups] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Routines_Institutes]...';


GO
ALTER TABLE [dbo].[Routines] WITH NOCHECK
    ADD CONSTRAINT [FK_Routines_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Routines_AcademicBranches]...';


GO
ALTER TABLE [dbo].[Routines] WITH NOCHECK
    ADD CONSTRAINT [FK_Routines_AcademicBranches] FOREIGN KEY ([AcademicBranchId]) REFERENCES [dbo].[AcademicBranches] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Routines_AcademicClasses]...';


GO
ALTER TABLE [dbo].[Routines] WITH NOCHECK
    ADD CONSTRAINT [FK_Routines_AcademicClasses] FOREIGN KEY ([AcademicClassId]) REFERENCES [dbo].[AcademicClasses] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Routines_AcademicShifts]...';


GO
ALTER TABLE [dbo].[Routines] WITH NOCHECK
    ADD CONSTRAINT [FK_Routines_AcademicShifts] FOREIGN KEY ([AcademicShiftId]) REFERENCES [dbo].[AcademicShifts] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Routines_AcademicSections]...';


GO
ALTER TABLE [dbo].[Routines] WITH NOCHECK
    ADD CONSTRAINT [FK_Routines_AcademicSections] FOREIGN KEY ([AcademicSectionId]) REFERENCES [dbo].[AcademicSections] ([Id]);


GO
PRINT N'Creating [dbo].[FK_WeekDays_Institutes]...';


GO
ALTER TABLE [dbo].[WeekDays] WITH NOCHECK
    ADD CONSTRAINT [FK_WeekDays_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[BuildingRooms] WITH CHECK CHECK CONSTRAINT [FK_BuildingRooms_Buildings];

ALTER TABLE [dbo].[Buildings] WITH CHECK CHECK CONSTRAINT [FK_Buildings_Institutes];

ALTER TABLE [dbo].[RoutineDetails] WITH CHECK CHECK CONSTRAINT [FK_RoutineDetails_Routines];

ALTER TABLE [dbo].[RoutineDetails] WITH CHECK CHECK CONSTRAINT [FK_RoutineDetails_RoutinePeriods];

ALTER TABLE [dbo].[RoutineDetails] WITH CHECK CHECK CONSTRAINT [FK_RoutineDetails_BuildingRooms];

ALTER TABLE [dbo].[RoutineDetails] WITH CHECK CHECK CONSTRAINT [FK_RoutineDetails_WeekDays];

ALTER TABLE [dbo].[RoutineDetails] WITH CHECK CHECK CONSTRAINT [FK_RoutineDetails_Subject];

ALTER TABLE [dbo].[RoutineDetails] WITH CHECK CHECK CONSTRAINT [FK_RoutineDetails_Teachers];

ALTER TABLE [dbo].[RoutinePeriods] WITH CHECK CHECK CONSTRAINT [FK_RoutinePeriods_Institutes];

ALTER TABLE [dbo].[Routines] WITH CHECK CHECK CONSTRAINT [FK_Routines_AcademicGroups];

ALTER TABLE [dbo].[Routines] WITH CHECK CHECK CONSTRAINT [FK_Routines_Institutes];

ALTER TABLE [dbo].[Routines] WITH CHECK CHECK CONSTRAINT [FK_Routines_AcademicBranches];

ALTER TABLE [dbo].[Routines] WITH CHECK CHECK CONSTRAINT [FK_Routines_AcademicClasses];

ALTER TABLE [dbo].[Routines] WITH CHECK CHECK CONSTRAINT [FK_Routines_AcademicShifts];

ALTER TABLE [dbo].[Routines] WITH CHECK CHECK CONSTRAINT [FK_Routines_AcademicSections];

ALTER TABLE [dbo].[WeekDays] WITH CHECK CHECK CONSTRAINT [FK_WeekDays_Institutes];


GO
update dbo._Migration set LastUpdate='0031'
PRINT N'Update complete.';


GO

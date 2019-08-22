
USE PNSMS;


GO
PRINT N'Creating [dbo].[LibraryBookAuthores]...';


GO
CREATE TABLE [dbo].[LibraryBookAuthores] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT             NOT NULL,
    [Name]           NVARCHAR (1024) NOT NULL,
    [IsActive]       BIT             NOT NULL,
    [LastUpdateTime] DATETIME        NULL,
    CONSTRAINT [PK_LibraryBookAuthores] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[LibraryBookAuthorOfBooks]...';


GO
CREATE TABLE [dbo].[LibraryBookAuthorOfBooks] (
    [Id]                  INT IDENTITY (1, 1) NOT NULL,
    [LibraryBookId]       INT NOT NULL,
    [LibraryBookAuthorId] INT NOT NULL,
    CONSTRAINT [PK_LibraryBookAuthorOfBooks] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[LibraryBooks]...';


GO
CREATE TABLE [dbo].[LibraryBooks] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [InstituteId]    INT             NOT NULL,
    [Name]           NVARCHAR (1024) NOT NULL,
    [Quantity]       INT             NOT NULL,
    [IsActive]       BIT             NOT NULL,
    [LastUpdateTime] DATETIME        NULL,
    [ISBN]           NVARCHAR (1024) NULL,
    CONSTRAINT [PK_LibraryBooks] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_LibraryBookAuthores_Institutes]...';


GO
ALTER TABLE [dbo].[LibraryBookAuthores] WITH NOCHECK
    ADD CONSTRAINT [FK_LibraryBookAuthores_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_LibraryBookAuthorOfBooks_LibraryBooks]...';


GO
ALTER TABLE [dbo].[LibraryBookAuthorOfBooks] WITH NOCHECK
    ADD CONSTRAINT [FK_LibraryBookAuthorOfBooks_LibraryBooks] FOREIGN KEY ([LibraryBookId]) REFERENCES [dbo].[LibraryBooks] ([Id]);


GO
PRINT N'Creating [dbo].[FK_LibraryBookAuthorOfBooks_LibraryBookAuthores]...';


GO
ALTER TABLE [dbo].[LibraryBookAuthorOfBooks] WITH NOCHECK
    ADD CONSTRAINT [FK_LibraryBookAuthorOfBooks_LibraryBookAuthores] FOREIGN KEY ([LibraryBookAuthorId]) REFERENCES [dbo].[LibraryBookAuthores] ([Id]);


GO
PRINT N'Creating [dbo].[FK_LibraryBooks_Institutes]...';


GO
ALTER TABLE [dbo].[LibraryBooks] WITH NOCHECK
    ADD CONSTRAINT [FK_LibraryBooks_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[LibraryBookAuthores] WITH CHECK CHECK CONSTRAINT [FK_LibraryBookAuthores_Institutes];

ALTER TABLE [dbo].[LibraryBookAuthorOfBooks] WITH CHECK CHECK CONSTRAINT [FK_LibraryBookAuthorOfBooks_LibraryBooks];

ALTER TABLE [dbo].[LibraryBookAuthorOfBooks] WITH CHECK CHECK CONSTRAINT [FK_LibraryBookAuthorOfBooks_LibraryBookAuthores];

ALTER TABLE [dbo].[LibraryBooks] WITH CHECK CHECK CONSTRAINT [FK_LibraryBooks_Institutes];


GO
update dbo._Migration set LastUpdate='0044'
PRINT N'Update complete.';


GO

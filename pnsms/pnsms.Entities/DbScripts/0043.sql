
USE PNSMS;


GO
PRINT N'Creating [dbo].[ChartOfAccounts]...';


GO
CREATE TABLE [dbo].[ChartOfAccounts] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [ParentId]       INT            NULL,
    [InstituteId]    INT            NOT NULL,
    [Name]           NVARCHAR (256) NOT NULL,
    [Code]           NVARCHAR (50)  NULL,
    [IsAsset]        BIT            NULL,
    [IsLiabilities]  BIT            NULL,
    [IsIncome]       BIT            NULL,
    [IsExpense]      BIT            NULL,
    [IsCapital]      BIT            NULL,
    [IsActive]       BIT            NOT NULL,
    [LastUpdateTime] DATETIME       NULL,
    CONSTRAINT [PK_ChartOfAccounts] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[VoucherDetails]...';


GO
CREATE TABLE [dbo].[VoucherDetails] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [VoucherId]        INT             NOT NULL,
    [ChartOfAccountId] INT             NOT NULL,
    [Amount]           DECIMAL (18, 2) NOT NULL,
    [Narration]        NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_VoucherDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Vouchers]...';


GO
CREATE TABLE [dbo].[Vouchers] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [InstituteId]      INT             NOT NULL,
    [AcademicBranchId] INT             NOT NULL,
    [VoucherDate]      DATE            NOT NULL,
    [Narration]        NVARCHAR (MAX)  NULL,
    [TotalAmount]      DECIMAL (18, 2) NOT NULL,
    [IsActive]         BIT             NOT NULL,
    [LastUpdateTime]   DATETIME        NULL,
    [IsIncome]         BIT             NULL,
    [IsExpense]        BIT             NULL,
    CONSTRAINT [PK_Vouchers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_ChartOfAccounts_ChartOfAccounts]...';


GO
ALTER TABLE [dbo].[ChartOfAccounts] WITH NOCHECK
    ADD CONSTRAINT [FK_ChartOfAccounts_ChartOfAccounts] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[ChartOfAccounts] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ChartOfAccounts_Institutes]...';


GO
ALTER TABLE [dbo].[ChartOfAccounts] WITH NOCHECK
    ADD CONSTRAINT [FK_ChartOfAccounts_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_VoucherDetails_ChartOfAccounts]...';


GO
ALTER TABLE [dbo].[VoucherDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_VoucherDetails_ChartOfAccounts] FOREIGN KEY ([ChartOfAccountId]) REFERENCES [dbo].[ChartOfAccounts] ([Id]);


GO
PRINT N'Creating [dbo].[FK_VoucherDetails_Vouchers]...';


GO
ALTER TABLE [dbo].[VoucherDetails] WITH NOCHECK
    ADD CONSTRAINT [FK_VoucherDetails_Vouchers] FOREIGN KEY ([VoucherId]) REFERENCES [dbo].[Vouchers] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Vouchers_Institutes]...';


GO
ALTER TABLE [dbo].[Vouchers] WITH NOCHECK
    ADD CONSTRAINT [FK_Vouchers_Institutes] FOREIGN KEY ([InstituteId]) REFERENCES [dbo].[Institutes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Vouchers_AcademicBranches]...';


GO
ALTER TABLE [dbo].[Vouchers] WITH NOCHECK
    ADD CONSTRAINT [FK_Vouchers_AcademicBranches] FOREIGN KEY ([AcademicBranchId]) REFERENCES [dbo].[AcademicBranches] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE PNSMS;


GO
ALTER TABLE [dbo].[ChartOfAccounts] WITH CHECK CHECK CONSTRAINT [FK_ChartOfAccounts_ChartOfAccounts];

ALTER TABLE [dbo].[ChartOfAccounts] WITH CHECK CHECK CONSTRAINT [FK_ChartOfAccounts_Institutes];

ALTER TABLE [dbo].[VoucherDetails] WITH CHECK CHECK CONSTRAINT [FK_VoucherDetails_ChartOfAccounts];

ALTER TABLE [dbo].[VoucherDetails] WITH CHECK CHECK CONSTRAINT [FK_VoucherDetails_Vouchers];

ALTER TABLE [dbo].[Vouchers] WITH CHECK CHECK CONSTRAINT [FK_Vouchers_Institutes];

ALTER TABLE [dbo].[Vouchers] WITH CHECK CHECK CONSTRAINT [FK_Vouchers_AcademicBranches];


GO
update dbo._Migration set LastUpdate='0043'
PRINT N'Update complete.';


GO

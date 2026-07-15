CREATE TABLE [dbo].[Roles] (
    [RoleId]    INT           IDENTITY (1, 1) NOT NULL,
    [RoleName]  NVARCHAR (20) NOT NULL,
    [CreatedAt] DATETIME      DEFAULT (getdate()) NOT NULL,
    [IsDeleted] BIT           DEFAULT ((0)) NOT NULL,
    [UpdateAt]  DATETIME      NULL,
    [IsActive]  BIT           DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC),
    UNIQUE NONCLUSTERED ([RoleName] ASC)
);


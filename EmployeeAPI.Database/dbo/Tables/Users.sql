CREATE TABLE [dbo].[Users] (
    [UserId]       INT            IDENTITY (1, 1) NOT NULL,
    [FullName]     NVARCHAR (100) NOT NULL,
    [Email]        NVARCHAR (100) NOT NULL,
    [PasswordHash] NVARCHAR (MAX) NOT NULL,
    [RoleId]       INT            NOT NULL,
    [CreatedAt]    DATETIME       DEFAULT (getdate()) NOT NULL,
    [IsActive]     BIT            DEFAULT ((1)) NOT NULL,
    [IsFistLogin]  BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId]),
    UNIQUE NONCLUSTERED ([Email] ASC)
);


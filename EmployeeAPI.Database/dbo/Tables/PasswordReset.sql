CREATE TABLE [dbo].[PasswordReset] (
    [PasswordResetId] INT            IDENTITY (1, 1) NOT NULL,
    [UserId]          INT            NOT NULL,
    [Token]           NVARCHAR (500) NOT NULL,
    [ExpiryTime]      DATETIME2 (7)  NOT NULL,
    [IsUsed]          BIT            DEFAULT ((0)) NOT NULL,
    [CreatedOn]       DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL,
    [UsedOn]          DATETIME2 (7)  NULL,
    PRIMARY KEY CLUSTERED ([PasswordResetId] ASC),
    CONSTRAINT [FK_PasswordResetTokens_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PasswordResetTokens_Token]
    ON [dbo].[PasswordReset]([Token] ASC);


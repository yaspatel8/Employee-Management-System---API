CREATE TABLE [dbo].[Position] (
    [PositionId]   INT            IDENTITY (1, 1) NOT NULL,
    [PositionName] NVARCHAR (100) NOT NULL,
    [Level]        INT            NOT NULL,
    [IsActive]     BIT            DEFAULT ((1)) NOT NULL,
    [IsDeleted]    BIT            DEFAULT ((0)) NOT NULL,
    [CreatedBy]    INT            NOT NULL,
    [CreatedAt]    DATETIME       DEFAULT (getdate()) NOT NULL,
    [UpdatedBy]    INT            NULL,
    [UpdatedAt]    DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([PositionId] ASC),
    CONSTRAINT [UQ_Position_Level] UNIQUE NONCLUSTERED ([Level] ASC),
    CONSTRAINT [UQ_PositionName] UNIQUE NONCLUSTERED ([PositionName] ASC)
);


CREATE TABLE [dbo].[Department] (
    [DepartmentId]   INT            IDENTITY (1, 1) NOT NULL,
    [DepartmentName] NVARCHAR (100) NOT NULL,
    [CreatedAt]      DATETIME       DEFAULT (getdate()) NOT NULL,
    [IsDeleted]      BIT            DEFAULT ((0)) NOT NULL,
    [IsActive]       BIT            DEFAULT ((1)) NOT NULL,
    [UpdateAt]       DATETIME       NULL,
    [UpdatedBy]      INT            NULL,
    [CreatedBy]      INT            DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([DepartmentId] ASC),
    UNIQUE NONCLUSTERED ([DepartmentName] ASC)
);


CREATE TABLE [dbo].[Employee] (
    [EmployeeId]   INT             IDENTITY (1, 1) NOT NULL,
    [UserId]       INT             NOT NULL,
    [PhoneNumber]  NVARCHAR (20)   NULL,
    [Salary]       DECIMAL (18, 2) NULL,
    [CreatedAt]    DATETIME        DEFAULT (getdate()) NOT NULL,
    [IsDeleted]    BIT             DEFAULT ((0)) NOT NULL,
    [IsActive]     BIT             DEFAULT ((1)) NOT NULL,
    [UpdatedAt]    DATETIME        NULL,
    [ProfileImage] NVARCHAR (MAX)  NULL,
    [UpdatedBy]    INT             NULL,
    [CreatedBy]    INT             NULL,
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC),
    CONSTRAINT [fk_user] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Employee_PhoneNumber]
    ON [dbo].[Employee]([PhoneNumber] ASC) WHERE ([PhoneNumber] IS NOT NULL);


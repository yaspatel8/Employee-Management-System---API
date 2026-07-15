CREATE TABLE [dbo].[EmployeePosition] (
    [EmployeePositionId]          INT      IDENTITY (1, 1) NOT NULL,
    [EmployeeId]                  INT      NOT NULL,
    [DepartmentId]                INT      NULL,
    [PositionId]                  INT      NULL,
    [ReportsToEmployeePositionId] INT      NULL,
    [StartDate]                   DATE     DEFAULT (getdate()) NULL,
    [EndDate]                     DATE     NULL,
    [IsActive]                    BIT      DEFAULT ((1)) NOT NULL,
    [CreatedAt]                   DATETIME DEFAULT (getdate()) NULL,
    [UpdatedAt]                   DATETIME NULL,
    [createdBy]                   INT      NULL,
    [updatedBy]                   INT      NULL,
    PRIMARY KEY CLUSTERED ([EmployeePositionId] ASC),
    CONSTRAINT [FK_EP_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([DepartmentId]),
    CONSTRAINT [FK_EP_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    CONSTRAINT [FK_EP_Position] FOREIGN KEY ([PositionId]) REFERENCES [dbo].[Position] ([PositionId]),
    CONSTRAINT [FK_EP_Reporting] FOREIGN KEY ([ReportsToEmployeePositionId]) REFERENCES [dbo].[EmployeePosition] ([EmployeePositionId])
);


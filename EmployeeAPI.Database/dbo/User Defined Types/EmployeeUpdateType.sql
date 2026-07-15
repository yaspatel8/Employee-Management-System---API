CREATE TYPE [dbo].[EmployeeUpdateType] AS TABLE (
    [EmployeeId]   INT             NOT NULL,
    [FullName]     NVARCHAR (100)  NULL,
    [Email]        NVARCHAR (100)  NULL,
    [Salary]       DECIMAL (18, 2) NULL,
    [DepartmentId] INT             NULL,
    [IsActive]     BIT             NULL);


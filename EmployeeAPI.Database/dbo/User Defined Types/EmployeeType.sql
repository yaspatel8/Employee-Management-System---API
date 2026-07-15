CREATE TYPE [dbo].[EmployeeType] AS TABLE (
    [FullName]     NVARCHAR (100)  NOT NULL,
    [Email]        NVARCHAR (100)  NOT NULL,
    [PasswordHash] NVARCHAR (MAX)  NOT NULL,
    [Salary]       DECIMAL (18, 2) NULL,
    [DepartmentId] INT             NULL);


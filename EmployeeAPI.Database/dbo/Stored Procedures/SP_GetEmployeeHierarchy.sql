CREATE   PROCEDURE SP_GetEmployeeHierarchy
    @EmployeePositionId INT,
    @IncludeSelf BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH Hierarchy AS
    (
        -- Root Employee
        SELECT
            EP.EmployeePositionId,
            EP.EmployeeId,
            EP.DepartmentId,
            EP.PositionId,
            EP.ReportsToEmployeePositionId,
            0 AS HierarchyLevel
        FROM EmployeePosition EP
        WHERE EP.EmployeePositionId = @EmployeePositionId
          AND EP.IsActive = 1

        UNION ALL

        -- Child Employees
        SELECT
            C.EmployeePositionId,
            C.EmployeeId,
            C.DepartmentId,
            C.PositionId,
            C.ReportsToEmployeePositionId,
            H.HierarchyLevel + 1
        FROM EmployeePosition C
        INNER JOIN Hierarchy H
            ON C.ReportsToEmployeePositionId = H.EmployeePositionId
        WHERE C.IsActive = 1
    )

    SELECT
        H.EmployeePositionId,
        H.EmployeeId,
        U.FullName,
        D.DepartmentName,
        P.PositionName,
        H.HierarchyLevel
    FROM Hierarchy H
    INNER JOIN Employee E
        ON H.EmployeeId = E.EmployeeId
    INNER JOIN Users U
        ON E.UserId = U.UserId
    INNER JOIN Department D
        ON H.DepartmentId = D.DepartmentId
    INNER JOIN Position P
        ON H.PositionId = P.PositionId
    ORDER BY H.HierarchyLevel, P.Level;
END
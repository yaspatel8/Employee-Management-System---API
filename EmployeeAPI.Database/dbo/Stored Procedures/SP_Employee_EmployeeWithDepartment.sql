CREATE   PROCEDURE SP_Employee_EmployeeWithDepartment
AS
BEGIN

    SELECT
        E.EmployeeId,
        U.FullName,
        U.Email,
        E.PhoneNumber,
        E.Salary,
        STRING_AGG(P.PositionName, ', ') AS positionName,
        STRING_AGG(D.DepartmentName, ', ') AS departmentName,
        U.IsActive
    FROM Employee E
    RIGHT JOIN Users U
        ON E.UserId = U.UserId
    LEFT JOIN EmployeePosition EP
        ON E.EmployeeId = EP.EmployeeId AND EP.IsActive = 1
    LEFT JOIN Department D
        ON EP.DepartmentId = D.DepartmentId
    LEFT JOIN Position P
        ON EP.PositionId = P.PositionId
    WHERE E.IsDeleted = 0
    GROUP BY
    E.EmployeeId,
    U.FullName,
    U.Email,
    E.PhoneNumber,
    E.Salary,
    U.IsActive

END
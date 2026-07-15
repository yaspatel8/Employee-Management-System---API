
CREATE   PROCEDURE SP_ExportEmployees
(
    @EmployeeIds dbo.IdListType READONLY
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        E.EmployeeId,
        U.FullName,
        U.Email,
        COALESCE(E.Salary, 0) AS Salary,
        COALESCE(E.PhoneNumber, 'Not Available') AS PhoneNumber,
        CoALESCE(D.DepartmentName, '-- Not Assigned --') AS DepartmentName,
        CoALESCE(P.PositionName, '-- Not Assigned --') AS PositionName,
        CASE WHEN E.IsActive = 1 THEN 'Yes' ELSE 'No' END AS IsActive,
        CASE WHEN E.IsDeleted = 1 THEN 'Yes' ELSE 'No' END AS IsDeleted,
        E.CreatedAt AS DateOfJoining,
        E.UpdatedAt
      
    FROM Employee E
    INNER JOIN Users U
        ON E.UserId = U.UserId
    LEFT JOIN EmployeePosition EP
        ON E.EmployeeId = EP.EmployeeId AND EP.IsActive = 1
    LEFT JOIN Department D
        ON EP.DepartmentId = D.DepartmentId
    LEFT JOIN Position P
        ON EP.PositionId = P.PositionId
    WHERE
        NOT EXISTS (SELECT 1 FROM @EmployeeIds)
        OR E.EmployeeId IN (SELECT Id FROM @EmployeeIds)
    ORDER BY E.EmployeeId;

    IF @@ROWCOUNT = 0
    BEGIN
        SELECT 0 AS Code,
               'No employees found.' AS Message;
    END
    ELSE
    BEGIN
        SELECT 1 AS Code,
               'Employees exported successfully.' AS Message;
    END
END
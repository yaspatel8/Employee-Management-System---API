CREATE   PROCEDURE SP_GetHierarchyTree
    @DepartmentId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @DepartmentId = 0
        SET @DepartmentId = NULL;

    SELECT
        EP.EmployeePositionId,
        EP.ReportsToEmployeePositionId,

        E.EmployeeId,

        U.FullName,

        EP.DepartmentId,
        D.DepartmentName,

        EP.PositionId,
        P.PositionName,
        P.Level,

        E.ProfileImage

    FROM EmployeePosition EP

    INNER JOIN Employee E
        ON EP.EmployeeId = E.EmployeeId

    INNER JOIN Users U
        ON E.UserId = U.UserId

    LEFT JOIN Department D
        ON EP.DepartmentId = D.DepartmentId

    LEFT JOIN Position P
        ON EP.PositionId = P.PositionId

    WHERE
        EP.IsActive = 1
        AND E.IsActive = 1
        AND E.IsDeleted = 0
        AND (@DepartmentId IS NULL OR EP.DepartmentId = @DepartmentId)

    ORDER BY
        P.Level,
        U.FullName;
END;
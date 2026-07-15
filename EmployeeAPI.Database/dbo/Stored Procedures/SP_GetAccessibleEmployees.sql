 
CREATE   PROCEDURE SP_GetAccessibleEmployees 
( 
    @UserId INT,
    @IncludeSelf BIT = 1
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE
        @EmployeeId INT,
        @EmployeePositionId INT,
        @RoleName NVARCHAR(100);

    -------------------------------------------------------
    -- Get Login User Details
    -------------------------------------------------------
    SELECT
        @EmployeeId = E.EmployeeId,
        @RoleName = R.RoleName
    FROM Users U
    INNER JOIN Employee E
        ON U.UserId = E.UserId
    INNER JOIN Roles R
        ON U.RoleId = R.RoleId
    WHERE U.UserId = @UserId;

    -------------------------------------------------------
    -- Get Primary Position
    -------------------------------------------------------
    SELECT TOP (1)
        @EmployeePositionId = EmployeePositionId
    FROM EmployeePosition
    WHERE EmployeeId = @EmployeeId
      AND IsActive = 1
    ORDER BY StartDate DESC;

    -------------------------------------------------------
    -- Admin / CEO
    -------------------------------------------------------
    IF (@RoleName = 'admin')
    BEGIN

       SELECT DISTINCT
        E.EmployeeId
        FROM Employee E
        INNER JOIN Users U
            ON E.UserId = U.UserId
        INNER JOIN EmployeePosition EP
            ON E.EmployeeId = EP.EmployeeId
            AND EP.IsActive = 1
        --INNER JOIN Department D
        --    ON EP.DepartmentId = D.DepartmentId
        --LEFT JOIN Position P
        --    ON EP.PositionId = P.PositionId
        WHERE
            E.IsActive = 1
            AND E.IsDeleted = 0;

        RETURN;
    END

    -------------------------------------------------------
    -- Hierarchy
    -------------------------------------------------------
    ;WITH Hierarchy AS
    (
        SELECT
            EmployeePositionId,
            EmployeeId,
            ReportsToEmployeePositionId
        FROM EmployeePosition
        WHERE EmployeePositionId = @EmployeePositionId

        UNION ALL

        SELECT
            EP.EmployeePositionId,
            EP.EmployeeId,
            EP.ReportsToEmployeePositionId
        FROM EmployeePosition EP
        INNER JOIN Hierarchy H
            ON EP.ReportsToEmployeePositionId = H.EmployeePositionId
        WHERE EP.IsActive = 1
    )

    SELECT DISTINCT
        H.EmployeeId
    FROM Hierarchy H
    INNER JOIN Employee E
        ON H.EmployeeId = E.EmployeeId
    INNER JOIN Users U
        ON E.UserId = U.UserId
    INNER JOIN EmployeePosition EP
        ON H.EmployeePositionId = EP.EmployeePositionId
    INNER JOIN Department D
        ON EP.DepartmentId = D.DepartmentId
    INNER JOIN Position P
        ON EP.PositionId = P.PositionId
    WHERE
        (@IncludeSelf = 1
         OR H.EmployeePositionId <> @EmployeePositionId)
        AND E.IsActive = 1
        AND E.IsDeleted = 0
    ORDER BY
          H.EmployeeId;

END

CREATE   PROCEDURE SP_GetManagers (
    @DepartmentId INT,
    @PositionId INT,
    @EmployeeId INT = NULL 
)
AS
BEGIN
    SET NOCOUNT ON;
        
    DECLARE @Level INT;

    SELECT @Level = Level
    FROM Position
    WHERE PositionId = @PositionId;

    SELECT
        EP.EmployeePositionId, -- Manager Position Id
        E.EmployeeId,
        U.FullName,
        U.Email,
        P.PositionName,
        D.DepartmentName
    FROM EmployeePosition EP

    INNER JOIN Employee E
        ON EP.EmployeeId = E.EmployeeId

    INNER JOIN Users U
        ON E.UserId = U.UserId

    INNER JOIN Position P
        ON EP.PositionId = P.PositionId

    LEFT JOIN Department D
        ON EP.DepartmentId = D.DepartmentId

    INNER JOIN Roles R
        ON U.RoleId = R.RoleId

    WHERE
        EP.IsActive = 1
        AND E.IsActive = 1
        AND E.IsDeleted = 0
        AND E.EmployeeId <> ISNULL(@EmployeeId, 0)
        AND (
           
            (
                @Level = 2
                AND R.RoleName = 'admin'
            )

           
            OR (
                @Level = 3
                AND (
                    (EP.DepartmentId = @DepartmentId AND P.Level = 2) 
                    OR R.RoleName = 'admin'
                )
            )

            
            OR (
                @Level = 4
                AND (
                    (EP.DepartmentId = @DepartmentId AND P.Level = 3)
                    OR R.RoleName = 'admin' 
                )
            )

            
            OR (
                @Level >= 5
                AND EP.DepartmentId = @DepartmentId
                AND P.Level IN (2, 3, 4)
               
            )
        )

    ORDER BY
        P.Level,
        U.FullName;
END
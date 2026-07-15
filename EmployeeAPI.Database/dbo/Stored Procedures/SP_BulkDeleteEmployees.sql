
--delete bulk employee
CREATE   PROCEDURE SP_BulkDeleteEmployees
@EmployeeIds dbo.EmployeeDeleteType READONLY, @DeletedBy INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        UPDATE E
        SET    E.IsDeleted = 1,
               E.IsActive = 0,
               E.UpdatedAt = GETDATE(),
               E.UpdatedBy = @DeletedBy
        FROM   Employee AS E
               INNER JOIN
               @EmployeeIds AS EI
               ON E.EmployeeId = EI.EmployeeId
        WHERE  E.IsDeleted = 0
               AND E.IsActive = 1;

        UPDATE U 
        SET    U.IsActive = 0
               FROM Users AS U
               INNER JOIN
               @EmployeeIds AS EI
               ON U.UserId = (SELECT E.UserId FROM Employee AS E WHERE E.EmployeeId = EI.EmployeeId)
               WHERE  U.IsActive = 1;

        IF @@ROWCOUNT > 0
            BEGIN
                SELECT 1 AS Code,
                       CONCAT('employees deleted successfully by User ID: ', @DeletedBy) AS Message;
            END
        ELSE
            BEGIN
                SELECT 0 AS Code,
                       'No employees were deleted.' AS Message;
            END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        SELECT -1 AS Code,
               ERROR_MESSAGE() AS Message;
    END CATCH
END
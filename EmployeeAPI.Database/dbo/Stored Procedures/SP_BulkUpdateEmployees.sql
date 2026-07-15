
CREATE   PROCEDURE SP_BulkUpdateEmployees
@Employees dbo.EmployeeUpdateType READONLY, @UpdatedBy INT
AS
BEGIN
    --CHECK FOR DUPLICATE EMAILS IN THE PROVIDED DATA
    DECLARE @DuplicateEmails AS NVARCHAR (MAX);
    SELECT @DuplicateEmails = STRING_AGG(E.Email, ', ')
    FROM   @Employees AS E
           INNER JOIN
           Users AS U
           ON E.Email = U.Email
    WHERE  E.EmployeeId <> (SELECT EmployeeId
                            FROM   Employee
                            WHERE  UserId = U.UserId);
    BEGIN TRY
        BEGIN TRANSACTION;
        UPDATE E
        SET    E.Salary       = COALESCE (EU.Salary, E.Salary),
               E.UpdatedAt    = GETDATE(),
               E.IsActive     = COALESCE (EU.IsActive, E.IsActive),
               E.UpdatedBy    = @UpdatedBy
        FROM   Employee AS E
               INNER JOIN
               @Employees AS EU
               ON E.EmployeeId = EU.EmployeeId;
        -- Update Department in EmployeePosition
        UPDATE EP
        SET
            EP.DepartmentId = COALESCE(EU.DepartmentId, EP.DepartmentId),
            EP.UpdatedAt = GETDATE(),
            EP.UpdatedBy = @UpdatedBy
        FROM EmployeePosition EP
        INNER JOIN @Employees EU
            ON EP.EmployeeId = EU.EmployeeId
        WHERE EP.IsActive = 1;

        UPDATE U
        SET    U.FullName = COALESCE (EU.FullName, U.FullName),
               U.Email    = COALESCE (EU.Email, U.Email),
               U.IsActive = COALESCE (EU.IsActive, U.IsActive)
        FROM   Users AS U
               INNER JOIN
               @Employees AS EU
               ON U.UserId = (SELECT E.UserId
                              FROM   Employee AS E
                              WHERE  E.EmployeeId = EU.EmployeeId);
        DECLARE @TotalCount AS INT = (SELECT COUNT(*)
                                      FROM   @Employees);
        DECLARE @UpdatedCount AS INT = (SELECT COUNT(*)
                                       FROM   Employee AS E
                                              INNER JOIN
                                              @Employees AS EU
                                              ON E.EmployeeId = EU.EmployeeId);
        DECLARE @SkippedCount AS INT = @TotalCount - @UpdatedCount;

        IF (@UpdatedCount > 0)
            BEGIN
                SELECT 1 AS Code,
                       @UpdatedCount AS UpdatedCount,
                       @SkippedCount AS SkippedCount,
                       ISNULL(@DuplicateEmails, '') AS DuplicateEmails,
                       CONCAT(@UpdatedCount, ' of ', @TotalCount, ' employees updated successfully.', CASE WHEN @SkippedCount > 0 THEN ' Skipped ' + CAST (@SkippedCount AS NVARCHAR (10)) + ' duplicate emails: ' + @DuplicateEmails ELSE '' END) AS Message;
            END
        ELSE
            BEGIN
                SELECT -1 AS Code,
                       0 AS UpdatedCount,
                       @TotalCount AS SkippedCount,
                       ISNULL(@DuplicateEmails, '') AS DuplicateEmails,
                       'No employees were updated. All provided emails already exist.' AS Message;
            END;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        SELECT -1 AS Code,
               ERROR_MESSAGE() AS Message;
    END CATCH
END
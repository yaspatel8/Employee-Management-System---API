
CREATE   PROCEDURE SP_BulkSaveEmployees
@Employees dbo.EmployeeType READONLY, @CreatedBy INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @RoleId AS INT;
    DECLARE @InsertedUsers TABLE (
        UserId   INT           ,
        Email    NVARCHAR (100),
        FullName NVARCHAR (100)); -- Check Duplicate Emails
    DECLARE @DuplicateEmails AS NVARCHAR (MAX);
    SELECT @DuplicateEmails = STRING_AGG(E.Email, ', ')
    FROM   @Employees AS E
           INNER JOIN
           Users AS U
           ON E.Email = U.Email; -- Employee Role
    SELECT @RoleId = RoleId
    FROM   Roles
    WHERE  LOWER(RoleName) = 'employee'; -- Insert Users
    BEGIN TRY
        BEGIN TRANSACTION;
        INSERT INTO Users (FullName, Email, PasswordHash, RoleId,IsActive)
        OUTPUT INSERTED.UserId, INSERTED.Email, INSERTED.FullName INTO @InsertedUsers (UserId, Email, FullName)
        SELECT E.FullName,
               E.Email,
               E.PasswordHash,
               @RoleId,
               1
        FROM   @Employees AS E
        WHERE  NOT EXISTS (SELECT 1
                           FROM   Users AS U
                           WHERE  U.Email = E.Email); -- Insert Employee
        INSERT INTO Employee (UserId, Salary, CreatedBy)
        SELECT U.UserId,
               E.Salary,
               @CreatedBy
        FROM   @InsertedUsers AS U
               INNER JOIN
               @Employees AS E
               ON U.Email = E.Email; -- Save
        DECLARE @TotalCount AS INT = (SELECT COUNT(*)
                                      FROM   @Employees);
        DECLARE @InsertedCount AS INT = (SELECT COUNT(*)
                                         FROM   @InsertedUsers);
        DECLARE @SkippedCount AS INT = @TotalCount - @InsertedCount;

        INSERT INTO EmployeePosition
        (
            EmployeeId,
            DepartmentId,
            StartDate,
            IsActive,
            CreatedBy,
            CreatedAt
        )
        SELECT
            E.EmployeeId,
            T.DepartmentId,
            GETDATE(),
            1,
            @CreatedBy,
            GETDATE()
        FROM Employee E
        INNER JOIN @InsertedUsers U
            ON E.UserId = U.UserId
        INNER JOIN @Employees T
            ON U.Email = T.Email;
        IF (@InsertedCount > 0)
            BEGIN
                SELECT 1 AS Code,
                       @InsertedCount AS InsertedCount,
                       @SkippedCount AS SkippedCount,
                       ISNULL(@DuplicateEmails, '') AS DuplicateEmails,
                       CONCAT(@InsertedCount, ' of ', @TotalCount, ' employees added successfully.', CASE WHEN @SkippedCount > 0 THEN ' Skipped ' + CAST (@SkippedCount AS NVARCHAR (10)) + ' duplicate employees: ' + @DuplicateEmails ELSE '' END) AS Message;
            END
        ELSE
            BEGIN
                SELECT -1 AS Code,
                       0 AS InsertedCount,
                       @TotalCount AS SkippedCount,
                       ISNULL(@DuplicateEmails, '') AS DuplicateEmails,
                       'No employees were added. All provided emails already exist.' AS Message;
            END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        SELECT -1 AS Code,
               ERROR_MESSAGE() AS Message;
    END CATCH
END
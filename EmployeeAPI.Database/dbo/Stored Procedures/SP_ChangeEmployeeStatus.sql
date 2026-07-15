
--change isactive status of employee
CREATE   PROCEDURE SP_ChangeEmployeeStatus
    @EmployeeId INT,
    @IsActive BIT,
    @UpdatedBy INT
AS
BEGIN
    UPDATE E
    SET    E.IsActive = @IsActive,
           E.UpdatedAt = GETDATE(),
           E.UpdatedBy = @UpdatedBy
    FROM   Employee AS E
    WHERE  E.EmployeeId = @EmployeeId

    
    UPDATE U 
    SET    U.IsActive = @IsActive
    FROM Users AS U 
    WHERE U.UserId = (SELECT E.UserId FROM Employee AS E WHERE E.EmployeeId = @EmployeeId);

     
    IF @@ROWCOUNT > 0
        BEGIN
            SELECT 1 AS Code,
                   CONCAT('Employee status changed successfully by User ID: ', @UpdatedBy) AS Message;
        END
    ELSE
        BEGIN
            SELECT 0 AS Code,
                   'No employee status was changed.' AS Message;
        END
END
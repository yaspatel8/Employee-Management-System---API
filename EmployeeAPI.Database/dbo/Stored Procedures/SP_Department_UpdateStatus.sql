CREATE   PROCEDURE SP_Department_UpdateStatus
(
    @DepartmentId INT,
    @IsActive BIT,
    @UpdatedBy INT
)
AS
BEGIN
    UPDATE Department
    SET IsActive = @IsActive,
        UpdateAt = GETDATE(),
        UpdatedBy = @UpdatedBy
    WHERE DepartmentId = @DepartmentId;
    IF (@@ROWCOUNT > 0)
    BEGIN
        SELECT 1 AS Code, CONCAT('Department status updated successfully by User ID: ', @UpdatedBy) AS [Message];
    END
    ELSE 
    BEGIN
        SELECT 0 AS Code, 'Department status update failed' AS [Message];
    END
END
CREATE   PROCEDURE SP_Position_UpdateStatus
(
    @PositionId INT,
    @IsActive BIT,
    @UpdatedBy INT
)
AS
BEGIN
    UPDATE Position
    SET IsActive = @IsActive,
        UpdatedAt = GETDATE(),
        UpdatedBy = @UpdatedBy
    WHERE PositionId = @PositionId;
    IF (@@ROWCOUNT > 0)
    BEGIN
        SELECT 1 AS Code, CONCAT('Position status updated successfully by User ID: ', @UpdatedBy) AS [Message];
    END
    ELSE 
    BEGIN
        SELECT 0 AS Code, 'Position status update failed' AS [Message];
    END
END
CREATE   PROCEDURE SP_DeletePosition
(
    @PositionId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF EXISTS
        (
            SELECT 1
            FROM Employee
            WHERE PositionId = @PositionId
              AND IsDeleted = 0
              AND IsActive = 1
        )
        BEGIN
            ROLLBACK TRANSACTION;

            SELECT -1 AS Code,
                   'This position is assigned to employees and cannot be deleted.' AS Message;
            RETURN;
        END

        UPDATE Position
        SET IsDeleted = 1
        WHERE PositionId = @PositionId
          AND IsDeleted = 0;

        IF @@ROWCOUNT > 0
        BEGIN
            COMMIT TRANSACTION;

            SELECT 1 AS Code,
                   'Position deleted successfully.' AS Message;
        END
        ELSE
        BEGIN
            ROLLBACK TRANSACTION;

            SELECT 0 AS Code,
                   'Position not found.' AS Message;
        END
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        SELECT -99 AS Code,
               ERROR_MESSAGE() AS Message;
    END CATCH
END
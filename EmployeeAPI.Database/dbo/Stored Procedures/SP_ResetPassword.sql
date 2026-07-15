

CREATE   PROCEDURE SP_ResetPassword
(
    @Token NVARCHAR(500),
    @PasswordHash NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserId INT;

    SELECT @UserId = UserId
    FROM PasswordReset
    WHERE Token = @Token
      AND IsUsed = 0
      AND ExpiryTime > GETUTCDATE();

    IF @UserId IS NULL
    BEGIN
        SELECT
            -1 AS Code,
            'Invalid, expired or already Changed Reset Password.' AS Message;
        RETURN;
    END

    BEGIN TRY
        BEGIN TRAN;

        -- Update password
        UPDATE Users
        SET PasswordHash = @PasswordHash,IsFistLogin=0
        WHERE UserId = @UserId;

        -- Mark token as used
        UPDATE PasswordReset
        SET
            IsUsed = 1,
            UsedOn = GETUTCDATE()
        WHERE Token = @Token;

        COMMIT TRAN;

        SELECT
            1 AS Code,
            'Password reset successfully.' AS Message;
    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        SELECT
            -1 AS Code,
            ERROR_MESSAGE() AS Message;
    END CATCH
END;
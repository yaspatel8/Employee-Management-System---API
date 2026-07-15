
CREATE   PROCEDURE SP_Login (
    @Email NVARCHAR(100)
)
AS
BEGIN

    SELECT
        U.UserId,
        U.FullName,
        U.Email,
        U.PasswordHash,
        U.RoleId,
        R.RoleName,
		U.IsFistLogin
    FROM Users U
    INNER JOIN Roles R
        ON U.RoleId = R.RoleId
    WHERE U.Email = LOWER(TRIM(@Email)) AND U.IsActive=1

	
END
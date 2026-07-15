
CREATE   PROCEDURE SP_Register
(
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100),
    @PasswordHash NVARCHAR(MAX)
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @RoleId INT;
	DECLARE @UserId INT;
	DECLARE @EmployeeId INT;
    
	IF EXISTS(SELECT Email FROM Users WHERE Email = LOWER(TRIM(@Email)))
		BEGIN
			SELECT -1 AS Code , 'Email already exists' AS [Message]
		    RETURN;
		END

		SELECT @RoleId = RoleId FROM Roles WHERE RoleName = LOWER(TRIM('employee'));

		INSERT INTO Users
		(
		    FullName,
		    Email,
		    PasswordHash,
		    RoleId,
			IsFistLogin
		)
		VALUES
		(
		   LOWER(@FullName),
		   LOWER(@Email),
		   @PasswordHash,
		   @RoleId,0
		)

		SET @UserId = SCOPE_IDENTITY();

		INSERT INTO Employee
		(
		    UserId
		)
		VALUES
		(
		    @UserId
		);

		SET @EmployeeId = SCOPE_IDENTITY();

		INSERT INTO EmployeePosition
		(
			EmployeeId
			
		)
		VALUES
		(
			@EmployeeId	
		);
		

		SELECT 1 AS Code , 'User registered successfully' AS [Message]
END
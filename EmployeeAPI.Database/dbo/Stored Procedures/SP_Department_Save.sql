CREATE   PROCEDURE SP_Department_Save
(
    @DepartmentId INT = NULL,
    @DepartmentName NVARCHAR(100),
	@UpdatedBy INT = NULL, 
	@CreatedBy INT = NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(SELECT DepartmentName FROM Department WHERE DepartmentName=LOWER(TRIM(@DepartmentName)))
		BEGIN
			SELECT -1 AS Code , 'Department already exists' AS [Message]
			RETURN;
		END

    IF @DepartmentId IS NULL OR @DepartmentId = 0
		BEGIN	
			INSERT INTO Department ( DepartmentName,CreatedBy) VALUES ( LOWER(TRIM(@DepartmentName)),  @CreatedBy);
			IF(@@ROWCOUNT > 0)
				BEGIN
					SELECT 1 AS Code , CONCAT('Department Inserted successfully by User ID: ', @CreatedBy) AS [Message]
				ENd
			ELSE 
				BEGIN
					SELECT 0 AS Code , 'Department Insertion Failed' AS [Message]
				END
		END
    ELSE
		BEGIN
			UPDATE Department SET DepartmentName = LOWER(@DepartmentName), UpdateAt = GETDATE(),Updatedby=@UpdatedBy WHERE DepartmentId = @DepartmentId;
			IF(@@ROWCOUNT > 0)
				BEGIN
					SELECT 1 AS Code ,  CONCAT('Department Updated successfully by User ID: ', @UpdatedBy) AS [Message]
				
				ENd
			ELSE 
				BEGIN
					SELECT 0 AS Code , 'Department Updation Failed' AS [Message]
				ENd
		END
	END
CREATE   PROCEDURE SP_Department_Delete
@DepartmentId INT
AS
BEGIN
	UPDATE Department SET IsDeleted=1 WHERE DepartmentId=@DepartmentId
	IF(@@ROWCOUNT > 0)
		BEGIN
			SELECT 1 AS Code , 'Department Deleted successfully' AS [Message]
		
		ENd
	ELSE 
		BEGIN
			SELECT 0 AS Code , 'Department Deleted Failed' AS [Message]
		ENd
END
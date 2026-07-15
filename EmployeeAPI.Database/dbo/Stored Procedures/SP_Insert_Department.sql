CREATE   PROCEDURE SP_Insert_Department
@DepartmentName NVARCHAR(100)
AS
BEGIN
	INSERT INTO Department(DepartmentName) VALUES(LOWER(@DepartmentName))
END
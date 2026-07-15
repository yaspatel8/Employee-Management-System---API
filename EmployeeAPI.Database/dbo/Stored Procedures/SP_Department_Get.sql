CREATE   PROCEDURE SP_Department_Get
AS
BEGIN
	SELECT DepartmentId,DepartmentName FROM Department WHERE IsDeleted=0 AND IsActive=1
END
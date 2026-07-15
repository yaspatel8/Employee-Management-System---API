
CREATE   PROCEDURE SP_Employee_Get
@EmployeeId INT
AS
BEGIN
	SELECT * FROM Employee WHERE EmployeeId=@EmployeeId
END
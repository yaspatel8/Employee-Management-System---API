CREATE   PROCEDURE SP_Employee_Insert
@EmployeeName NVARCHAR(200),@EmployeeEmail NVARCHAR(250),@PhoneNumber NVARCHAR(20),@Salary DECIMAL(18,2),@DepartmentId INT
AS
BEGIN
	DECLARE @RoleId INT = 0;
	SET @RoleId=(SELECT RoleId FROM Roles WHERE RoleName=LOWER('employee'))
	
	INSERT INTO Employee(EmployeeName,EmployeeEmail,PhoneNumber,Salary,DepartmentId,RoleId) 
	VALUES(LOWER(@EmployeeName),LOWER(@EmployeeEmail),@PhoneNumber,@Salary,@DepartmentId,@RoleId)
END
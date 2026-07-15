
--ALTER TABLE Employee
--ADD ManagerId INT NULL;

--ALTER TABLE Employee
--ADD CONSTRAINT FK_Employee_Manager
--FOREIGN KEY (ManagerId)
--REFERENCES Employee(EmployeeId);

--CREATE OR ALTER PROCEDURE SP_Employee_Insert
--@EmployeeName NVARCHAR(200),@EmployeeEmail NVARCHAR(250),@PhoneNumber NVARCHAR(20),@Salary DECIMAL(18,2),@DepartmentId INT
--AS
--BEGIN
--	DECLARE @RoleId INT = 0;
--	SET @RoleId=(SELECT RoleId FROM Roles WHERE RoleName=LOWER('employee'))
	
--	INSERT INTO Employee(EmployeeName,EmployeeEmail,PhoneNumber,Salary,DepartmentId,RoleId) 
--	VALUES(LOWER(@EmployeeName),LOWER(@EmployeeEmail),@PhoneNumber,@Salary,@DepartmentId,@RoleId)
--END

--GO

--CREATE OR ALTER PROCEDURE SP_Employee_GetAll
--AS
--BEGIN
--	SELECT * FROM Employee WHERE IsDeleted=0
--END

--GO

CREATE   PROCEDURE SP_Employee_Delete
@EmployeeId INT
AS
BEGIN
	UPDATE Employee SET IsDeleted=1 WHERE EmployeeId=@EmployeeId
	IF(@@ROWCOUNT > 0)
		BEGIN
			SELECT 1 AS Code , 'Employee Deleted successfully' AS [Message]
		
		ENd
	ELSE 
		BEGIN
			SELECT 0 AS Code , 'Employee Deleted Failed' AS [Message]
		ENd
END
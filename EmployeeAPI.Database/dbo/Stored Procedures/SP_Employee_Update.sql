
CREATE   PROCEDURE SP_Employee_Update
@EmployeeId INT,@EmployeeName NVARCHAR(200),@EmployeeEmail NVARCHAR(250),@PhoneNumber NVARCHAR(20),@Salary DECIMAL(18,2)
AS
BEGIN
	UPDATE Employee SET EmployeeName=@EmployeeName,EmployeeEmail=@EmployeeEmail,PhoneNumber=@PhoneNumber,Salary=@Salary,UpdateAt=GETUTCDATE()
	WHERE EmployeeId=@EmployeeId
END
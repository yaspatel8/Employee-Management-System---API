CREATE   PROCEDURE SP_Profile_Get
(
	@UserId INT
)
AS
BEGIN
	SELECT E.UserId,E.EmployeeId,U.FullName,U.Email,E.PhoneNumber,E.Salary,EP.DepartmentId,D.DepartmentName ,E.ProfileImage ,E.CreatedAt,E.UpdatedAt
	FROM Employee E 
    RIGHT JOIN Users U ON E.UserId=U.UserId 
    LEFT JOIN EmployeePosition EP ON E.EmployeeId=EP.EmployeeId
    LEFT JOIN Department D ON D.DepartmentId=EP.DepartmentId WHERE E.UserId=@UserId
END
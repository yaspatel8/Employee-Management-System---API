CREATE   PROCEDURE SP_Department_Export
    @DepartmentIds dbo.IdListType READONLY
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        DepartmentId,
        DepartmentName,
        CreatedAt,
        UpdateAt,
        CASE WHEN Department.IsActive = 1 THEN 'Yes' ELSE 'No' END AS IsActive,
        CASE WHEN Department.IsDeleted = 1 THEN 'Yes' ELSE 'No' END AS IsDeleted
    FROM Department WHERE NOT EXISTS (SELECT 1 FROM @DepartmentIds)
        OR DepartmentId IN (SELECT Id FROM @DepartmentIds)
    ORDER BY DepartmentId;
END
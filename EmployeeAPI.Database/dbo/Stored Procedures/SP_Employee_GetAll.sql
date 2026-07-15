CREATE   PROCEDURE SP_Employee_GetAll 
(
    @userId INT,
    @SearchText NVARCHAR(200) = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 3,
    @SortColumn NVARCHAR(50) = 'EmployeeId',
    @SortOrder NVARCHAR(4) = 'DESC'
)
AS
BEGIN
    SET NOCOUNT ON;
    SET @SearchText = NULLIF(LTRIM(RTRIM(@SearchText)), '');

    IF @PageNumber < 1
        SET @PageNumber = 1;

    IF @PageSize < 1
        SET @PageSize = 3;


    CREATE TABLE #AccessibleEmployees
    (
        EmployeeId INT PRIMARY KEY
    );
    
    INSERT INTO #AccessibleEmployees(EmployeeId)
    EXEC SP_GetAccessibleEmployees
        @UserId = @UserId;

    CREATE TABLE #EmployeeData
    (
        EmployeeId INT,
        UserId INT,
        FullName NVARCHAR(200),
        Email NVARCHAR(200),
        PhoneNumber NVARCHAR(40),
        Salary DECIMAL(18,2),
        DepartmentId INT,
        DepartmentName NVARCHAR(200),
        RoleId INT,
        RoleName NVARCHAR(50),
        PositionId INT,
        PositionName NVARCHAR(200),
        ProfileImage NVARCHAR(MAX),
        ManagerEmail NVARCHAR(200),
        CreatedAt DATETIME,
        IsActive BIT
    );
    INSERT INTO #EmployeeData
    (
        EmployeeId,
        UserId,
        FullName,
        Email,
        PhoneNumber,
        Salary,
        DepartmentId,
        DepartmentName,
        RoleId,
        RoleName,
        PositionId,
        PositionName,
        ProfileImage,
        ManagerEmail,
        CreatedAt,
        IsActive
    )
    SELECT
        E.EmployeeId,
        U.UserId,
        U.FullName,
        U.Email,
        E.PhoneNumber,
        E.Salary,
        EP.DepartmentId,
        D.DepartmentName,
        U.RoleId,
        R.RoleName,
        EP.PositionId,
        P.PositionName,
        E.ProfileImage,
        MU.Email,
        E.CreatedAt,
        U.IsActive
    FROM Employee E
    
    INNER JOIN #AccessibleEmployees AE
        ON E.EmployeeId = AE.EmployeeId
    
    INNER JOIN Users U
        ON E.UserId = U.UserId
    
    LEFT JOIN EmployeePosition EP
        ON E.EmployeeId = EP.EmployeeId
       AND EP.IsActive = 1
    
    LEFT JOIN Department D
        ON EP.DepartmentId = D.DepartmentId
    
    LEFT JOIN Position P
        ON EP.PositionId = P.PositionId
    
    INNER JOIN Roles R
        ON U.RoleId = R.RoleId
    
    LEFT JOIN EmployeePosition MEP
        ON EP.ReportsToEmployeePositionId = MEP.EmployeePositionId
    
    LEFT JOIN Employee ME
        ON MEP.EmployeeId = ME.EmployeeId
    
    LEFT JOIN Users MU
        ON ME.UserId = MU.UserId
    
    WHERE
        E.IsDeleted = 0;

        CREATE CLUSTERED INDEX IX_EmployeeData_EmployeeId
        ON #EmployeeData(EmployeeId);

        DECLARE @TotalRecords INT;

    IF @SortColumn NOT IN
        (
            'EmployeeId',
            'FullName',
            'Email',
            'PhoneNumber',
            'Salary',
            'DepartmentName',
            'RoleName',
            'PositionName',
            'ManagerEmail',
            'CreatedAt',
            'IsActive'
        )
        BEGIN
            SET @SortColumn = 'EmployeeId';
        END
        
        IF UPPER(@SortOrder) NOT IN ('ASC','DESC')
        BEGIN
            SET @SortOrder = 'DESC';
        END

        DECLARE @SQL NVARCHAR(MAX);

        SET @SQL = N'
        SELECT
            EmployeeId,
            UserId,
            FullName,
            Email,
            PhoneNumber,
            Salary,
            DepartmentId,
            DepartmentName,
            RoleId,
            RoleName,
            PositionId,
            PositionName,
            ProfileImage,
            ManagerEmail,
            IsActive,
            CreatedAt,
            @TotalRecords AS TotalRecords
        FROM #EmployeeData
        WHERE
        (
              @SearchText IS NULL
           OR FullName LIKE ''%'' + @SearchText + ''%''
           OR Email LIKE ''%'' + @SearchText + ''%''
           OR PhoneNumber LIKE ''%'' + @SearchText + ''%''
           OR CAST(Salary AS NVARCHAR(30)) LIKE ''%'' + @SearchText + ''%''
           OR DepartmentName LIKE ''%'' + @SearchText + ''%''
           OR RoleName LIKE ''%'' + @SearchText + ''%''
           OR PositionName LIKE ''%'' + @SearchText + ''%''
           OR ManagerEmail LIKE ''%'' + @SearchText + ''%''
           OR EmployeeId = TRY_CAST(@SearchText AS INT)
           OR DepartmentId = TRY_CAST(@SearchText AS INT)
           OR PositionId = TRY_CAST(@SearchText AS INT)
        )
        ORDER BY ' + QUOTENAME(@SortColumn) + ' ' + @SortOrder + '
        OFFSET (@PageNumber - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY;';

        EXEC sp_executesql
        @SQL,
        N'@SearchText NVARCHAR(200),
          @PageNumber INT,
          @PageSize INT,
          @TotalRecords INT',
        @SearchText,
        @PageNumber,
        @PageSize,
        @TotalRecords;

    END
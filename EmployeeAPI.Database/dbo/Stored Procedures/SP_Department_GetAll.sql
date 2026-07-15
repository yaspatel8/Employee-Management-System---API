CREATE   PROCEDURE SP_Department_GetAll
(
    @SearchText NVARCHAR(100) = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 3,
    @SortColumn NVARCHAR(50) = 'DepartmentId',
    @SortOrder NVARCHAR(4) = 'DESC'
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @SearchText = NULLIF(TRIM(@SearchText), '');

    IF @PageNumber < 1
        SET @PageNumber = 1;

    IF @PageSize < 1
        SET @PageSize = 3;

    DECLARE @TotalRecords INT;

    SELECT @TotalRecords = COUNT(*)
    FROM Department
    WHERE IsDeleted = 0
      AND (
            @SearchText IS NULL
            OR LOWER(DepartmentName) LIKE '%' + LOWER(@SearchText) + '%'
            OR DepartmentId = TRY_CAST(@SearchText AS INT)
          );

    -- Validation
    IF @SortColumn NOT IN ('DepartmentId', 'DepartmentName', 'CreatedAt', 'UpdateAt', 'IsActive')
        SET @SortColumn = 'DepartmentId';

    IF UPPER(@SortOrder) NOT IN ('ASC', 'DESC')
        SET @SortOrder = 'DESC';

    DECLARE @SQL NVARCHAR(MAX);

    SET @SQL = '
    SELECT
        DepartmentId,
        DepartmentName,
        CreatedAt,
        UpdateAt,
        IsDeleted,
        IsActive,
        ' + CAST(@TotalRecords AS NVARCHAR(20)) + ' AS TotalRecords
    FROM Department
    WHERE IsDeleted = 0
      AND (
            @SearchText IS NULL
            OR LOWER(DepartmentName) LIKE ''%'' + LOWER(@SearchText) + ''%''
            OR DepartmentId = TRY_CAST(@SearchText AS INT)
          )
    ORDER BY ' + QUOTENAME(@SortColumn) + ' ' + @SortOrder + '
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY';

    EXEC sp_executesql
        @SQL,
        N'@SearchText NVARCHAR(100), @PageNumber INT, @PageSize INT',
        @SearchText,
        @PageNumber,
        @PageSize;
END
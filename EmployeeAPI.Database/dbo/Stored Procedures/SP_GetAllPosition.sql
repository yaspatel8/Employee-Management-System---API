CREATE   PROCEDURE SP_GetAllPosition
(
    @Search NVARCHAR(100) = NULL,
    @SortColumn NVARCHAR(50) = 'Level',
    @SortOrder NVARCHAR(4) = 'ASC', 
    @PageNumber INT = 1,
    @PageSize INT = 3
)
AS
BEGIN
      SET NOCOUNT ON;   

    SET @Search = NULLIF(LTRIM(RTRIM(@Search)), '');

    IF @PageNumber < 1
        SET @PageNumber = 1;

    IF @PageSize < 1
        SET @PageSize = 3;

    DECLARE @TotalRecords INT;

    SELECT @TotalRecords = COUNT(*)
    FROM Position WHERE IsDeleted = 0
      AND (
            @Search IS NULL
         OR LOWER(PositionName) LIKE '%' + LOWER(@Search) + '%'
         OR CAST([Level] AS NVARCHAR(50)) LIKE '%' + @Search + '%'
         OR PositionId = TRY_CAST(@Search AS INT)
      );

    -- Validate Sort Column
    IF @SortColumn NOT IN
    (
        'PositionId',
        'PositionName',
        'Level'
    )
        SET @SortColumn = 'Level';

     IF UPPER(@SortOrder) NOT IN ('ASC', 'DESC')
        SET @SortOrder = 'DESC';

    DECLARE @SQL NVARCHAR(MAX);

    SET @SQL = '
    SELECT
        PositionId,
        PositionName,
        [Level],
        CreatedBy,
        CreatedAt,
        IsActive,
        UpdatedBy,
        ' + CAST(@TotalRecords AS NVARCHAR(20)) + ' AS TotalRecords
    FROM Position
    WHERE IsDeleted = 0
      AND (
            @Search IS NULL
         OR LOWER(PositionName) LIKE ''%'' + LOWER(@Search) + ''%''
         OR CAST([Level] AS NVARCHAR(50)) LIKE ''%'' + @Search + ''%''
         OR PositionId = TRY_CAST(@Search AS INT)
      )
    ORDER BY ' +  QUOTENAME(@SortColumn) + ' ' + @SortOrder + '
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY';


    EXEC sp_executesql
        @SQL,
        N'@Search NVARCHAR(200), @PageNumber INT, @PageSize INT',
        @Search,
        @PageNumber,
        @PageSize;
END
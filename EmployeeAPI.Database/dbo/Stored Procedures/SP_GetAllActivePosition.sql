CREATE   PROCEDURE SP_GetAllActivePosition
AS
bEGIN
    SET NOCOUNT ON;
    SELECT PositionId, PositionName, [Level]
    FROM Position
    WHERE IsActive = 1 AND IsDeleted = 0
    ORDER BY [Level] ASC;
END
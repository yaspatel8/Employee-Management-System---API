CREATE   PROCEDURE SP_AddOrUpdatePosition
(
    @PositionId INT = NULL,
    @PositionName NVARCHAR(100),
    @Level INT,
    @CreatedBy INT = NULL,
    @UpdatedBy INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    -----------------------
    -- INSERT
    -----------------------
    IF ISNULL(@PositionId,0) = 0
    BEGIN

        IF EXISTS
        (
            SELECT 1
            FROM Position
            WHERE LOWER(TRIM(PositionName)) = LOWER(TRIM(@PositionName))
              AND IsDeleted = 0
        )
        BEGIN
            SELECT -1 AS Code,
                   'Position Name already exists.' AS Message;
            RETURN;
        END

        IF EXISTS
        (
            SELECT 1
            FROM Position
            WHERE [Level] = @Level
              AND IsDeleted = 0
        )
        BEGIN
            SELECT -1 AS Code,
                   'Position Level already exists.' AS Message;
            RETURN;
        END

        INSERT INTO Position
        (
            PositionName,
            [Level],
            CreatedBy,
            CreatedAt
        )
        VALUES
        (
            LOWER(TRIM(@PositionName)),
            @Level,
            @CreatedBy,
            GETDATE()
        );

        SELECT 1 AS Code,
               'Position added successfully.' AS Message;

        RETURN;
    END


    -----------------------
    -- UPDATE
    -----------------------

    -- Check duplicate name except current record
    IF EXISTS
    (
        SELECT 1
        FROM Position
        WHERE LOWER(TRIM(PositionName)) = LOWER(TRIM(@PositionName))
          AND PositionId <> @PositionId
          AND IsDeleted = 0
    )
    BEGIN
        SELECT -1 AS Code,
               'Position Name already exists.' AS Message;
        RETURN;
    END

    -- Check duplicate level except current record
    IF EXISTS
    (
        SELECT 1
        FROM Position
        WHERE [Level] = @Level
          AND PositionId <> @PositionId
          AND IsDeleted = 0
    )
    BEGIN
        SELECT -1 AS Code,
               'Position Level already exists.' AS Message;
        RETURN;
    END

    UPDATE Position
    SET PositionName = LOWER(TRIM(@PositionName)),
        [Level] = @Level,
        UpdatedBy = @UpdatedBy,
        UpdatedAt = GETDATE()
    WHERE PositionId = @PositionId;

    IF @@ROWCOUNT > 0
    BEGIN
        SELECT 1 AS Code,
               'Position updated successfully.' AS Message;
    END
    ELSE
    BEGIN
        SELECT 0 AS Code,
               'Failed to update Position.' AS Message;
    END
END
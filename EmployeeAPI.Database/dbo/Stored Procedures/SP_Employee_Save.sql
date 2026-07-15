CREATE   PROCEDURE SP_Employee_Save
(
    @EmployeeId INT = NULL,
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100)=NULL,
    @PhoneNumber NVARCHAR(20),
    @Salary DECIMAL(18,2),
    @DepartmentId INT,
    @RoleId INT = NULL,
    @PasswordHash NVARCHAR(MAX),
    @ProfileImage NVARCHAR(MAX) = NULL,
    @OldFileName NVARCHAR(200) OUTPUT,
    @UpdatedBy INT = NULL,
    @CreatedBy INT = NULL,
    @PositionId INT = NULL,
    @ReportsToEmployeePositionId INT = NULL
  
)
AS
BEGIN
 
DECLARE @UserId INT;
    DECLARE @NewEmployeeId INT;
 
IF ISNULL(@EmployeeId,0) = 0
BEGIN
--SELECT @RoleId = RoleId
--FROM Roles
--WHERE LOWER(RoleName) = 'employee';
 
--ADD Employee
    IF EXISTS (
        SELECT 1
        FROM Users
        WHERE Email = LOWER(TRIM(@Email))
    )
    BEGIN
        SELECT -1 AS Code,
               'Email already exists' AS Message;
        RETURN;
    END
 
    IF EXISTS (
        SELECT 1
        FROM Employee
        WHERE PhoneNumber = @PhoneNumber
    )
    BEGIN
        SELECT -1 AS Code,
               'Phone number already exists' AS Message;
        RETURN;
    END
 
   INSERT INTO Users
    (
        FullName,
        Email,
        PasswordHash,
        RoleId,
        IsFistLogin
    )
    VALUES
    (
        LOWER(TRIM(@FullName)),
        LOWER(TRIM(@Email)),
        @PasswordHash,
        @RoleId,1
    );
 
    SET @UserId = SCOPE_IDENTITY();
 
    INSERT INTO Employee
    (
        UserId,
        PhoneNumber,
        Salary,
        --DepartmentId,
        ProfileImage,
        CreatedBy
        --PositionId,
        --ManagerId
    )
    VALUES
    (
        @UserId,
        @PhoneNumber,
        @Salary,
        --@DepartmentId,
        @ProfileImage,
        @CreatedBy
        --@PositionId,
        --@ManagerId
              
    );
 
            SET @NewEmployeeId = SCOPE_IDENTITY();
            INSERT INTO EmployeePosition
            (
                EmployeeId,
                DepartmentId,
                PositionId,
                ReportsToEmployeePositionId,
                StartDate,
                IsActive,
                CreatedBy,
                CreatedAt
            )
            VALUES
            (
                @NewEmployeeId,
                @DepartmentId,
                @PositionId,
                @ReportsToEmployeePositionId,
                GETDATE(),
                1,
                @CreatedBy,
                GETDATE()
            );
 
        IF(@@ROWCOUNT > 0)
        BEGIN
        SELECT 1 AS Code,  CONCAT('Employee added successfully by User ID: ', @CreatedBy) AS Message;
        END
        ELSE
        BEGIN
        SELECT 0 AS Code , 'Employee added Failed' AS [Message]
        END
            
        END
         
        --UPDATE
         
            ELSE
        BEGIN
            BEGIN TRY
         
                SELECT @UserId = UserId
                FROM Employee
                WHERE EmployeeId = @EmployeeId;
         
                IF EXISTS
                (
                    SELECT 1
                    FROM Users
                    WHERE Email = LOWER(TRIM(@Email))
                    AND UserId <> @UserId
                )
                BEGIN
                    SELECT -1 AS Code,'Email already exists' AS Message;
                    RETURN;
                END
         
                IF EXISTS
                (
                    SELECT 1
                    FROM Employee
                    WHERE PhoneNumber = @PhoneNumber
                    AND EmployeeId <> @EmployeeId
                )
                BEGIN
                    SELECT -1 AS Code,'Phone number already exists' AS Message;
                    RETURN;
                END
         
                -- Old Image
                SELECT @OldFileName = ProfileImage
                FROM Employee
                WHERE EmployeeId=@EmployeeId;
         
                -----------------------------
                -- Update User
                -----------------------------
                UPDATE Users
                SET
                    FullName = LOWER(TRIM(@FullName)),
                    Email = LOWER(TRIM(@Email)),
                    RoleId = @RoleId
                WHERE UserId=@UserId;
         
                -----------------------------
                -- Update Employee
                -----------------------------
                UPDATE Employee
                SET
                    PhoneNumber=@PhoneNumber,
                    Salary=@Salary,
                    ProfileImage = ISNULL(@ProfileImage,ProfileImage),
                    UpdatedAt=GETDATE(),
                    UpdatedBy=@UpdatedBy
                WHERE EmployeeId=@EmployeeId;
         
                DECLARE
                    @CurrentDepartmentId INT,
                    @CurrentPositionId INT,
                    @CurrentReportsToEmployeePositionId INT;
         
                SELECT
                    @CurrentDepartmentId = DepartmentId,
                    @CurrentPositionId = PositionId,
                    @CurrentReportsToEmployeePositionId = ReportsToEmployeePositionId
                FROM EmployeePosition
                WHERE EmployeeId=@EmployeeId
                AND IsActive=1;
         
                -----------------------------------
                -- Department/Position Changed
                -----------------------------------
                IF(ISNULL(@CurrentDepartmentId,0)<>ISNULL(@DepartmentId,0)
                   OR ISNULL(@CurrentPositionId,0)<>ISNULL(@PositionId,0))
                BEGIN
         
                    UPDATE EmployeePosition
                    SET
                        EndDate=GETDATE(),
                        IsActive=0,
                        UpdatedBy=@UpdatedBy,
                        UpdatedAt=GETDATE()
                    WHERE EmployeeId=@EmployeeId
                    AND IsActive=1;
         
                    INSERT INTO EmployeePosition
                    (
                        EmployeeId,
                        DepartmentId,
                        PositionId,
                        ReportsToEmployeePositionId,
                        StartDate,
                        IsActive,
                        CreatedBy,
                        CreatedAt
                    )
                    VALUES
                    (
                        @EmployeeId,
                        @DepartmentId,
                        @PositionId,
                        @ReportsToEmployeePositionId,
                        GETDATE(),
                        1,
                        @UpdatedBy,
                        GETDATE()
                    );
         
                END
                -----------------------------------
                -- Only Reporting Manager Changed
                -----------------------------------
                ELSE IF(ISNULL(@CurrentReportsToEmployeePositionId,0)
                        <> ISNULL(@ReportsToEmployeePositionId,0))
                BEGIN
         
                    UPDATE EmployeePosition
                    SET
                        ReportsToEmployeePositionId=@ReportsToEmployeePositionId,
                        UpdatedBy=@UpdatedBy,
                        UpdatedAt=GETDATE()
                    WHERE EmployeeId=@EmployeeId
                    AND IsActive=1;
         
                END
         
              IF @@TRANCOUNT > 0
            COMMIT TRANSACTION;
         
        SELECT
            1 AS Code,
            CONCAT('Employee Updated Successfully by User ID : ', @UpdatedBy) AS Message;
            END TRY
         
            BEGIN CATCH
         
                IF @@TRANCOUNT>0
                    ROLLBACK TRANSACTION;
         
                SELECT
                    0 AS Code,
                    ERROR_MESSAGE() AS Message;
         
            END CATCH
        END
END
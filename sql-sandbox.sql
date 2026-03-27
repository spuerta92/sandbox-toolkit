CREATE DATABASE Testing;
USE Testing;

CREATE TABLE EmployeeA (
	EmployeeId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	EmployeeName VARCHAR(255) NOT NULL,
	CreateDate DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE EmployeeB (
	EmployeeId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	EmployeeName VARCHAR(255) NOT NULL,
	CreateDate DATETIME NOT NULL DEFAULT GETDATE()
);

ALTER TABLE EmployeeA ADD CONSTRAINT DF_EmployeeA DEFAULT GETDATE() FOR CreateDate
ALTER TABLE EmployeeB ADD CONSTRAINT DF_EmployeeB DEFAULT GETDATE() FOR CreateDate

INSERT INTO EmployeeA (EmployeeName) 
	VALUES ('Karen'), ('Thomas'), ('Winston'), ('Jean'), ('Dominique'), ('Paul'), ('Lin')
INSERT INTO EmployeeB (EmployeeName) 
	VALUES ('Kristen'), ('Eric'), ('Kate'), ('Jean'), ('Dominique'), ('George'), ('Sarah')

SELECT * FROM EmployeeA
SELECT * FROM EmployeeB

--------------------------------------------------------------
-- JOINS
SELECT *
FROM EmployeeA ea
JOIN EmployeeB eb
	ON ea.EmployeeName = eb.EmployeeName

SELECT *
FROM EmployeeA ea
LEFT JOIN EmployeeB eb
	ON ea.EmployeeName = eb.EmployeeName


--FROM Table1 t1
--LEFT JOIN Table2 t2
--ON t1.Column = t2.Column
--WHERE t2.Column IS NULL;

SELECT *
FROM EmployeeA ea
RIGHT JOIN EmployeeB eb
	ON ea.EmployeeName = eb.EmployeeName

SELECT *
FROM EmployeeA ea
RIGHT JOIN EmployeeB eb
	ON ea.EmployeeName = eb.EmployeeName

SELECT *
FROM EmployeeA ea
FULL JOIN EmployeeB eb
	ON ea.EmployeeName = eb.EmployeeName

SELECT *
FROM EmployeeA ea
FULL OUTER JOIN EmployeeB eb
	ON ea.EmployeeName = eb.EmployeeName

-- CARTESIAN PRODUCT (CROSS JOIN or SELF JOIN)
SELECT * 
FROM dbo.EmployeeA ea
CROSS JOIN dbo.EmployeeB eb

--------------------------------------------------------------
-- CONNECT TWO TABLES
SELECT * 
FROM EmployeeA, EmployeeB
WHERE EmployeeA.EmployeeName = EmployeeB.EmployeeName

--------------------------------------------------------------
-- UNION & INTERSECT
SELECT *
FROM EmployeeA 
UNION
SELECT *
FROM EmployeeB

SELECT EmployeeId, EmployeeName
FROM EmployeeA 
INTERSECT
SELECT EmployeeId, EmployeeName
FROM EmployeeB

--------------------------------------------------------------
-- CASE WHEN
SELECT 
    Id,
    FirstName,
    LastName,
    Salary,
    CASE 
        WHEN Salary >= 100000 THEN 'High'
        WHEN Salary >= 50000 THEN 'Medium'
        ELSE 'Low'
    END AS SalaryCategory
FROM Employees

--------------------------------------------------------------
-- IF THEN
IF EXISTS (SELECT 1 FROM Employees WHERE Department = 'IT')
    PRINT 'IT Department exists.'
ELSE
    PRINT 'IT Department does not exist.'

--------------------------------------------------------------
-- MERGE
MERGE INTO TargetTable AS target
USING SourceTable AS source
    ON target.Id = source.Id
WHEN MATCHED THEN
    UPDATE SET target.Name = source.Name
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Id, Name) VALUES (source.Id, source.Name)
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;

--------------------------------------------------------------
-- RANK / PARTITION
SELECT
    Id,
    FirstName,
    LastName,
    Department,
    Salary,
    RANK() OVER (PARTITION BY Department ORDER BY Salary DESC) AS SalaryRank
FROM Employees

SELECT *
FROM (
    SELECT 
        ROW_NUMBER() OVER (PARTITION BY f.Id ORDER BY f.SysStartTime DESC) AS RowNum
    FROM dbo.Fruits f 
) s1
WHERE s1.RowNum = 1;

--------------------------------------------------------------
-- TRIGGER
CREATE TRIGGER trg_EmployeeInsert
ON Employees
AFTER INSERT
AS
BEGIN
    INSERT INTO Employee (EmployeeId, Action, ActionDate)
    SELECT Id, 'INSERT', GETDATE()
    FROM inserted
END

--------------------------------------------------------------
-- INDEX
CREATE INDEX IX_Employees_Department
ON Employees (Department)

--------------------------------------------------------------
-- VIEW
CREATE VIEW dbo.vw_ActiveEmployees AS
SELECT Id, FirstName, LastName, Department
FROM Employees
WHERE IsActive = 1

--------------------------------------------------------------
-- FUNCTION
CREATE FUNCTION dbo.GetFullName (@FirstName NVARCHAR(50), @LastName NVARCHAR(50))
RETURNS NVARCHAR(101)
AS
BEGIN
    RETURN (@FirstName + ' ' + @LastName)
END

--------------------------------------------------------------
-- STORE PROC
CREATE PROCEDURE dbo.usp_GetEmployeeById
    @EmployeeId INT
AS
BEGIN
    SELECT Id, FirstName, LastName, Department
    FROM Employees
    WHERE Id = @EmployeeId
END

--------------------------------------------------------------
-- TRY CATCH
BEGIN TRY
    -- Code that might throw an error
    UPDATE Employees SET Salary = Salary * 1.1 WHERE Department = 'Sales';
END TRY
BEGIN CATCH
    PRINT 'An error occurred: ' + ERROR_MESSAGE();
END CATCH

--------------------------------------------------------------
-- AGGREGATING DATA: SUM, COUNT, MIN, MAX, AVG
SELECT EmployeeId, COUNT(ProjectId) AS NumberOfProjects
FROM dbo.Employees
GROUP BY EmployeeId

SELECT MAX(BudgetAmount) AS MaxBudget
FROM dbo.Projects

SELECT MIN(BudgetAmount) AS MinBudget
FROM dbo.Projects

SELECT SUM(BudgetAmount) AS TotalBudget
FROM dbo.Projects

SELECT AVG(BudgetAmount) AS TotalBudget
FROM dbo.Projects

--------------------------------------------------------------
-- EXCEPTION
SELECT Id, Name FROM Employees
EXCEPT
SELECT Id, Name FROM FormerEmployees

--------------------------------------------------------------
-- DATE OPs
SELECT DATEADD(day, 7, GETDATE()) AS OneWeekFromNow
FROM Employees

SELECT DATEDIFF(day, HireDate, GETDATE()) AS DaysSinceHire
FROM Employees

--------------------------------------------------------------
-- JSON READ
SELECT p.*
FROM (
	SELECT
		JSON_VALUE(JSON_QUERY(rm.Message, '$.payload'), '$.department.id') AS DepartmentId,
		JSON_VALUE(JSON_QUERY(rm.Message, '$.payload'), '$.project.id') AS ProjectId
	FROM (
		SELECT TOP 100 
			Body,
			JSON_VALUE(Body, '$.Message') AS Message
		FROM dbo.ReceivedMessages rm
	) s
) p

--------------------------------------------------------------
-- WHITE LOOP
	DECLARE @employeeId BIGINT;
	DECLARE _cursor CURSOR FOR SELECT EmployeeId FROM #temp WHERE CanDelete = 1
	OPEN _cursor;
	FETCH NEXT FROM _cursor INTO @employeeId;
	WHILE (@@FETCH_STATUS = 0)
	BEGIN
		DECLARE @roleIds dbo.RoleTable

		INSERT INTO @roleIds (RoleId)
		SELECT s.RoleId
		FROM dbo.Roles r
		
		FETCH NEXT FROM _cursor INTO @employeeId;
	END;

	CLOSE _cursor;
	DEALLOCATE _cursor;
	
--------------------------------------------------------------
-- CTEs
WITH OrgChart AS (
    SELECT Id, ManagerId, Name, 0 AS Level
    FROM Employees
    WHERE ManagerId IS NULL
    UNION ALL
    SELECT e.Id, e.ManagerId, e.Name, oc.Level + 1
    FROM Employees e
    INNER JOIN OrgChart oc ON e.ManagerId = oc.Id
)
SELECT * FROM OrgChart
ORDER BY Level, ManagerId

--------------------------------------------------------------
-- JSON or XML output
SELECT Id, Name, Department
FROM Employees
FOR JSON AUTO

SELECT Id, Name, Department
FROM Employees
FOR XML AUTO

--------------------------------------------------------------
-- APPLY - Invoke table-valued functions or correlated subqueries per row.
SELECT e.Id, e.Name, p.ProjectName
FROM Employees e
CROSS APPLY (
    SELECT TOP 1 ProjectName
    FROM Projects p
    WHERE p.EmployeeId = e.Id
    ORDER BY p.StartDate DESC
) p

--------------------------------------------------------------
-- PIVOT Transform rows to columns (and vice versa) for reporting.
SELECT Department, [2023], [2024]
FROM (
    SELECT Department, YEAR(HireDate) AS HireYear, COUNT(*) AS EmployeeCount
    FROM Employees
    GROUP BY Department, YEAR(HireDate)
) AS SourceTable
PIVOT (
    SUM(EmployeeCount)
    FOR HireYear IN ([2023], [2024])
) AS PivotTable

--------------------------------------------------------------
--  Window functions - Access data from other rows without self-joins.
SELECT 
    Id, 
    Salary,
    LAG(Salary, 1) OVER (ORDER BY HireDate) AS PrevSalary,
    LEAD(Salary, 1) OVER (ORDER BY HireDate) AS NextSalary
FROM Employees

--------------------------------------------------------------
--  Temporarily disable IDENTITY (auto-increment primary key)
SET IDENTITY_INSERT dbo.Roles ON;
INSERT INTO dbo.Roles
(
	RoleId,
	RoleName
)
VALUES
(	4,
	N'Treasury Officer (DEPRECATED)',  
	)
SET IDENTITY_INSERT dbo.Roles OFF;



 








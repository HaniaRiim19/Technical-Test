USE [DevelopmentTest];
GO

ALTER PROCEDURE ClassRegistrationReport
AS
BEGIN
    
    PRINT 'Entered';


    SELECT 
        c.ClassName AS [Class], 
        t.TeacherName AS [Teacher Name],
        COUNT(cr.Student_ID) AS [Registrations],
        SUM(CASE WHEN cr.HasPaidFees = 1 THEN 1 ELSE 0 END) AS [Number Paid]
    FROM 
        [dbo].[Class] c
    INNER JOIN 
        [dbo].[Teacher] t ON c.Teacher_ID = t.Teacher_ID
    LEFT OUTER JOIN 
        [dbo].[ClassRegistration] cr ON c.Class_ID = cr.Class_ID
    GROUP BY 
        c.ClassName,
        t.TeacherName
    ORDER BY 
        c.ClassName;
END;
GO



-- USE [DevelopmentTest];
-- GO
-- SELECT 
--     c.ClassName AS [Class], 
--     t.TeacherName AS [Teacher Name],
--     COUNT(cr.Student_ID) AS [Registrations],
--     SUM(CASE WHEN cr.HasPaidFees = 1 THEN 1 ELSE 0 END) AS [Number Paid]
-- FROM 
--     [dbo].[Class] c
-- INNER JOIN 
--     [dbo].[Teacher] t ON c.Teacher_ID = t.Teacher_ID
-- LEFT OUTER JOIN 
--     [dbo].[ClassRegistration] cr ON c.Class_ID = cr.Class_ID
-- GROUP BY 
--     c.ClassName,
--     t.TeacherName
-- ORDER BY 
--     c.ClassName;

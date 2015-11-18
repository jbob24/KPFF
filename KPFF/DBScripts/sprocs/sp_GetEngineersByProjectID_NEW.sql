IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetEngineersByProjectID_NEW]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_GetEngineersByProjectID_NEW]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetEngineersByProjectID_NEW]
	@ProjectID int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT e.EmployeeID, EmployeeFirst, EmployeeLast, EmployeeName, Title, EmploymentStartDate, YearsOfExperience,
		Education, Licenses, ProfessionalMemberships, ProfessionalCommittees, Comments, HoursPerWeek,
		PhoneExtension, EmailAddress, PMFiscalSummary, PICFiscalSummary
	FROM tblEmployeeProjectAssignments a 
		JOIN tblEmployees e on a.EmployeeID = e.EmployeeID
	WHERE a.ProjectID = @ProjectID
		AND a.Assigned = 1
END
GO



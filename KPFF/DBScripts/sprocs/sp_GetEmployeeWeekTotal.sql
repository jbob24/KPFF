IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetEmployeeWeekTotal]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].sp_GetEmployeeWeekTotal
GO

create PROCEDURE [dbo].sp_GetEmployeeWeekTotal
	@EmployeeId INT,
	@WeekdId INT

AS
BEGIN

	SELECT SUM(tblSchedule.Hours) AS WeekHours
	FROM tblSchedule INNER JOIN
	tblProjects ON
	tblSchedule.ProjectID = tblProjects.ID
	WHERE tblSchedule.EmployeeID = @EmployeeId 
	AND tblSchedule.WeekID = @WeekdId
	AND tblProjects.Active = 1
	
END

GO



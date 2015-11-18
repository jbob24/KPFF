IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetUserByUserNamePassword]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_GetUserByUserNamePassword]
GO

create PROCEDURE [dbo].sp_GetUserByUserNamePassword
	@UserName varchar(50),
	@Password varchar(50)

AS
BEGIN

	SELECT u.UserLevel, u.EmployeeID, e.EmployeeFirst, e.EmployeeLast, e.PMFiscalSummary, e.PICFiscalSummary, e.HoursPerWeek, e.EmployeeCode
	FROM tblEmployees e
	INNER JOIN tblUsers u ON e.EmployeeID = u.EmployeeID
	WHERE u.UserName = @UserName 
		AND u.Password = @Password
END

GO



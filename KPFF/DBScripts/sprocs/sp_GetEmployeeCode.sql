IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetEmployeeCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].sp_GetEmployeeCode
GO

create PROCEDURE [dbo].sp_GetEmployeeCode
	@EmployeeId INT

AS
BEGIN

	SELECT EmployeeCode FROM tblEmployees WHERE EmployeeID = @EmployeeId;
	
END

GO



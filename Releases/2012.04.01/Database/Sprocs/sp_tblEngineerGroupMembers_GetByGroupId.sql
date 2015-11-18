IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tblEngineerGroupMembers_GetByGroupId]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_tblEngineerGroupMembers_GetByGroupId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create PROCEDURE [dbo].[sp_tblEngineerGroupMembers_GetByGroupId]
	@GroupID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT m.EmployeeID,e.EmployeeName, m.Active
	FROM tblEngineerGroupMembers m
	Inner Join tblEmployees e on m.EmployeeID = e.EmployeeID
	WHERE m.GroupID = @GroupID

END

GO



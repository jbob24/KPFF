IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tblEngineerGroupMembers_Insert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_tblEngineerGroupMembers_Insert]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create PROCEDURE [dbo].[sp_tblEngineerGroupMembers_Insert]
	@GroupID int,
	@EmployeeId int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF (SELECT COUNT(EmployeeId) FROM tblEngineerGroupMembers WHERE GroupID = @GroupID AND EmployeeID = @EmployeeId) <= 0 
	BEGIN
		INSERT INTO tblEngineerGroupMembers (GroupID, EmployeeID)
		SELECT @GroupID,@EmployeeId
	END
	
END

GO



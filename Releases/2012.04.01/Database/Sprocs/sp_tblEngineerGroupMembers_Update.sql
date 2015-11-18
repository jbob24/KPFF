IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tblEngineerGroupMembers_Update]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_tblEngineerGroupMembers_Update]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create PROCEDURE [dbo].[sp_tblEngineerGroupMembers_Update]
	@GroupID int,
	@EmployeeID int,
	@Active bit

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	UPDATE tblEngineerGroupMembers
	SET [Active] = @Active
	WHERE GroupID = @GroupID AND EmployeeID = @EmployeeID

END

GO



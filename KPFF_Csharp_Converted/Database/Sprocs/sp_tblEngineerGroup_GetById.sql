IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tblEngineerGroup_GetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_tblEngineerGroup_GetById]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create PROCEDURE [dbo].[sp_tblEngineerGroup_GetById]
	@GroupID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT ID, Name, Description, Active, DateCreated 
	FROM tblEngineerGroups
	WHERE ID = @GroupID

END

GO



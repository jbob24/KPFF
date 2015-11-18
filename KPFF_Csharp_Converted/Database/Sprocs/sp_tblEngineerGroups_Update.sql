IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tblEngineerGroups_Update]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_tblEngineerGroups_Update]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create PROCEDURE [dbo].[sp_tblEngineerGroups_Update]
	@ID int,
	@Name varchar(50),
	@Description varchar(50),
	@Active bit

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	UPDATE tblEngineerGroups
	SET [Name] = @Name,
		[Description] = @Description,
		[Active] = @Active
	WHERE ID = @ID

END

GO



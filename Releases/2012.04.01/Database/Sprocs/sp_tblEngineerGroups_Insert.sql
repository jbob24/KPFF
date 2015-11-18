IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tblEngineerGroups_Insert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_tblEngineerGroups_Insert]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create PROCEDURE [dbo].[sp_tblEngineerGroups_Insert]
	@Name varchar(50),
	@Description varchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO tblEngineerGroups ([Name] ,[Description])
    SELECT @Name, @Description
    
    SELECT @@IDENTITY
END

GO



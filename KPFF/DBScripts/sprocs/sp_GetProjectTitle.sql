IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetProjectTitle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].sp_GetProjectTitle
GO

create PROCEDURE [dbo].sp_GetProjectTitle
	@ProjectId INT


AS
BEGIN

	SELECT ProjectNo, ProjectName
	FROM tblProjects
	WHERE ID = @ProjectId
END

GO



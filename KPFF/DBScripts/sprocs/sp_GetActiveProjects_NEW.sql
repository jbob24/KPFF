IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetActiveProjects_NEW]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_GetActiveProjects_NEW]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetActiveProjects_NEW]
	@EmployeeID int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT ID, ProjectNo, ProjectName, ClientID, ProjectStatus, ProjectLocation,
		ConstructionType, ProjectType, PhaseID, EstimatedStartDate, EstimatedCompletionDate, FeeAmount,
		FeeStructure, ContractType, Contractee, PIC, PICCode, PM1, PM1Code, PM2, PM2Code, Comments,
		Active, LastModifiedByUserID, LastModifiedDate 
	FROM tblProjects p	
	WHERE
		p.Active = 1
	ORDER BY p.ProjectNo

END



GO



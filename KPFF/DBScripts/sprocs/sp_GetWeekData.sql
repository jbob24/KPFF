IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetWeekData]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_GetWeekData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetWeekData]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ID, WeekNo, WeekNoYear, WeekNoMonth, WeekNoDay, WeekNoStartDate  
	FROM tblWeeks (NOLOCK)
	ORDER BY ID

END


GO



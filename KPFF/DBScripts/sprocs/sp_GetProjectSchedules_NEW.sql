IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetProjectSchedules_NEW]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_GetProjectSchedules_NEW]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetProjectSchedules_NEW]
	@ProjectList varchar(max),
	@StartDate datetime,
	@EndDate datetime
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @ProjectTable table
	(
		ProjectId int
	)

	DECLARE @ProjectId varchar(10), @Pos int

	SET @ProjectList = LTRIM(RTRIM(@ProjectList))+ ','
	SET @Pos = CHARINDEX(',', @ProjectList, 1)

	IF REPLACE(@ProjectList, ',', '') <> ''
	BEGIN
		WHILE @Pos > 0
		BEGIN
			SET @ProjectId = LTRIM(RTRIM(LEFT(@ProjectList, @Pos - 1)))
			IF @ProjectId <> ''
			BEGIN
				INSERT INTO @ProjectTable (ProjectId) VALUES (CAST(@ProjectId AS int))
			END
			SET @ProjectList = RIGHT(@ProjectList, LEN(@ProjectList) - @Pos)
			SET @Pos = CHARINDEX(',', @ProjectList, 1)

		END
	END	
	
	SELECT s.ID, s.ProjectID, s.EmployeeID, s.WeekID, s.Hours, s.LastModifiedDate, s.LastModifiedByUserID
	FROM tblSchedule s
		JOIN @ProjectTable p on s.ProjectID = p.ProjectId
		JOIN tblWeeks w on s.WeekID = w.ID
	WHERE 
		s.ProjectID in (587,292,293,889,1305,1095,1136,1216,1259,1215,1272,1322,1326,115,88,29,652)
		AND WeekNoStartDate BETWEEN @StartDate AND @EndDate
	ORDER BY WeekID	
END
GO	
	

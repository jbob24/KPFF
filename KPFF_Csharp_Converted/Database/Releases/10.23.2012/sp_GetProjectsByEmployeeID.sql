/****** Object:  StoredProcedure [dbo].[sp_GetProjectsByEmployeeID]    Script Date: 10/23/2012 21:15:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetProjectsByEmployeeID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_GetProjectsByEmployeeID]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetProjectsByEmployeeID]    Script Date: 10/23/2012 21:15:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetProjectsByEmployeeID]
	@EmployeeID int,
	@StartDate datetime,
	@EndDate datetime,
	@WeekNums varchar(100)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
declare @Weeks table (id int identity(1,1), weeknum int);

insert into @Weeks 
SELECT CAST(DATA as int)
FROM   [dbo].[uf_Split] (@WeekNums, ',')		

declare @WeekNum1 int, 	@WeekNum2 int, 	@WeekNum3 int, 	@WeekNum4 int, 	@WeekNum5 int, 	@WeekNum6 int,
	@WeekNum7 int, 	@WeekNum8 int, 	@WeekNum9 int, 	@WeekNum10 int,	@WeekNum11 int,	@WeekNum12 int, 
	@WeekNum13 int, @WeekNum14 int, @WeekNum15 int, @WeekNum16 int, @WeekNum17 int, @WeekNum18 int, 
	@WeekNum19 int, @WeekNum20 int;
		
select @WeekNum1 = WeekNum from @Weeks where id = 1;
select @WeekNum2 = WeekNum from @Weeks where id = 2
select @WeekNum3 = WeekNum from @Weeks where id = 3
select @WeekNum4 = WeekNum from @Weeks where id = 4
select @WeekNum5 = WeekNum from @Weeks where id = 5
select @WeekNum6 = WeekNum from @Weeks where id = 6
select @WeekNum7 = WeekNum from @Weeks where id = 7
select @WeekNum8 = WeekNum from @Weeks where id = 8
select @WeekNum9 = WeekNum from @Weeks where id = 9
select @WeekNum10 = WeekNum from @Weeks where id = 10
select @WeekNum11 = WeekNum from @Weeks where id = 11
select @WeekNum12 = WeekNum from @Weeks where id = 12
select @WeekNum13 = WeekNum from @Weeks where id = 13
select @WeekNum14 = WeekNum from @Weeks where id = 14
select @WeekNum15 = WeekNum from @Weeks where id = 15
select @WeekNum16 = WeekNum from @Weeks where id = 16
select @WeekNum17 = WeekNum from @Weeks where id = 17
select @WeekNum18 = WeekNum from @Weeks where id = 18
select @WeekNum19 = WeekNum from @Weeks where id = 19
select @WeekNum20 = WeekNum from @Weeks where id = 20	
	

SELECT (e.[ID]) AS ProjectID, e.ProjectNo, UPPER(ProjectName) AS ProjectName, e.PICCode, e.PM1Code , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum1)) AS [Week1] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum2))) AS [Week2] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum3))) AS [Week3] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum4))) AS [Week4] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum5))) AS [Week5] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum6))) AS [Week6] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum7))) AS [Week7] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum8))) AS [Week8] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum9))) AS [Week9] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum10))) AS [Week10] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum11))) AS [Week11] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum12))) AS [Week12] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum13))) AS [Week13] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum14))) AS [Week14] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum15))) AS [Week15] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum16))) AS [Week16] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum17))) AS [Week17] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum18))) AS [Week18] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum19))) AS [Week19] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek s WHERE s.ProjectID = e.ID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = (@WeekNum20))) AS [Week20] 

FROM tblProjects e 
WHERE ((e.PIC = @EmployeeID) OR (e.PM1 = @EmployeeID)) 
AND (e.Active = 1) 
AND e.ID in (Select ProjectID from tblEmployeeProjectAssignments where employeeid = @EmployeeID and assigned = 1)
GROUP BY e.ID, e.ProjectNo, e.ProjectName, e.PICCode, e.PM1Code 
--ORDER BY e.ProjectNo

Union

SELECT e.ProjectID, e.ProjectNo, UPPER(e.ProjectName) AS ProjectName, e.PICCode, e.PM1Code , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum1)) AS [Week1] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum2)) AS [Week2] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum3)) AS [Week3] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum4)) AS [Week4] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum5)) AS [Week5] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum6)) AS [Week6] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum7)) AS [Week7] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum8)) AS [Week8] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum9)) AS [Week9] , 
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum10)) AS [Week10] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum11)) AS [Week11] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum12)) AS [Week12] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum13)) AS [Week13] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum14)) AS [Week14] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum15)) AS [Week15] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum16)) AS [Week16] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum17)) AS [Week17] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum18)) AS [Week18] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum19)) AS [Week19] ,
(SELECT SUM ([Hours]) FROM v_EmployeeScheduleByWeek_ActiveProjects s WHERE s.ProjectID = e.ProjectID AND (s.WeekNoStartDate BETWEEN @StartDate AND @EndDate) AND (s.WeekNo = @WeekNum20)) AS [Week20] 

FROM v_EmployeeScheduleByWeek_ActiveProjects e 
WHERE e.EmployeeID = @EmployeeID 
AND (e.PIC <> @EmployeeID) 
AND (e.PM1 <> @EmployeeID)
AND e.ProjectID in (Select ProjectID from tblEmployeeProjectAssignments where employeeid = @EmployeeID and assigned = 1) 
GROUP BY e.ProjectID, e.ProjectNo, e.ProjectName, e.PICCode, e.PM1Code, e.EmployeeID 
--ORDER BY e.ProjectNo
order by e.ProjectNo

END


GO



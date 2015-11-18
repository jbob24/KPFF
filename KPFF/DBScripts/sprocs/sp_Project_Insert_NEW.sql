IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Project_Insert_NEW]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Project_Insert_NEW]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[sp_Project_Insert_NEW]
	@ProjectNo float,
	@ProjectName nvarchar(50),
	@ClientID int,
	@ProjectLocation nvarchar(100),
	@ConstructionType nvarchar(100),
	@ProjectType nvarchar(100),
	@PhaseID int,
	@EstimatedStartDate datetime,
	@EstimatedCompletionDate datetime,
	@FeeAmount nvarchar(50),
	@FeeStructure nvarchar(50),
	@Comments ntext,
	@PIC int,
	@PICCode nvarchar(50),
	@PM1 int,
	@PM1Code nvarchar(50),
	@EmployeeID int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @newID int
	declare @weekId int

        INSERT INTO tblProjects
        (ProjectName, ProjectNo, ClientID, ProjectLocation, ConstructionType,
        ProjectType, PhaseID, EstimatedStartDate, EstimatedCompletionDate, FeeAmount,
        FeeStructure, Comments, PIC, PICCode, PM1, PM1Code)
        VALUES (@ProjectName, @ProjectNo, @ClientID, @Projectlocation, @ConstructionType,
        @ProjectType, @PhaseID, @EstimatedStartDate, @EstimatedCompletionDate, CONVERT(money, @FeeAmount),
        @FeeStructure, @Comments, @PIC, @PICCode, @PM1, @PM1Code)
        
        set @newID = SCOPE_IDENTITY()
        SELECT @weekId = id FROM tblWeeks where WeekNoStartDate < GETDATE() and WeekNoStartDate > DATEADD(WEEK,-1,GETDATE())
        
		if (@PIC > 0)
		begin
			exec sp_AddProject @newID,@PIC,@weekId,@EmployeeID
		end
		
		if (@PM1 > 0)
		begin
			exec sp_AddProject @newID,@PM1,@weekId,@EmployeeID
		end
END


GO



IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblEngineerGroupMembers_tblEmployees]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblEngineerGroupMembers]'))
ALTER TABLE [dbo].[tblEngineerGroupMembers] DROP CONSTRAINT [FK_tblEngineerGroupMembers_tblEmployees]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblEngineerGroupMembers_tblEngineerGroups]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblEngineerGroupMembers]'))
ALTER TABLE [dbo].[tblEngineerGroupMembers] DROP CONSTRAINT [FK_tblEngineerGroupMembers_tblEngineerGroups]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblEngineerGroupMembers_Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblEngineerGroupMembers] DROP CONSTRAINT [DF_tblEngineerGroupMembers_Active]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblEngineerGroupMembers_DateAdded]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblEngineerGroupMembers] DROP CONSTRAINT [DF_tblEngineerGroupMembers_DateAdded]
END

GO
/****** Object:  Table [dbo].[tblEngineerGroupMembers]    Script Date: 01/09/2012 22:35:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblEngineerGroupMembers]') AND type in (N'U'))
DROP TABLE [dbo].[tblEngineerGroupMembers]
GO

/****** Object:  Table [dbo].[tblEngineerGroupMembers]    Script Date: 01/09/2012 22:35:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblEngineerGroupMembers](
	[GroupID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_tblEngineerGroupMembers] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC,
	[EmployeeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[tblEngineerGroupMembers]  WITH CHECK ADD  CONSTRAINT [FK_tblEngineerGroupMembers_tblEmployees] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[tblEmployees] ([EmployeeID])
GO

ALTER TABLE [dbo].[tblEngineerGroupMembers] CHECK CONSTRAINT [FK_tblEngineerGroupMembers_tblEmployees]
GO

ALTER TABLE [dbo].[tblEngineerGroupMembers]  WITH CHECK ADD  CONSTRAINT [FK_tblEngineerGroupMembers_tblEngineerGroups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[tblEngineerGroups] ([ID])
GO

ALTER TABLE [dbo].[tblEngineerGroupMembers] CHECK CONSTRAINT [FK_tblEngineerGroupMembers_tblEngineerGroups]
GO

ALTER TABLE [dbo].[tblEngineerGroupMembers] ADD  CONSTRAINT [DF_tblEngineerGroupMembers_Active]  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [dbo].[tblEngineerGroupMembers] ADD  CONSTRAINT [DF_tblEngineerGroupMembers_DateAdded]  DEFAULT (getdate()) FOR [DateAdded]
GO



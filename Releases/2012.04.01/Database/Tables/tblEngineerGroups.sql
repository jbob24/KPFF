IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblEngineerGroups_Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblEngineerGroups] DROP CONSTRAINT [DF_tblEngineerGroups_Active]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblEngineerGroups_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblEngineerGroups] DROP CONSTRAINT [DF_tblEngineerGroups_DateCreated]
END

GO

/****** Object:  Table [dbo].[tblEngineerGroups]    Script Date: 01/09/2012 22:28:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblEngineerGroups]') AND type in (N'U'))
DROP TABLE [dbo].[tblEngineerGroups]
GO

/****** Object:  Table [dbo].[tblEngineerGroups]    Script Date: 01/09/2012 22:28:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblEngineerGroups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](255) NULL,
	[Active] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblEngineerGroups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[tblEngineerGroups] ADD  CONSTRAINT [DF_tblEngineerGroups_Active]  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [dbo].[tblEngineerGroups] ADD  CONSTRAINT [DF_tblEngineerGroups_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO



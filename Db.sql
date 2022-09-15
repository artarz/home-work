USE [ToDoDB]
GO

ALTER TABLE [dbo].[ToDo] DROP CONSTRAINT [DF_ToDo_CreatedDate]
GO

ALTER TABLE [dbo].[ToDo] DROP CONSTRAINT [DF_ToDo_IsComplete]
GO

/****** Object:  Index [IX_ToDo_1]    Script Date: 9/15/2022 17:12:05 ******/
DROP INDEX [IX_ToDo_1] ON [dbo].[ToDo]
GO

/****** Object:  Index [IX_ToDo]    Script Date: 9/15/2022 17:12:05 ******/
DROP INDEX [IX_ToDo] ON [dbo].[ToDo]
GO

/****** Object:  Table [dbo].[ToDo]    Script Date: 9/15/2022 17:12:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ToDo]') AND type in (N'U'))
DROP TABLE [dbo].[ToDo]
GO

/****** Object:  Table [dbo].[ToDo]    Script Date: 9/15/2022 17:12:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ToDo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[IsComplete] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ToDo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Index [IX_ToDo]    Script Date: 9/15/2022 17:12:05 ******/
CREATE NONCLUSTERED INDEX [IX_ToDo] ON [dbo].[ToDo]
(
	[CreatedDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

/****** Object:  Index [IX_ToDo_1]    Script Date: 9/15/2022 17:12:05 ******/
CREATE NONCLUSTERED INDEX [IX_ToDo_1] ON [dbo].[ToDo]
(
	[IsComplete] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ToDo] ADD  CONSTRAINT [DF_ToDo_IsComplete]  DEFAULT ((0)) FOR [IsComplete]
GO

ALTER TABLE [dbo].[ToDo] ADD  CONSTRAINT [DF_ToDo_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO



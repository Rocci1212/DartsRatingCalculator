/****** Object:  Table [dbo].[Campaign]    Script Date: 7/29/2015 12:22:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Campaign](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Season] [int] NOT NULL CONSTRAINT [DF_Campaign_Season]  DEFAULT (''),
	[Year] [int] NOT NULL CONSTRAINT [DF_Campaign_Year]  DEFAULT ((2000)),
	[Class] [int] NOT NULL CONSTRAINT [DF_Campaign_Class]  DEFAULT (''),
	[Conference] [int] NOT NULL CONSTRAINT [DF_Campaign_Conference]  DEFAULT (''),
	[Identifier] [int] NULL,
 CONSTRAINT [PK_Campaign] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



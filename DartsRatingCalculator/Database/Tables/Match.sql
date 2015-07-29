/****** Object:  Table [dbo].[Match]    Script Date: 7/29/2015 1:01:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Match](
	[ID] [int] NOT NULL,
	[WeekNumber] [int] NOT NULL,
	[AwaySquad] [int] NOT NULL,
	[HomeSquad] [int] NOT NULL,
	[Campaign] [int] NOT NULL,
 CONSTRAINT [PK_Match] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Match] ADD  CONSTRAINT [DF_Match_WeekNumber]  DEFAULT ((0)) FOR [WeekNumber]
GO

ALTER TABLE [dbo].[Match] ADD  CONSTRAINT [DF_Match_AwaySquad]  DEFAULT ((0)) FOR [AwaySquad]
GO

ALTER TABLE [dbo].[Match] ADD  CONSTRAINT [DF_Match_HomeSquad]  DEFAULT ((0)) FOR [HomeSquad]
GO

ALTER TABLE [dbo].[Match] ADD  CONSTRAINT [DF_Match_Campaign]  DEFAULT ((0)) FOR [Campaign]
GO


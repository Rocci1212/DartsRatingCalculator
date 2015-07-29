/****** Object:  Table [dbo].[Game]    Script Date: 7/29/2015 12:25:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Game](
	[Match] [int] NOT NULL,
	[GameNumber] [int] NOT NULL,
	[GameType] [int] NOT NULL,
	[AwaySquad] [int] NOT NULL,
	[HomeSquad] [int] NOT NULL,
	[Result] [int] NOT NULL,
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[Match] ASC,
	[GameNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Game] ADD  CONSTRAINT [DF_Game_GameType]  DEFAULT ((0)) FOR [GameType]
GO

ALTER TABLE [dbo].[Game] ADD  CONSTRAINT [DF_Game_AwaySquad]  DEFAULT ((0)) FOR [AwaySquad]
GO

ALTER TABLE [dbo].[Game] ADD  CONSTRAINT [DF_Game_HomeSquad]  DEFAULT ((0)) FOR [HomeSquad]
GO

ALTER TABLE [dbo].[Game] ADD  CONSTRAINT [DF_Game_Result]  DEFAULT ((0)) FOR [Result]
GO


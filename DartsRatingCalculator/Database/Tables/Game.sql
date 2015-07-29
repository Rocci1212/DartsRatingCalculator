/****** Object:  Table [dbo].[Game]    Script Date: 7/29/2015 5:53:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Game](
	[Match] [int] NOT NULL,
	[GameNumber] [int] NOT NULL,
	[GameType] [int] NOT NULL,
	[Player] [int] NOT NULL,
	[Result] [int] NOT NULL,
	[PreRatingMean] [numeric](19, 5) NOT NULL,
	[PreRatingDeviation] [numeric](19, 5) NOT NULL,
	[PreCampaignRatingMean] [numeric](19, 5) NOT NULL,
	[PreCampaignRatingDeviation] [numeric](19, 5) NOT NULL,
	[PreClassRatingMean] [numeric](19, 5) NOT NULL,
	[PreClassRatingDeviation] [numeric](19, 5) NOT NULL,
	[PostRatingMean] [numeric](19, 5) NOT NULL,
	[PostRatingDeviation] [numeric](19, 5) NOT NULL,
	[PostCampaignRatingMean] [numeric](19, 5) NOT NULL,
	[PostCampaignRatingDeviation] [numeric](19, 5) NOT NULL,
	[PostClassRatingMean] [numeric](19, 5) NOT NULL,
	[PostClassRatingDeviation] [numeric](19, 5) NOT NULL,
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[Match] ASC,
	[GameNumber] ASC,
	[Player] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Game] ADD  CONSTRAINT [DF_Game_GameType]  DEFAULT ((0)) FOR [GameType]
GO

ALTER TABLE [dbo].[Game] ADD  CONSTRAINT [DF_Game_AwaySquad]  DEFAULT ((0)) FOR [Player]
GO

ALTER TABLE [dbo].[Game] ADD  CONSTRAINT [DF_Game_Result]  DEFAULT ((0)) FOR [Result]
GO


/****** Object:  Table [dbo].[PlayerCampaignRating]    Script Date: 7/29/2015 1:03:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PlayerCampaignRating](
	[Player] [int] NOT NULL,
	[Campaign] [int] NOT NULL,
	[Wins] [int] NOT NULL CONSTRAINT [DF_PlayerCampaignRating_Wins]  DEFAULT ((0)),
	[Losses] [int] NOT NULL CONSTRAINT [DF_PlayerCampaignRating_Losses]  DEFAULT ((0)),
	[RatingMean] [numeric](19, 5) NOT NULL CONSTRAINT [DF_PlayerCampaignRating_RatingMean]  DEFAULT ((25)),
	[RatingDeviation] [numeric](19, 5) NOT NULL CONSTRAINT [DF_PlayerCampaignRating_RatingDeviation]  DEFAULT ((8.33333)),
 CONSTRAINT [PK_PlayerCampaignRating] PRIMARY KEY CLUSTERED 
(
	[Player] ASC,
	[Campaign] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

